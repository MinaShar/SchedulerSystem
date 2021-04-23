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
    public class ResponseToFrontEnd
    {
        /// <summary>
        /// true => LectureOfGroupStudentID ( print time table again ) || false => String(reason)
        /// </summary>
        public bool Flag { get; set; }
        public int LectureOfGroupStudentID { get; set; }
        public string ReasonWhyNot { get; set; }
    }

    public class LecturesOfgroupsStudents
    {
        public int Id { get; set; }
        public int LecturesOfGroupsId { get; set; }
        public int StudentId { get; set; }
        public int StudentCourseSemesterId { get; set; }

        public int GroupNumber { get; set; }
        public int SectionNumber { get; set; }
        public int PeriodDay { get; set; }
        public int PeriodNumber { get; set; }
        public string Place { get; set; }
        public int PeriodType { get; set; }
        public int Capacity { get; set; }
        public string CourseName { get; set; }
        public List<Doctor> DoctorsOfThePeriod { get; set; }
        public string StudentName { get; set; }
        public int Grade { get; set; }

        /// <summary>
        /// return true if student have overlabs 
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="PeriodDay"></param>
        /// <param name="PeriodNumber"></param>
        /// <returns></returns>
        public static bool IsStudentHasOverlab(int StudentID,int PeriodDay,int PeriodNumber)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM LecturesOfGroupsStudents
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroups.Id=LecturesOfGroupsStudents.LecturesOfGroupsId
                                              INNER JOIN CourseSemester ON LecturesOfGroups.CourseSemesterId=CourseSemester.Id
                                              INNER JOIN Semesters ON Semesters.Id=CourseSemester.SemesterId
                                              WHERE LecturesOfGroupsStudents.StudentId={0}
                                              AND Semesters.Id IN (SELECT MAX(Id) FROM Semesters)
                                              AND LecturesOfGroups.PeriodDay={1} AND LecturesOfGroups.PeriodNumber={2}", StudentID, PeriodDay, PeriodNumber);
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


        public static int GetNumberOfStudentsRegisteredInLectureOfGroup(int LectureOfGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM LecturesOfGroupsStudents WHERE LecturesOfGroupsId={0}", LectureOfGroupID);
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

        public static ResponseToFrontEnd RegisterPeriod(int LecturesOfGroupID, int StudentID)
        {
            ///////////////////check that student donot have overlabs in the required period////////////////
            LecturesOfGroups l = LecturesOfGroups.GetLectureOfGroupByID(LecturesOfGroupID);
            if (IsStudentHasOverlab(StudentID, l.PeriodDay, l.PeriodNumber))
            {
                return new ResponseToFrontEnd { Flag = false, ReasonWhyNot = "You have overlabs in this Period" };
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////  

            ////////////////////////////check there is room for new student/////////////////////////////////
            int AllCapacity = LecturesOfGroups.GetAllCapacityOfLectureOfGroup(LecturesOfGroupID);
            int RegisteredStudentInThePeriod = LecturesOfgroupsStudents.GetNumberOfStudentsRegisteredInLectureOfGroup(LecturesOfGroupID);
            int RemainingPlaces = AllCapacity - RegisteredStudentInThePeriod;
            if (RemainingPlaces <= 0)
            {
                return new ResponseToFrontEnd { Flag = false, ReasonWhyNot = "No Room for new student in this period" };
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////Check To Register In The same Group////////////////////////////////
            int CourseID = LecturesOfGroups.GetCourseIDEquivelentToLectureOfGroupID(LecturesOfGroupID);
            if (StudentCourseSemester.IsStudentAlreadyRegisteredInCourseInLastSemester(CourseID, StudentID) == true)
            {
                int GroupNumberTheStudnetAlreadyRegisteredIN = LecturesOfGroups.GetGroupNumberTheStudentAlreadyRegisteredIN(StudentID, CourseID);
                int GroupNumberOfPeriodRequiredToRegisterIN = LecturesOfGroups.GetGroupNumberOfLectureOfGroup(LecturesOfGroupID);
                if (GroupNumberTheStudnetAlreadyRegisteredIN != GroupNumberOfPeriodRequiredToRegisterIN)
                {
                    return new ResponseToFrontEnd { Flag = false, ReasonWhyNot = "You are registered in this course but in another Group" };
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////check that what the student registerd donot exceed the required for the course/////////////////
            List<Course> ListCourseRequiredToRegisterIN = Course.GetCourseByID(CourseID);
            Course CourseRequiredToRegisterIN = ListCourseRequiredToRegisterIN[0];
            LecturesOfGroups LectureOfGroupRequiredToRegisterIN = LecturesOfGroups.GetLectureOfGroupByID(LecturesOfGroupID);
            int LectureOfgroupType = LectureOfGroupRequiredToRegisterIN.PeriodType;
            int NumberOfPeriodsStudentAlreadyRegisteredTillNow = StudentCourseSemester.GetNumberOfThePeriodsThisStudentRegisteredForThisCourse(StudentID, CourseID, LectureOfgroupType);
            if (LectureOfgroupType == 1)
            {
                if (NumberOfPeriodsStudentAlreadyRegisteredTillNow >= CourseRequiredToRegisterIN.NumberOfLectures)
                {
                    return new ResponseToFrontEnd { Flag = false, ReasonWhyNot = "You registered the required Number of Lectures" };
                }
            }
            else if (LectureOfgroupType == 2)
            {
                if (NumberOfPeriodsStudentAlreadyRegisteredTillNow >= CourseRequiredToRegisterIN.NumberOfSections)
                {
                    return new ResponseToFrontEnd { Flag = false, ReasonWhyNot = "You registered the required number of Tutorials" };
                }
            }
            else
            {
                if (NumberOfPeriodsStudentAlreadyRegisteredTillNow >= CourseRequiredToRegisterIN.NumberOfLabs)
                {
                    return new ResponseToFrontEnd { Flag = false, ReasonWhyNot = "You registered the required number of Labs" };
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


            /////////////////////////////////////////////Register the new period/////////////////////////////////////////
            int StudentCourseSemesterID = StudentCourseSemester.RegisterStudentInCourse(LecturesOfGroupID, StudentID);
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"INSERT INTO LecturesOfGroupsStudents (LecturesOfGroupsId,StudentId,StudentCourseSemesterId) VALUES ({0},{1},{2})
                                              SELECT SCOPE_IDENTITY() AS ID", LecturesOfGroupID, StudentID, StudentCourseSemesterID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        object ob = cmd.ExecuteScalar();
                        int InsertedLectureOfGroupStudentID = int.Parse(string.Format("{0}", ob));
                        return new ResponseToFrontEnd { Flag = true, LectureOfGroupStudentID = InsertedLectureOfGroupStudentID };
                    }
                }
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// remove the registration and return the LectureOfGroupID from the record deleted 
        /// </summary>
        /// <param name="LectureOfGroupStudentID"></param>
        /// <returns></returns>
        public static int RemoveRegistration(int LectureOfGroupStudentID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM LecturesOfGroupsStudents WHERE LecturesOfGroupsStudents.Id={0}", LectureOfGroupStudentID);
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
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"DELETE FROM LecturesOfGroupsStudents WHERE LecturesOfGroupsStudents.Id={0}", LectureOfGroupStudentID);
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

            DataRow row = dt.Rows[0];
            int StudnetCourseSemesterID = int.Parse(row[3].ToString());
            CheckIfStudentStillRegisteredAtAnyIntervalOfThisCourse(StudnetCourseSemesterID);
            return int.Parse(row[1].ToString());
        }

        public static void CheckIfStudentStillRegisteredAtAnyIntervalOfThisCourse(int StudentCourseSemesterID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format("SELECT * FROM LecturesOfGroupsStudents WHERE StudentCourseSemesterId={0};", StudentCourseSemesterID);
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
                if (dt.Rows.Count == 0)
                {
                    StudentCourseSemester.DeleterecordByID(StudentCourseSemesterID);
                }

            }
        }



        public static List<LecturesOfgroupsStudents> GetStudentTimeTable(int StudentID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT LecturesOfGroupsStudents.Id AS LecturesOfGroupsStudentID,* FROM LecturesOfGroupsStudents
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroups.Id=LecturesOfGroupsStudents.LecturesOfGroupsId
                                              INNER JOIN CourseSemester ON CourseSemester.Id=LecturesOfGroups.CourseSemesterId
                                              INNER JOIN Semesters ON Semesters.Id=CourseSemester.SemesterId
                                              INNER JOIN Courses ON Courses.Id=CourseSemester.CourseId
                                              WHERE Semesters.Id IN (SELECT MAX(Id) FROM Semesters)
                                              AND LecturesOfGroupsStudents.StudentId={0}", StudentID);
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
                List<LecturesOfgroupsStudents> List =
                    (from item in myEnumerable
                     select new LecturesOfgroupsStudents
                     {
                         Id = item.Field<int>("LecturesOfGroupsStudentID"),
                         LecturesOfGroupsId = item.Field<int>("LecturesOfGroupsId"),
                         StudentId = item.Field<int>("StudentId"),
                         GroupNumber = item.Field<int>("GroupNumber"),
                         SectionNumber = item.Field<int>("SectionNumber"),
                         PeriodDay = item.Field<int>("PeriodDay"),
                         PeriodNumber = item.Field<int>("PeriodNumber"),
                         Place = item.Field<string>("Place"),
                         PeriodType = item.Field<int>("PeriodType"),
                         Capacity = item.Field<int>("Capacity"),
                         CourseName = item.Field<string>("CourseName"),
                         DoctorsOfThePeriod = Doctor.GetDoctorsByLectureOfGroupID(item.Field<int>("LecturesOfGroupsId"))//LecturesOfGroupsDoctors.GetDoctorsOfLecturePeriod(item.Field<int>("LecturesOfGroupsId"))
                     }).ToList();
                return List;
            }
        }

        /// <summary>
        /// return the LectureOfGroupStudentID if the student registered in the period
        /// else return -1
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="LectureOfGroupID"></param>
        /// <returns></returns>
        public static int CheckIfStudentALreadyRegisteredInLectureOfGroup(int StudentID, int LectureOfGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT * FROM LecturesOfGroupsStudents
                                              WHERE LecturesOfGroupsStudents.StudentId={0} AND LecturesOfGroupsStudents.LecturesOfGroupsId={1}", StudentID, LectureOfGroupID);
                using (SqlCommand cmd = new SqlCommand(form))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter adabter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        adabter.Fill(dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        return int.Parse(row["Id"].ToString());
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public static List<LecturesOfgroupsStudents> GetLecturesOfGroupsStudentsByStudentCourseSemesterID(int StudentCourseSemesterID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT LecturesOfGroupsStudents.Id AS LecturesOfGroupsStudentsID,* FROM LecturesOfGroupsStudents
                                              INNER JOIN LecturesOfGroups ON LecturesOfGroupsStudents.LecturesOfGroupsId=LecturesOfGroups.Id
                                              WHERE StudentCourseSemesterId={0}", StudentCourseSemesterID);
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
                List<LecturesOfgroupsStudents> UsersList =
                    (from item in myEnumerable
                     select new LecturesOfgroupsStudents
                     {
                         Id = item.Field<int>("LecturesOfGroupsStudentsID"),
                         LecturesOfGroupsId = item.Field<int>("LecturesOfGroupsId"),
                         StudentId = item.Field<int>("StudentId"),
                         StudentCourseSemesterId = item.Field<int>("StudentCourseSemesterId"),
                         PeriodType = item.Field<int>("PeriodType")
                     }).ToList();
                return UsersList;
            }
        }

        public static List<LecturesOfgroupsStudents> GetStudentsOfLectureOfGroup(int LectureOfGroupID)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=.;Initial Catalog=SSP;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string form = string.Format(@"SELECT Students.Id AS StudentID,* FROM LecturesOfGroupsStudents
                                              INNER JOIN Students ON Students.Id=LecturesOfGroupsStudents.StudentId
                                              INNER JOIN StudentCourseSemester ON StudentCourseSemester.Id=LecturesOfGroupsStudents.StudentCourseSemesterId 
                                              WHERE LecturesOfGroupsStudents.LecturesOfGroupsId={0}", LectureOfGroupID);
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
                List<LecturesOfgroupsStudents> UsersList =
                    (from item in myEnumerable
                     select new LecturesOfgroupsStudents
                     {
                         StudentId = item.Field<int>("StudentID"),
                         StudentName = item.Field<string>("Name"),
                         Grade = item["Grade"] == DBNull.Value ? -1 : item.Field<int>("Grade")
                     }).ToList();
                return UsersList;
            }
        }

    }
}
