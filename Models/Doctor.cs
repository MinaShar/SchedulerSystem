using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Rank { get; set; }


        /// <summary>
        /// return false if it overlapas true => donot overlaps
        /// </summary>
        /// <param name="DoctorID"></param>
        /// <param name="PeriodDay"></param>
        /// <param name="PeriodNumber"></param>
        /// <returns></returns>
        public static bool CheckDoctorOverLap(int DoctorID,int PeriodDay,int PeriodNumber)
        {
            int LastSemesterID = Semester.GetLastSemesterID();
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT CourseSemesterId,PeriodDay,PeriodNumber
                                              FROM LecturesOfGroups

                                              INNER JOIN CourseSemester ON CourseSemester.Id=LecturesOfGroups.CourseSemesterId
                                              INNER JOIN Semesters ON CourseSemester.SemesterId=Semesters.Id AND Semesters.Id={0}

                                              WHERE LecturesOfGroups.Id IN
                                              (
                                                  SELECT LecturesOfGroupsId   
                                                  FROM LecturesOfGroupsDoctors
                                                  WHERE DoctorId={1}
                                              )", LastSemesterID,DoctorID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        adabter.Fill(dt);
                    }
                }

                foreach(DataRow r in dt.Rows)
                {
                    if(int.Parse(r["PeriodDay"].ToString())==PeriodDay && int.Parse(r["PeriodNumber"].ToString()) == PeriodNumber)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// get the doctors LectureOfGroupID
        /// </summary>
        /// <param name="LectureOfGroupID"></param>
        /// <returns></returns>
        public static List<Doctor> GetDoctorsByLectureOfGroupID(int LectureOfGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT Doctors.Id AS DoctorID,* FROM LecturesOfGroupsDoctors
                                              INNER JOIN Doctors ON LecturesOfGroupsDoctors.DoctorId=Doctors.Id
                                              WHERE LecturesOfGroupsDoctors.LecturesOfGroupsId={0}", LectureOfGroupID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        adabter.Fill(dt);
                    }
                }

                var myEnumerable = dt.AsEnumerable();
                List<Doctor> UsersList =
                    (from item in myEnumerable
                     select new Doctor
                     {
                         Id = item.Field<int>("DoctorID"),
                         Name = item.Field<string>("Name"),
                         Password = item.Field<string>("Password"),
                         Rank = item.Field<int>("Rank")
                     }).ToList();
                return UsersList;
            }
        }


        public static Doctor GetDoctorByID(int DoctorID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM Doctors WHERE Id={0};",DoctorID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        adabter.Fill(dt);
                    }
                }

                var myEnumerable = dt.AsEnumerable();
                List<Doctor> UsersList =
                    (from item in myEnumerable
                     select new Doctor
                     {
                         Id = item.Field<int>("Id"),
                         Name = item.Field<string>("Name"),
                         Password = item.Field<string>("Password"),
                         Rank = item.Field<int>("Rank")
                     }).ToList();
                return UsersList[0];
            }
        }


        public static List<Doctor> GetAll()
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM Doctors;");
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        adabter.Fill(dt);
                    }
                }

                var myEnumerable = dt.AsEnumerable();
                List<Doctor> UsersList =
                    (from item in myEnumerable
                     select new Doctor
                     {
                         Id = item.Field<int>("Id"),
                         Name = item.Field<string>("Name"),
                         Password = item.Field<string>("Password"),
                         Rank = item.Field<int>("Rank")
                     }).ToList();
                return UsersList;
            }
        }
        /// <summary>
        /// insert new doctor and return the inserted id
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <param name="Rank"></param>
        /// <returns></returns>
        public static int InsertNewDoctor(string Name,string Password,int Rank)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"INSERT INTO Doctors (Name,Password,Rank) VALUES('{0}','{1}',{2})
                                              SELECT SCOPE_IDENTITY() AS ID", Name, Password, Rank);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        object ob = cmd.ExecuteScalar();
                        return int.Parse(string.Format("{0}", ob));
                    }
                }
            }
        }

    }
}
