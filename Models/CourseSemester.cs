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
    public class CourseSemester
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int SemesterId { get; set; }
        public int NumberOfGroups { get; set; }
        public int NumberOfSectionsPerGroup { get; set; }

        public int NumberOfLectures { get; set; }
        public int NumberOfSections { get; set; }
        public int NumberOfLabs { get; set; }
        public int CreditHours { get; set; }


        public static bool CheckSectionNumberInRange(int CourseSemesterID,int SectionNumberEnterd)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT CourseSemester.NumberOfSectionsPerGroup FROM CourseSemester WHERE CourseSemester.Id={0}", CourseSemesterID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        object ob = cmd.ExecuteScalar();
                        int SectionsNumberInDB = int.Parse(string.Format("{0}", ob));
                        if(SectionNumberEnterd>0 && SectionNumberEnterd<= SectionsNumberInDB)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        public static int GetCourseSemesterIDForSpecificLectureOfGroup(int LectureOfGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT Id from CourseSemester
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroups.CourseSemesterId=CourseSemester.Id
                                              WHERE LecturesOfGroups.Id={0}", LectureOfGroupID);
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

        public static List<CourseSemester> GetCoursesOfLastSemesterForSpecificDeparmentAndSpecificTerm(int DepartmentID,int Term)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT CourseSemester.Id AS CourseSemesterID,* FROM CourseSemester
                                              INNER JOIN Courses ON Courses.Id=CourseSemester.CourseId
                                              INNER JOIN Semesters ON CourseSemester.SemesterId=Semesters.Id

                                              WHERE Courses.DepartmentId={0} AND Courses.Term={1} 
                                              AND Semesters.Id IN (SELECT MAX(Id) FROM Semesters)", DepartmentID, Term);
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
            List<CourseSemester> CoursesList =
                (from item in myEnumerable
                 select new CourseSemester
                 {
                     Id = item.Field<int>("CourseSemesterID"),
                     CourseName = item.Field<string>("CourseName"),
                     CourseId = item.Field<int>("CourseId"),
                     SemesterId = item.Field<int>("SemesterId"),
                     NumberOfSectionsPerGroup = item.Field<int>("NumberOfSectionsPerGroup"),
                     NumberOfGroups = item.Field<int>("NumberOfGroups"),
                     CreditHours = item.Field<int>("CreditHours")
                 }).ToList();
            return CoursesList;
        }

        public static CourseSemester GetCourseSemesterByID(int CourseSemesterID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM CourseSemester WHERE CourseSemester.Id={0};", CourseSemesterID);
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
            CourseSemester ValueRequired=new CourseSemester
            {
                Id = dt.Rows[0].Field<int>("Id"),
                CourseId = dt.Rows[0].Field<int>("CourseId"),
                SemesterId= dt.Rows[0].Field<int>("SemesterId"),
                NumberOfSectionsPerGroup=dt.Rows[0].Field<int>("NumberOfSectionsPerGroup"),
                NumberOfGroups= dt.Rows[0].Field<int>("NumberOfGroups")
            };
            return ValueRequired;
        }


        /// <summary>
        /// remove course from semester and return ListOfOne course removed
        /// </summary>
        /// <param name="CourseSemesterID"></param>
        /// <returns></returns>
        public static List<Course> RemoveCourseFromSemester(int CourseSemesterID)
        {
            int CourseIDremoved;
            List<Course> ListOfOneCourse;
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form2 = string.Format("SELECT CourseSemester.CourseId FROM CourseSemester WHERE CourseSemester.Id={0};", CourseSemesterID);
                using (SqlCommand cmd = new SqlCommand(form2))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        object ob = cmd.ExecuteScalar();
                        CourseIDremoved = int.Parse(string.Format("{0}", ob));
                    }
                }
                ListOfOneCourse = Course.GetCourseByID(CourseIDremoved);


                string form = string.Format("DELETE FROM CourseSemester WHERE CourseSemester.Id={0};", CourseSemesterID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return ListOfOneCourse;
        }


        public static bool CheckCourseExistInLastSemester(int CourseID)
        {
            DataTable dt = new DataTable();
            int LastSemesterId;
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
                    LastSemesterId = int.Parse(dt.Rows[0][0].ToString());
                }
                DataTable NewDT = new DataTable();
                string form2 = string.Format("SELECT * FROM CourseSemester WHERE CourseSemester.CourseId = {0} AND CourseSemester.SemesterId = {1}; ", CourseID, LastSemesterId);
                using (SqlCommand cmd = new SqlCommand(form2))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter Newadabter = new SqlDataAdapter(cmd))
                    {
                        cmd.ExecuteNonQuery();
                        Newadabter.Fill(NewDT);
                        if (NewDT.Rows.Count > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// add new course to semester and return the id of last (coursesemester)inserted  
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="NumberOfLectures"></param>
        /// <param name="NumberOfLabs"></param>
        /// <param name="NumberOfSections"></param>
        /// <param name="NumberOfGroups"></param>
        /// <returns></returns>
        public static int AddCourseToLastSemester(int CourseId, int NumberOfGroups,int NumberOfSectionsPerGroup)
        {
            DataTable dt = new DataTable();
            int LastSemesterId;
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
                    LastSemesterId = int.Parse(dt.Rows[0][0].ToString());
                }


                string form2 = string.Format(@"INSERT INTO CourseSemester (CourseId,SemesterId,NumberOfGroups,NumberOfSectionsPerGroup) VALUES ({0},{1},{2},{3})
                                               SELECT SCOPE_IDENTITY() AS ID", CourseId, LastSemesterId, NumberOfGroups, NumberOfSectionsPerGroup);
                using (SqlCommand cmd = new SqlCommand(form2))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        object ob=cmd.ExecuteScalar();
                        return int.Parse(string.Format("{0}", ob));
                    }
                }
            }
        }

        public static List<CourseSemester> GetCoursesOfSemesterByDepartmentAndTerm(int DepartmentID, int Term)
        {
            int LastSemesterID = Semester.GetLastSemesterID();

            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT CourseSemester.Id AS CourseSemesterID,* FROM Courses 
                                              INNER JOIN CourseSemester ON Courses.Id=CourseSemester.CourseId
                                              WHERE Courses.Term={0} AND Courses.DepartmentId={1} AND CourseSemester.SemesterId={2};", Term, DepartmentID, LastSemesterID);
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
            List<CourseSemester> CoursesList =
                (from item in myEnumerable
                 select new CourseSemester
                 {
                     Id = item.Field<int>("CourseSemesterID"),
                     CourseName = item.Field<string>("CourseName"),
                     CourseId = item.Field<int>("CourseId"),
                     SemesterId = item.Field<int>("SemesterId"),
                     NumberOfSectionsPerGroup=item.Field<int>("NumberOfSectionsPerGroup"),
                     NumberOfGroups = item.Field<int>("NumberOfGroups"),
                     NumberOfLectures=item.Field<int>("NumberOfLectures"),
                     NumberOfSections=item.Field<int>("NumberOfSections"),
                     NumberOfLabs=item.Field<int>("NumberOfLabs")
                 }).ToList();
            return CoursesList;

        }


    }
}
