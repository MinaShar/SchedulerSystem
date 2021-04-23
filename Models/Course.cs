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
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int Term { get; set; }
        public int DepartmentId { get; set; }
        public int CreditHours { get; set; }
        public int NumberOfLectures { get; set; }
        public int NumberOfSections { get; set; }
        public int NumberOfLabs { get; set; }


        public int GroupNumber { get; set; }

        /// <summary>
        /// get the courses that this doctor give the lecture in it
        /// </summary>
        /// <param name="DoctorID"></param>
        /// <returns></returns>
        public static List<Course> GetCoursesOfDoctor(int DoctorID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT Courses.Id AS CourseID,* FROM LecturesOfGroupsDoctors
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroupsDoctors.LecturesOfGroupsId=LecturesOfGroups.Id
                                              INNER JOIN CourseSemester ON LecturesOfGroups.CourseSemesterId=CourseSemester.Id
                                              INNER JOIN Courses ON Courses.Id=CourseSemester.CourseId
                                              INNER JOIN Semesters ON Semesters.Id=CourseSemester.SemesterId
                                              WHERE Semesters.Id IN (SELECT MAX(Id) FROM Semesters)
                                              AND LecturesOfGroupsDoctors.DoctorId={0}
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
                List<Course> CoursesList =
                    (from item in myEnumerable
                     select new Course
                     {
                         Id = item.Field<int>("CourseID"),
                         CourseName = item.Field<string>("CourseName"),
                         Term = item.Field<int>("Term"),
                         DepartmentId = item.Field<int>("DepartmentId"),
                         CreditHours = item.Field<int>("CreditHours"),
                         NumberOfLectures = item.Field<int>("NumberOfLectures"),
                         NumberOfSections = item.Field<int>("NumberOfSections"),
                         NumberOfLabs = item.Field<int>("NumberOfLabs"),
                         GroupNumber = item.Field<int>("GroupNumber")
                     }).ToList();
                return CoursesList;
            }
        }

        /// <summary>
        /// return list with only one course that have this id
        /// </summary>
        /// <param name="CourseID"></param>
        /// <returns></returns>
        public static List<Course> GetCourseByID(int CourseID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM Courses WHERE Id={0};", CourseID);
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
                List<Course> CoursesList =
                    (from item in myEnumerable
                     select new Course
                     {
                         Id = item.Field<int>("Id"),
                         CourseName = item.Field<string>("CourseName"),
                         Term = item.Field<int>("Term"),
                         DepartmentId = item.Field<int>("DepartmentId"),
                         CreditHours = item.Field<int>("CreditHours"),
                         NumberOfLectures = item.Field<int>("NumberOfLectures"),
                         NumberOfSections = item.Field<int>("NumberOfSections"),
                         NumberOfLabs = item.Field<int>("NumberOfLabs")
                     }).ToList();
                return CoursesList;
            }
        }

        public static Course GetCourseByCourseSemesterID(int CourseSemesterID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM CourseSemester 
                                              INNER JOIN Courses ON CourseSemester.CourseId=Courses.Id
                                              WHERE CourseSemester.Id={0}",CourseSemesterID);
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
                List<Course> CoursesList =
                    (from item in myEnumerable
                     select new Course
                     {
                         Id = item.Field<int>("CourseId"),
                         CourseName = item.Field<string>("CourseName"),
                         Term = item.Field<int>("Term"),
                         DepartmentId = item.Field<int>("DepartmentId"),
                         CreditHours = item.Field<int>("CreditHours"),
                         NumberOfLectures = item.Field<int>("NumberOfLectures"),
                         NumberOfSections = item.Field<int>("NumberOfSections"),
                         NumberOfLabs = item.Field<int>("NumberOfLabs")
                     }).ToList();
                return CoursesList[0];
            }
        }


        public static List<Course> GetCoursesByTermAndDepartment(int term, int DepartmentId)
        {

            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM Courses WHERE Term={0} AND DepartmentId={1};", term, DepartmentId);
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
                List<Course> CoursesList =
                    (from item in myEnumerable
                     select new Course
                     {
                         Id = item.Field<int>("Id"),
                         CourseName = item.Field<string>("CourseName"),
                         Term=item.Field<int>("Term"),
                         DepartmentId=item.Field<int>("DepartmentId"),
                         CreditHours=item.Field<int>("CreditHours"),
                         NumberOfLectures=item.Field<int>("NumberOfLectures"),
                         NumberOfSections=item.Field<int>("NumberOfSections"),
                         NumberOfLabs=item.Field<int>("NumberOfLabs")
                     }).ToList();
                return CoursesList;
            }

        }
    }
}
