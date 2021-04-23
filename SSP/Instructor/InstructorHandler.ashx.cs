using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Newtonsoft.Json;

namespace SSP.Instructor
{
    /// <summary>
    /// Summary description for InstructorHandler
    /// </summary>
    public class InstructorHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request.Form["Method"])
            {
                case "GetStudentsOfPreviousCourse":
                    GetStudentsOfPreviousCourse(context);
                    break;
                case "InsertGrade":
                    InsertGrade(context);
                    break;
                case "GetStudents":
                    GetStudents(context);
                    break;
                case "GetCourseInfo":
                    GetCourseInfo(context);
                    break;
            }
        }
        public void GetStudentsOfPreviousCourse(HttpContext context)
        {
            int LectureOfGroupID = int.Parse(context.Request.Form["LectureOfGroupID"].ToString());
            List<LecturesOfgroupsStudents> RequiredList = LecturesOfgroupsStudents.GetStudentsOfLectureOfGroup(LectureOfGroupID);

            string json = JsonConvert.SerializeObject(RequiredList, Formatting.Indented);
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void InsertGrade(HttpContext context)
        {
            int StudentID = int.Parse(context.Request.Form["StudentID"].ToString());
            int CourseID = int.Parse(context.Request.Form["CourseID"].ToString());
            int Grade = int.Parse(context.Request.Form["Grade"].ToString());
            StudentCourseSemester.InsertGrade(StudentID, CourseID, Grade);
        }

        public void GetStudents(HttpContext context)
        {
            int CourseID = int.Parse(context.Request.Form["CourseID"].ToString());
            int DoctorID = int.Parse(context.Request.Form["DoctorID"].ToString());
            int GroupNumber = int.Parse(context.Request.Form["GroupNumber"].ToString());

            List<Student> AllStudents = Student.GetStudetsRegisterdInCourseUnderSpecificDoctor(DoctorID, CourseID, GroupNumber);
            string json = JsonConvert.SerializeObject(AllStudents, Formatting.Indented);
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public void GetCourseInfo(HttpContext context)
        {
            int CourseID = int.Parse(context.Request.Form["CourseID"].ToString());
            List<Course> ListRequired = Course.GetCourseByID(CourseID);
            Course RequiredCourse = ListRequired[0];

            string json = JsonConvert.SerializeObject(RequiredCourse, Formatting.Indented);
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