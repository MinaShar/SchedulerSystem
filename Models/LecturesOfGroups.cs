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
    public class LecturesOfGroups
    {
        public int Id { get; set; }
        public int CourseSemesterId { get; set; }
        public int GroupNumber { get; set; }
        public int PeriodDay { get; set; }
        public int PeriodNumber { get; set; }
        public string Place { get; set; }
        public int PeriodType { get; set; }
        public int Capacity { get; set; }
        public int SectionNumber { get; set; }


        public string CourseName { get; set; }


        public static int GetAllCapacityOfLectureOfGroup(int LectureOFGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM LecturesOfGroups WHERE Id={0}", LectureOFGroupID);
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
            DataRow row = dt.Rows[0];
            return int.Parse(row["Capacity"].ToString());
        }
        public static int GetCourseIDEquivelentToLectureOfGroupID(int LectureOFGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT Courses.Id AS CourseID,* FROM LecturesOfGroups
                                              INNER JOIN CourseSemester ON LecturesOfGroups.CourseSemesterId=CourseSemester.Id
                                              INNER JOIN Courses ON Courses.Id=CourseSemester.CourseId
                                              WHERE LecturesOfGroups.Id={0}", LectureOFGroupID);
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
            DataRow row = dt.Rows[0];
            return int.Parse(row["CourseID"].ToString());
        }

        public static int GetGroupNumberTheStudentAlreadyRegisteredIN(int StudentID,int CourseID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT LecturesOfGroups.GroupNumber FROM StudentCourseSemester
                                              INNER JOIN LecturesOfGroupsStudents ON StudentCourseSemester.Id=LecturesOfGroupsStudents.StudentCourseSemesterId
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroups.Id=LecturesOfGroupsStudents.LecturesOfGroupsId
                                              WHERE StudentCourseSemester.StudentId={0} AND StudentCourseSemester.CourseId={1} 
                                              AND StudentCourseSemester.SemesterId IN (SELECT MAX(Id) FROM Semesters)", StudentID, CourseID);
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

        public static int GetGroupNumberOfLectureOfGroup(int LectureOfGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT GroupNumber FROM LecturesOfGroups WHERE LecturesOfGroups.Id={0}", LectureOfGroupID);
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

        public static List<LecturesOfGroups> GetAllLecturesForSpecificCourseSemester(int CourseSemesterID)
        {
            int LastSemesterID = Semester.GetLastSemesterID();
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM LecturesOfGroups
                                              WHERE CourseSemesterId={0}
                                              ORDER BY GroupNumber ASC ,SectionNumber ASC,PeriodType ASC", CourseSemesterID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        adabter.Fill(dt);
                    }
                }

                var myEnumerable = dt.AsEnumerable();
                List<LecturesOfGroups> AllLecturesOfGroup =
                    (from item in myEnumerable
                     select new LecturesOfGroups
                     {
                         Id = item.Field<int>("Id"),
                         CourseSemesterId = item.Field<int>("CourseSemesterId"),
                         GroupNumber = item.Field<int>("GroupNumber"),
                         PeriodDay = item.Field<int>("PeriodDay"),
                         PeriodNumber = item.Field<int>("PeriodNumber"),
                         Place = item.Field<string>("Place"),
                         PeriodType = item.Field<int>("PeriodType"),
                         SectionNumber=item.Field<int>("SectionNumber"),
                         Capacity = item.Field<int>("Capacity")
                     }).ToList();
                return AllLecturesOfGroup;
            }
        }
        
        
        /// <summary>
        /// get the doctor time table
        /// </summary>
        /// <param name="DoctorID"></param>
        /// <returns></returns>
        public static List<LecturesOfGroups> GetDoctorTimeTable(int DoctorID)
        {
            int LastSemesterID = Semester.GetLastSemesterID();
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM LecturesOfGroups

                                              INNER JOIN CourseSemester ON LecturesOfGroups.CourseSemesterId=CourseSemester.Id
                                              INNER JOIN Semesters ON CourseSemester.SemesterId=Semesters.Id AND Semesters.Id={0}
                                              INNER JOIN Courses ON Courses.Id=CourseSemester.CourseId

                                              WHERE LecturesOfGroups.Id IN
                                              (
                                                SELECT LecturesOfGroupsId
                                                FROM LecturesOfGroupsDoctors
                                                WHERE DoctorId = {1}
                                              )", LastSemesterID, DoctorID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        adabter.Fill(dt);
                    }
                }

                var myEnumerable = dt.AsEnumerable();
                List<LecturesOfGroups> AllLecturesOfGroup =
                    (from item in myEnumerable
                     select new LecturesOfGroups
                     {
                         Id = item.Field<int>("Id"),
                         CourseSemesterId = item.Field<int>("CourseSemesterId"),
                         GroupNumber = item.Field<int>("GroupNumber"),
                         PeriodDay = item.Field<int>("PeriodDay"),
                         PeriodNumber = item.Field<int>("PeriodNumber"),
                         Place = item.Field<string>("Place"),
                         PeriodType = item.Field<int>("PeriodType"),
                         SectionNumber=item.Field<int>("SectionNumber"),
                         CourseName=item.Field<string>("CourseName")
                     }).ToList();
                return AllLecturesOfGroup;
            }

        }


        public static void RemoveLectureByID(int LectureID)
        {
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("DELETE FROM LecturesOfGroups WHERE Id={0};", LectureID);
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


        public static List<LecturesOfGroups> GetLecturesOfGroup(int CourseSemesterID, int GroupNumber)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM LecturesOfGroups WHERE CourseSemesterId={0} AND GroupNumber={1};", CourseSemesterID, GroupNumber);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        adabter.Fill(dt);
                    }
                }

                var myEnumerable = dt.AsEnumerable();
                List<LecturesOfGroups> AllLecturesOfGroup =
                    (from item in myEnumerable
                     select new LecturesOfGroups
                     {
                         Id = item.Field<int>("Id"),
                         CourseSemesterId = item.Field<int>("CourseSemesterId"),
                         GroupNumber = item.Field<int>("GroupNumber"),
                         PeriodDay = item.Field<int>("PeriodDay"),
                         PeriodNumber = item.Field<int>("PeriodNumber"),
                         PeriodType = item.Field<int>("PeriodType"),
                         SectionNumber=item.Field<int>("SectionNumber")
                     }).ToList();
                return AllLecturesOfGroup;
            }
        }

        /// <summary>
        /// Insert New Lecture to group and return its ID 
        /// </summary>
        /// <param name="CourseSemesterId"></param>
        /// <param name="GroupNumber"></param>
        /// <param name="PeriodDay"></param>
        /// <param name="PeriodNumber"></param>
        /// <returns></returns>
        public static int AddLectureToGroup(int CourseSemesterId, int GroupNumber, int PeriodDay, int PeriodNumber, string Place, int PeriodType,int Capacity,int SectionNumber)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"INSERT INTO LecturesOfGroups (CourseSemesterId,GroupNumber,PeriodDay,PeriodNumber,Place,PeriodType,Capacity,SectionNumber) VALUES({0}, {1}, {2}, {3},'{4}',{5},{6},{7})
                                              SELECT SCOPE_IDENTITY() AS ID", CourseSemesterId, GroupNumber, PeriodDay, PeriodNumber, Place, PeriodType,Capacity,SectionNumber);
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

        public static bool CheckThereIsRoomForNewPeriod(int CourseSemesterID, int GroupNumber, int PeriodDay, int PeriodNumber, string Place, int PeriodType,int SectionNumber)
        {
            //CourseSemester course_required = CourseSemester.GetCourseSemesterByID(CourseSemesterID);
            Course course_required = Course.GetCourseByCourseSemesterID(CourseSemesterID);
            int NumberOfPeriodsRegisteredTillNow;
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT COUNT(CourseSemesterId) FROM LecturesOfGroups WHERE CourseSemesterId={0} AND GroupNumber={1} AND PeriodType={2} AND SectionNumber={3} GROUP BY CourseSemesterId;", CourseSemesterID, GroupNumber, PeriodType, SectionNumber);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        object ob = cmd.ExecuteScalar();
                        if (ob == null)
                        {
                            NumberOfPeriodsRegisteredTillNow = 0;
                        }
                        else
                        {
                            NumberOfPeriodsRegisteredTillNow = int.Parse(string.Format("{0}", ob));
                        }
                    }
                }
            }
            if (PeriodType == 1)
            {///lecture
                if (course_required.NumberOfLectures > NumberOfPeriodsRegisteredTillNow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (PeriodType == 2)
            {///section
                if (course_required.NumberOfSections > NumberOfPeriodsRegisteredTillNow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {///lap
                if (course_required.NumberOfLabs > NumberOfPeriodsRegisteredTillNow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }


        public static LecturesOfGroups GetLectureOfPeriod(int CourseSemesterID, int GroupNumber, int Day, int Period)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM LecturesOfGroups WHERE CourseSemesterId={0} AND GroupNumber={1} AND PeriodDay={2} AND PeriodNumber={3};", CourseSemesterID, GroupNumber, Day, Period);
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
                return new LecturesOfGroups
                {
                    Id = int.Parse(dt.Rows[0]["Id"].ToString()),
                    CourseSemesterId = int.Parse(dt.Rows[0]["CourseSemesterId"].ToString()),
                    GroupNumber = int.Parse(dt.Rows[0]["GroupNumber"].ToString()),
                    PeriodDay = int.Parse(dt.Rows[0]["PeriodDay"].ToString()),
                    PeriodNumber = int.Parse(dt.Rows[0]["PeriodNumber"].ToString()),
                    Place = dt.Rows[0]["Place"].ToString(),
                    PeriodType = int.Parse(dt.Rows[0]["PeriodType"].ToString()),
                    Capacity = dt.Rows[0]["Capacity"].ToString()!=null ? int.Parse(dt.Rows[0]["Capacity"].ToString()) : 0,
                    SectionNumber=int.Parse(dt.Rows[0]["SectionNumber"].ToString())
                };
            }
            else
            {
                return null;
            }
        }

        public static LecturesOfGroups GetLectureOfGroupByID(int LectureOfGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM LecturesOfGroups WHERE Id={0};", LectureOfGroupID);
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
                return new LecturesOfGroups
                {
                    Id = int.Parse(dt.Rows[0]["Id"].ToString()),
                    CourseSemesterId = int.Parse(dt.Rows[0]["CourseSemesterId"].ToString()),
                    GroupNumber = int.Parse(dt.Rows[0]["GroupNumber"].ToString()),
                    PeriodDay = int.Parse(dt.Rows[0]["PeriodDay"].ToString()),
                    PeriodNumber = int.Parse(dt.Rows[0]["PeriodNumber"].ToString()),
                    Place = dt.Rows[0]["Place"].ToString(),
                    PeriodType = int.Parse(dt.Rows[0]["PeriodType"].ToString()),
                    Capacity = dt.Rows[0]["Capacity"].ToString() != null ? int.Parse(dt.Rows[0]["Capacity"].ToString()) : 0,
                    SectionNumber = int.Parse(dt.Rows[0]["SectionNumber"].ToString())
                };
            }
            else
            {
                return null;
            }
        }

        public static List<LecturesOfGroups> GetCoursesOfDoctorAtGivenSemester(int DoctorID,int SemesterID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT LecturesOfGroups.Id AS LectureOfGroupID,* FROM LecturesOfGroupsDoctors
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroups.Id=LecturesOfGroupsDoctors.LecturesOfGroupsId
                                              INNER JOIN CourseSemester ON CourseSemester.Id=LecturesOfGroups.CourseSemesterId
                                              INNER JOIN Semesters ON Semesters.Id=CourseSemester.SemesterId
                                              INNER JOIN Courses ON Courses.Id=CourseSemester.CourseId
                                              WHERE LecturesOfGroupsDoctors.DoctorId={0}
                                              AND LecturesOfGroups.PeriodType=1
                                              AND Semesters.Id={1}", DoctorID, SemesterID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        adabter.Fill(dt);
                    }
                }

                var myEnumerable = dt.AsEnumerable();
                List<LecturesOfGroups> AllLecturesOfGroup =
                    (from item in myEnumerable
                     select new LecturesOfGroups
                     {
                         Id = item.Field<int>("LectureOfGroupID"),
                         CourseSemesterId = item.Field<int>("CourseSemesterId"),
                         GroupNumber = item.Field<int>("GroupNumber"),
                         PeriodDay = item.Field<int>("PeriodDay"),
                         PeriodNumber = item.Field<int>("PeriodNumber"),
                         PeriodType = item.Field<int>("PeriodType"),
                         SectionNumber = item.Field<int>("SectionNumber"),
                         CourseName = item.Field<string>("CourseName")
                     }).ToList();
                return AllLecturesOfGroup;
            }
        }

    }
}
