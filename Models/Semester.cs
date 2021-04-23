using System;
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
    public class Semester
    {
        public int Id { get; set; }
        public string SemesterName { get; set; }
        public DateTime StartD { get; set; }
        public DateTime EndD { get; set; }
        public bool IsWorking { get; set; }
        public bool IsRegistrationOpen { get; set; }


        public int GroupNumber { get; set; }

        /// <summary>
        /// flag=>true close semester || flag=>false open semester
        /// </summary>
        /// <param name="flag"></param>
        public static void CloseSemester(bool flag)
        {
            int x;
            if (flag == true)
            {
                x = 0;
            }
            else
            {
                x = 1;
            }
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"UPDATE Semesters SET IsWorking={0}
                                              WHERE Id IN (SELECT MAX(Id) FROM Semesters)", x);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }


        /// <summary>
        /// flag=>true open registration || flag=>false close registration
        /// for the current semester
        /// </summary>
        /// <param name="flag"></param>
        public static void OpenRegestration(bool flag)
        {
            int x;
            if (flag == true)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"UPDATE Semesters SET IsRegistrationOpen={0}
                                              WHERE Id IN (SELECT MAX(Id) FROM Semesters)", x);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static Semester GetLastSemester()
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM Semesters WHERE Id IN (SELECT MAX(Id) FROM Semesters)");
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
                List<Semester> UsersList =
                    (from item in myEnumerable
                     select new Semester
                     {
                         Id = item.Field<int>("Id"),
                         SemesterName = item.Field<string>("SemesterName"),
                         StartD = item.Field<DateTime>("StartD"),
                         EndD = item.Field<DateTime>("EndD"),
                         IsWorking = item.Field<bool>("IsWorking"),
                         IsRegistrationOpen = item.Field<bool>("IsRegistrationOpen")
                     }).ToList();
                return UsersList[0];
            }
        }

        public static int GetLastSemesterID()
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("select MAX(Id) from Semesters;");
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        adabter.Fill(dt);
                    }
                    return int.Parse(dt.Rows[0][0].ToString());
                }
            }
        }
        /// <summary>
        /// insert new semester and return the inserted id
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int InsertNewSemester(string Name, string start, string end)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"INSERT INTO Semesters (SemesterName,StartD,EndD,IsWorking,IsRegistrationOpen) VALUES('{0}','{1}','{2}',{3},{4})
                                              SELECT SCOPE_IDENTITY() AS ID", Name, start, end, 1, 0);
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

        public static List<Semester> GetStudentSemesters(int StudetID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT DISTINCT Semesters.Id AS SemesterID,SemesterName,StartD,EndD FROM StudentCourseSemester
                                              INNER JOIN Semesters ON StudentCourseSemester.SemesterId=Semesters.Id
                                              WHERE StudentCourseSemester.StudentId={0}", StudetID);
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
                List<Semester> UsersList =
                    (from item in myEnumerable
                     select new Semester
                     {
                         Id = item.Field<int>("SemesterID"),
                         SemesterName = item.Field<string>("SemesterName"),
                         StartD = item.Field<DateTime>("StartD"),
                         EndD = item.Field<DateTime>("EndD")
                     }).ToList();
                return UsersList;
            }
        }

        public static List<Semester> GetPreviosSemestersOFDoctor(int DoctorID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT DISTINCT Semesters.Id AS SemesterID,Semesters.SemesterName,Semesters.StartD,Semesters.EndD
                                              FROM LecturesOfGroupsDoctors
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroups.Id=LecturesOfGroupsDoctors.LecturesOfGroupsId
                                              INNER JOIN CourseSemester ON CourseSemester.Id=LecturesOfGroups.CourseSemesterId
                                              INNER JOIN Semesters ON Semesters.Id=CourseSemester.SemesterId
                                              WHERE LecturesOfGroupsDoctors.DoctorId={0}
                                              AND LecturesOfGroups.PeriodType=1", DoctorID);
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
                List<Semester> UsersList =
                    (from item in myEnumerable
                     select new Semester
                     {
                         Id = item.Field<int>("SemesterID"),
                         SemesterName = item.Field<string>("SemesterName"),
                         StartD = item.Field<DateTime>("StartD"),
                         EndD = item.Field<DateTime>("EndD")
                     }).ToList();
                return UsersList;
            }
        }

    }
}
