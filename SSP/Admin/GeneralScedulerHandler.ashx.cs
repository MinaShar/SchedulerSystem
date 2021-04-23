using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Newtonsoft.Json;


namespace SSP.Admin
{
    /// <summary>
    /// Summary description for GeneralScedulerHandler
    /// </summary>
    public class GeneralScedulerHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            switch(context.Request.Form["Method"])
            {
                case "CheckSectionNumberInRange":
                    CheckSectionNumberInRange(context);
                    break;
                case "CloseSemester":
                    CloseSemester(context);
                    break;
                case "OpenRegestration":
                    OpenRegestration(context);
                    break;
                case "CheckDoctorOverLap":
                    CheckDoctorOverLap(context);
                    break;
                case "GetAllDoctors":
                    GetAllDoctors(context);
                    break;
                case "GetAllPrograms":
                    GetAllPrograms(context);
                    break;
                case "GetCourseInfo":
                    GetCourseInfo(context);
                    break;
                case "AddCourseToSemester":
                    AddCourseToSemester(context);
                    break;
                case "RemoveCourseFromSemester":
                    RemoveCourseFromSemester(context);
                    break;
                case "GetCourseSemesterInfo":
                    GetCourseSemesterInfo(context);
                    break;
                case "AddLectureToGroup":
                    AddLectureToGroup(context);
                    break;
                case "AssignDoctorToLecture":
                    AssignDoctorToLecture(context);
                    break;
                case "GetAllLecturesOfGroupOfCourseSemester":
                    GetAllLecturesOfGroupOfCourseSemester(context);
                    break;
                case "GetPeriodInfo":
                    GetPeriodInfo(context);
                    break;
                case "RemoveLectureByID":
                    RemoveLectureByID(context);
                    break;
            }
        }
        public void CheckSectionNumberInRange(HttpContext context)
        {
            int CourseSemesterID= int.Parse(context.Request.Form["CourseSemesterID"].ToString());
            int SectionNumberEntered = int.Parse(context.Request.Form["SectionNumberEntered"].ToString());
            if (CourseSemester.CheckSectionNumberInRange(CourseSemesterID, SectionNumberEntered) == true)
            {
                int x = 1;
                string json = JsonConvert.SerializeObject(x, Formatting.Indented);
                context.Response.ContentType = "text/plain";
                context.Response.Write(json);
            }
            else
            {
                int x = -1;
                string json = JsonConvert.SerializeObject(x, Formatting.Indented);
                context.Response.ContentType = "text/plain";
                context.Response.Write(json);
            }
        }

        public void CloseSemester(HttpContext context)
        {
            bool flag = bool.Parse(context.Request.Form["flag"].ToString());
            Semester.CloseSemester(flag);
        }

        public void OpenRegestration(HttpContext context)
        {
            bool flag = bool.Parse(context.Request.Form["flag"].ToString());
            Semester.OpenRegestration(flag);
        }

        public void CheckDoctorOverLap(HttpContext context)
        {
            int DoctorID = int.Parse(context.Request.Form["DoctorID"].ToString());
            int PeriodDay = int.Parse(context.Request.Form["PeriodDay"].ToString());
            int PeriodNumber = int.Parse(context.Request.Form["PeriodNumber"].ToString());
            if (Doctor.CheckDoctorOverLap(DoctorID, PeriodDay, PeriodNumber) == false)
            {
                int x = -1;
                string json = JsonConvert.SerializeObject(x, Formatting.Indented);
                context.Response.ContentType = "text/plain";
                context.Response.Write(json);
            }
            else
            {
                int x = 1;
                string json = JsonConvert.SerializeObject(x, Formatting.Indented);
                context.Response.ContentType = "text/plain";
                context.Response.Write(json);
            }
        }

        public void RemoveLectureByID(HttpContext context)
        {
            int LectureID = int.Parse(context.Request.Form["LectureID"].ToString());
            LecturesOfGroups.RemoveLectureByID(LectureID);
        }

        public void GetPeriodInfo(HttpContext context)
        {
            int CourseSemesterID = int.Parse(context.Request.Form["CourseSemesterID"].ToString());
            int GroupNumber = int.Parse(context.Request.Form["GroupNumber"].ToString());
            int Day = int.Parse(context.Request.Form["Day"].ToString());
            int Period = int.Parse(context.Request.Form["Period"].ToString());
            PeriodInfoToAdmin DataRequired = PeriodInfoToAdmin.GetPeriodInfo(CourseSemesterID, GroupNumber, Day, Period);

            string json = JsonConvert.SerializeObject(DataRequired, Formatting.Indented);

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void GetAllLecturesOfGroupOfCourseSemester(HttpContext context)
        {
            int CourseSemesterID = int.Parse(context.Request.Form["CourseSemesterID"].ToString());
            int GroupNumber = int.Parse(context.Request.Form["GroupNumber"].ToString());
            List<LecturesOfGroups> AllLecturesOfGroup = LecturesOfGroups.GetLecturesOfGroup(CourseSemesterID, GroupNumber);

            string json = JsonConvert.SerializeObject(AllLecturesOfGroup, Formatting.Indented);

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);

        }

        public void AssignDoctorToLecture(HttpContext context)
        {
            int LectureOfGroupsID = int.Parse(context.Request.Form["LectureOfGroupsID"].ToString());
            int DoctorID = int.Parse(context.Request.Form["DoctorID"].ToString());

            LecturesOfGroupsDoctors.AssignDoctorToLecture(LectureOfGroupsID, DoctorID);
        }

        public void AddLectureToGroup(HttpContext context)
        {
            int CourseSemesterID = int.Parse(context.Request.Form["CourseSemesterId"].ToString());
            int GroupNumber = int.Parse(context.Request.Form["GroupNumber"].ToString());
            int PeriodDay = int.Parse(context.Request.Form["PeriodDay"].ToString());
            int PeriodNumber = int.Parse(context.Request.Form["PeriodNumber"].ToString());
            string PeriodPlace = context.Request.Form["PeriodPlace"].ToString();
            int PeriodType = int.Parse(context.Request.Form["PeriodType"].ToString());
            int PeriodCapacity = int.Parse(context.Request.Form["PeriodCapacity"].ToString());
            int SectionNumber= int.Parse(context.Request.Form["SectionNumber"].ToString());

            if (LecturesOfGroups.CheckThereIsRoomForNewPeriod(CourseSemesterID, GroupNumber, PeriodDay, PeriodNumber, PeriodPlace, PeriodType,SectionNumber))
            {
                int NewLectureInsertedID = LecturesOfGroups.AddLectureToGroup(CourseSemesterID, GroupNumber, PeriodDay, PeriodNumber, PeriodPlace, PeriodType, PeriodCapacity, SectionNumber);

                string json = JsonConvert.SerializeObject(NewLectureInsertedID, Formatting.Indented);

                context.Response.ContentType = "text/plain";
                context.Response.Write(json);
            }
            else
            {
                int x = -1;
                string json = JsonConvert.SerializeObject(x, Formatting.Indented);
                context.Response.ContentType = "text/plain";
                context.Response.Write(json);
            }
        }

        public void GetCourseSemesterInfo(HttpContext context)
        {
            int CourseSemesterID = int.Parse(context.Request.Form["CourseSemesterId"].ToString());
            CourseSemester RequiredCourseSemester = CourseSemester.GetCourseSemesterByID(CourseSemesterID);

            string json = JsonConvert.SerializeObject(RequiredCourseSemester, Formatting.Indented);

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void RemoveCourseFromSemester(HttpContext context)
        {
            int CourseSemesterID = int.Parse(context.Request.Form["CourseSemesterID"].ToString());
            List<Course> CourseRemoved=CourseSemester.RemoveCourseFromSemester(CourseSemesterID);

            string json = JsonConvert.SerializeObject(CourseRemoved, Formatting.Indented);

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void AddCourseToSemester(HttpContext context)
        {
            int CourseID = int.Parse(context.Request.Form["Id"].ToString());
            int GroupsNumber = int.Parse(context.Request.Form["GroupsNumber"].ToString());
            int SectionsPerGroupNumber = int.Parse(context.Request.Form["SectionsPerGroupNumber"].ToString());
            int LastCourseSemesterID = CourseSemester.AddCourseToLastSemester(CourseID, GroupsNumber, SectionsPerGroupNumber);


            string json = JsonConvert.SerializeObject(LastCourseSemesterID, Formatting.Indented);

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void GetCourseInfo(HttpContext context)
        {
            int CourseId = int.Parse(context.Request.Form["Id"].ToString());
            List<Course> CourseR = Course.GetCourseByID(CourseId);

            string json = JsonConvert.SerializeObject(CourseR, Formatting.Indented);

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }


        public void GetAllDoctors(HttpContext context)
        {
            List<Doctor> AllDoctors = Doctor.GetAll();
            string json = JsonConvert.SerializeObject(AllDoctors, Formatting.Indented);

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void GetAllPrograms(HttpContext context)
        {
            List<Department> AllDepartments = Department.GetAll();
            string json = JsonConvert.SerializeObject(AllDepartments, Formatting.Indented);

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}