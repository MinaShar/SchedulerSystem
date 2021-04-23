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
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }


        public int Grade { get; set; }

        public static List<Student> GetStudetsRegisterdInCourseUnderSpecificDoctor(int DoctorID, int CourseID, int GroupNumber)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT Students.Id AS StudentID,* FROM StudentCourseSemester
                                              INNER JOIN LecturesOfGroupsStudents ON StudentCourseSemester.Id=LecturesOfGroupsStudents.StudentCourseSemesterId
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroups.Id=LecturesOfGroupsStudents.LecturesOfGroupsId
                                              INNER JOIN LecturesOfGroupsDoctors ON LecturesOfGroups.Id=LecturesOfGroupsDoctors.LecturesOfGroupsId
                                              INNER JOIN Students ON StudentCourseSemester.StudentId=Students.Id
                                              WHERE LecturesOfGroupsDoctors.DoctorId={0}
                                              AND StudentCourseSemester.CourseId={1}
                                              AND StudentCourseSemester.SemesterId IN (SELECT MAX(Id) FROM Semesters)
                                              AND LecturesOfGroups.GroupNumber={2}
                                              AND LecturesOfGroups.PeriodType=1", DoctorID, CourseID, GroupNumber);
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
                List<Student> StudentList =
                    (from item in myEnumerable
                     select new Student
                     {
                         Id = item.Field<int>("StudentID"),
                         Name = item.Field<string>("Name"),
                         Password = item.Field<string>("Password"),
                         DepartmentId = item.Field<int>("DepartmentId"),
                         Grade = item["Grade"] == DBNull.Value ? 0 : item.Field<int>("Grade")
                     }).ToList();
                return StudentList;
            }
        }

        /// <summary>
        /// insert new student and return its id
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        public static int InsertNewStudent(string Name, string Password, int DepartmentId)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"INSERT INTO Students (Name,Password,DepartmentId) VALUES('{0}','{1}',{2})
                                              SELECT SCOPE_IDENTITY() AS ID", Name, Password, DepartmentId);
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

        public static Student GetStudentById(int ID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM Students WHERE Id={0};", ID);
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
                List<Student> StudentList =
                    (from item in myEnumerable
                     select new Student
                     {
                         Id = item.Field<int>("Id"),
                         Name = item.Field<string>("Name"),
                         Password = item.Field<string>("Password"),
                         DepartmentId = item.Field<int>("DepartmentId")
                     }).ToList();
                return StudentList[0];
            }
        }
    }
}
