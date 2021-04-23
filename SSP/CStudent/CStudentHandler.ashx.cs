using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Newtonsoft.Json;

namespace SSP.CStudent
{
    /// <summary>
    /// Summary description for CStudentHandler
    /// </summary>
    public class CStudentHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request.Form["Method"])
            {
                case "SaveRegistration":
                    SaveRegistration(context);
                    break;
                case "GetCurrentSemesterGrade":
                    GetCurrentSemesterGrade(context);
                    break;
                case "GetTheRemainingCapacityForPeriod":
                    GetTheRemainingCapacityForPeriod(context);
                    break;
                case "RemoveRegistration":
                    RemoveRegistration(context);
                    break;
                case "GetStudentTimeTable":
                    GetStudentTimeTable(context);
                    break;
                case "RegisterPeriod":
                    RegisterPeriod(context);
                    break;
            }
        }

        public void SaveRegistration(HttpContext context)
        {
            int StudentID = int.Parse(context.Request.Form["StudentID"].ToString());
            ResponseToFrontEnd x = StudentCourseSemester.SaveRegistration(StudentID);

            string json = JsonConvert.SerializeObject(x, Formatting.Indented);
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void GetCurrentSemesterGrade(HttpContext context)
        {
            int CurrentStudentID = int.Parse(context.Request.Form["CurrentStudentID"].ToString());
            List<StudentCourseSemester> ListRequired = StudentCourseSemester.GetCurrentSemesterGrade(CurrentStudentID);

            string json = JsonConvert.SerializeObject(ListRequired, Formatting.Indented);
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }
        public void GetTheRemainingCapacityForPeriod(HttpContext context)
        {
            int LectureOfGroupID = int.Parse(context.Request.Form["LectureOfGroupID"].ToString());
            int AllCapacity = LecturesOfGroups.GetAllCapacityOfLectureOfGroup(LectureOfGroupID);
            int RegisteredStudentInThePeriod = LecturesOfgroupsStudents.GetNumberOfStudentsRegisteredInLectureOfGroup(LectureOfGroupID);
            int RemainingPlaces = AllCapacity - RegisteredStudentInThePeriod;
            string json = JsonConvert.SerializeObject(RemainingPlaces, Formatting.Indented);
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void RemoveRegistration(HttpContext context)
        {
            int LectureOfGroupStudentID = int.Parse(context.Request.Form["LectureOfGroupStudentID"].ToString());
            int LectureOfGroupID = LecturesOfgroupsStudents.RemoveRegistration(LectureOfGroupStudentID);
            string json = JsonConvert.SerializeObject(LectureOfGroupID, Formatting.Indented);
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void GetStudentTimeTable(HttpContext context)
        {
            int StudentID = int.Parse(context.Request.Form["CurrentStudentID"].ToString());
            List<LecturesOfgroupsStudents> TimeTable = LecturesOfgroupsStudents.GetStudentTimeTable(StudentID);
            string json = JsonConvert.SerializeObject(TimeTable, Formatting.Indented);
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void RegisterPeriod(HttpContext context)
        {
            int StudentID = int.Parse(context.Request.Form["CurrentStudentID"].ToString());
            int LectureOfGroupID = int.Parse(context.Request.Form["LectureOfGroupID"].ToString());
            ResponseToFrontEnd x = LecturesOfgroupsStudents.RegisterPeriod(LectureOfGroupID, StudentID);
            string json = JsonConvert.SerializeObject(x, Formatting.Indented);
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