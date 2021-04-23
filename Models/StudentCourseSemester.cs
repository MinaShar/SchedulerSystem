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
    public class StudentCourseSemester
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public int Grade { get; set; }
        public int Saved { get; set; }

        public string CourseName { get; set; }
        public int NumberOfLectures { get; set; }
        public int NumberOfSections { get; set; }
        public int NumberOfLabs { get; set; }
        public List<LecturesOfgroupsStudents> LecturesOfGroupsStudentsEquivelent { get; set; }


        public static List<StudentCourseSemester> GetCurrentSemesterGrade(int StudentID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM StudentCourseSemester
                                              INNER JOIN Courses ON StudentCourseSemester.CourseId=Courses.Id
                                              WHERE StudentCourseSemester.StudentId={0}
                                              AND StudentCourseSemester.SemesterId IN (SELECT MAX(Id) FROM Semesters)", StudentID);
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
                List<StudentCourseSemester> UsersList =
                    (from item in myEnumerable
                     select new StudentCourseSemester
                     {
                         CourseName = item.Field<string>("CourseName"),
                         Grade = item["Grade"] == DBNull.Value ? -1 : item.Field<int>("Grade")
                     }).ToList();
                return UsersList;
            }
        }
        /// <summary>
        /// return the number of periods the student registered in, in this course
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfThePeriodsThisStudentRegisteredForThisCourse(int StudentID, int CourseID, int PeriodType)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM StudentCourseSemester
                                              INNER JOIN LecturesOfGroupsStudents ON LecturesOfGroupsStudents.StudentCourseSemesterId=StudentCourseSemester.Id
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroups.Id=LecturesOfGroupsStudents.LecturesOfGroupsId
                                              WHERE StudentCourseSemester.StudentId={0} AND StudentCourseSemester.CourseId={1} 
                                              AND StudentCourseSemester.SemesterId IN (SELECT MAX(Id) FROM Semesters)
                                              AND LecturesOfGroups.PeriodType={2}", StudentID, CourseID, PeriodType);
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
            }
            return dt.Rows.Count;
        }
        public static bool IsStudentAlreadyRegisteredInCourseInLastSemester(int CourseID, int StudentID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM StudentCourseSemester 
                                              WHERE StudentId={0} AND CourseId={1} AND SemesterId IN (SELECT MAX(Id) FROM Semesters)", StudentID, CourseID);
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
            }
            return dt.Rows.Count > 0 ? true : false;
        }

        public static void InsertGrade(int StudentID, int CourseID, int Grade)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"UPDATE StudentCourseSemester
                                              SET Grade={0}
                                              WHERE StudentId={1} AND CourseId={2} AND SemesterId IN (SELECT MAX(Id) FROM Semesters)", Grade, StudentID, CourseID);
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

        public static void DeleterecordByID(int StudentCourseSemesterID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"DELETE FROM StudentCourseSemester WHERE Id={0}", StudentCourseSemesterID);
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
        /// according to the LectureOfGroupID sent, it determine whether the student
        /// already registered in the course or not if not it register the student
        /// and in both case return the id of the StudentCourseSemester
        /// </summary>
        /// <param name="LectureOfGroupID"></param>
        /// <returns></returns>
        public static int RegisterStudentInCourse(int LectureOfGroupID, int StudentID)
        {
            int CourseID = LecturesOfGroups.GetCourseIDEquivelentToLectureOfGroupID(LectureOfGroupID);
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM StudentCourseSemester
                                              WHERE StudentCourseSemester.StudentId={0}
                                              AND StudentCourseSemester.CourseId={1}
                                              AND StudentCourseSemester.SemesterId IN (SELECT MAX(Id) FROM Semesters)", StudentID, CourseID);
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
            }
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return int.Parse(row["Id"].ToString());
            }


            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"INSERT INTO StudentCourseSemester (StudentId,CourseId,SemesterId,Saved) VALUES ({0},{1},(SELECT MAX(Id) FROM Semesters),0)
                                              SELECT SCOPE_IDENTITY() AS ID", StudentID, CourseID);
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

        public static ResponseToFrontEnd SaveRegistration(int StudetID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM StudentCourseSemester
                                              INNER JOIN Courses ON StudentCourseSemester.CourseId=Courses.Id
                                              WHERE StudentId={0} AND SemesterId IN(SELECT MAX(Id) FROM Semesters) AND Saved=0", StudetID);
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
            }
            var myEnumerable = dt.AsEnumerable();
            List<StudentCourseSemester> StudentCourseSemesterList =
                (from item in myEnumerable
                 select new StudentCourseSemester
                 {
                     Id = item.Field<int>("Id"),
                     StudentId = item.Field<int>("StudentId"),
                     CourseId = item.Field<int>("CourseId"),
                     SemesterId = item.Field<int>("SemesterId"),
                     Saved = item.Field<int>("Saved"),
                     NumberOfLectures = item.Field<int>("NumberOfLectures"),
                     NumberOfSections = item.Field<int>("NumberOfSections"),
                     NumberOfLabs = item.Field<int>("NumberOfLabs")
                 }).ToList();

            foreach (StudentCourseSemester x in StudentCourseSemesterList)
            {
                x.LecturesOfGroupsStudentsEquivelent = LecturesOfgroupsStudents.GetLecturesOfGroupsStudentsByStudentCourseSemesterID(x.Id);
            }

            ////////////////////////////////////check loop//////////////////////////////////

            foreach (StudentCourseSemester x in StudentCourseSemesterList)
            {
                int NumberOfLecturesRegisteredByStudent = 0;
                int NumberOfSectionsRegisteredByStudent = 0;
                int NumberOfLabsRegisteredByStudent = 0;
                foreach (LecturesOfgroupsStudents y in x.LecturesOfGroupsStudentsEquivelent)
                {
                    if (y.PeriodType == 1)
                    {
                        NumberOfLecturesRegisteredByStudent++;
                    }
                    else if (y.PeriodType == 2)
                    {
                        NumberOfSectionsRegisteredByStudent++;
                    }
                    else if (y.PeriodType == 3)
                    {
                        NumberOfLabsRegisteredByStudent++;
                    }
                }
                if(NumberOfLecturesRegisteredByStudent < x.NumberOfLectures)
                {
                    return new ResponseToFrontEnd { Flag = false, ReasonWhyNot = "You should add Lecture" };
                }
                if(NumberOfSectionsRegisteredByStudent < x.NumberOfSections)
                {
                    return new ResponseToFrontEnd { Flag = false, ReasonWhyNot = "You should add Tutorial" };
                }
                if(NumberOfLabsRegisteredByStudent < x.NumberOfLabs)
                {
                    return new ResponseToFrontEnd { Flag = false, ReasonWhyNot = "You should add Lab" };
                }
            }
            UpdateValuesForSaving(StudetID);
            return new ResponseToFrontEnd { Flag = true };

            ////////////////////////////////////////////////////////////////////////////////
        }

        public static void UpdateValuesForSaving(int StudnetID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"UPDATE StudentCourseSemester SET Saved=1
                                              WHERE StudentId={0} AND SemesterId IN (SELECT MAX(Id) FROM Semesters)", StudnetID);
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

        public static void DeleteUnsavedRegistration(int StudentID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"DELETE FROM StudentCourseSemester
                                              WHERE StudentId={0} AND SemesterId IN (SELECT MAX(Id) FROM Semesters) AND Saved=0", StudentID);
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

        public static List<StudentCourseSemester> GetGradesOfStudentInSemester(int StudentID, int SemesterID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM StudentCourseSemester
                                              INNER JOIN Courses ON StudentCourseSemester.CourseId=Courses.Id
                                              WHERE StudentId={0} AND SemesterId={1}", StudentID, SemesterID);
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
                List<StudentCourseSemester> UsersList =
                    (from item in myEnumerable
                     select new StudentCourseSemester
                     {
                         CourseName = item.Field<string>("CourseName"),
                         Grade = item["Grade"] == DBNull.Value ? -1 : item.Field<int>("Grade")
                     }).ToList();
                return UsersList;
            }
        }

        public static int GetStudentGradeInSemester(int StudentID, int SemesterID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT SUM(Grade) AS SUM FROM StudentCourseSemester
                                              WHERE StudentId={0} AND SemesterId={1}", StudentID, SemesterID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        object ob = cmd.ExecuteScalar();
                        if (ob == DBNull.Value)
                        {
                            return -1;
                        }
                        return int.Parse(string.Format("{0}", ob));
                    }
                }
            }
        }

    }
}
