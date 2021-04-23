using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.CStudent
{
    public partial class Register : System.Web.UI.Page
    {
        public int CurrentStudentID;
        public int CurrentStudentDepartmentID;

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentStudentID = int.Parse(Request.QueryString["ID"].ToString());
            Student CurrentS = Student.GetStudentById(CurrentStudentID);
            CurrentStudentDepartmentID = CurrentS.DepartmentId;
            CurrentStudentIDTextBox.Value = string.Format("{0}", CurrentStudentID);

            AllCoursesTerm1.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(1);
            AllCoursesTerm1.DataBind();

            AllCoursesTerm2.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(2);
            AllCoursesTerm2.DataBind();

            AllCoursesTerm3.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(3);
            AllCoursesTerm3.DataBind();

            AllCoursesTerm4.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(4);
            AllCoursesTerm4.DataBind();

            AllCoursesTerm5.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(5);
            AllCoursesTerm5.DataBind();

            AllCoursesTerm6.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(6);
            AllCoursesTerm6.DataBind();

            AllCoursesTerm7.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(7);
            AllCoursesTerm7.DataBind();

            AllCoursesTerm8.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(8);
            AllCoursesTerm8.DataBind();

            AllCoursesTerm9.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(9);
            AllCoursesTerm9.DataBind();

            AllCoursesTerm10.DataSource = GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(10);
            AllCoursesTerm10.DataBind();
        }

        public List<CourseSemester> GetCoursesOfLastSemesterForSpecificDepartmentAndSpecificTerm(int Term)
        {
            return CourseSemester.GetCoursesOfLastSemesterForSpecificDeparmentAndSpecificTerm(CurrentStudentDepartmentID, Term);
        }

        public List<LecturesOfGroups> GetAllPeriodsForThisCourseSemester(int CourseSemesterID)
        {
            return LecturesOfGroups.GetAllLecturesForSpecificCourseSemester(CourseSemesterID);
        }
        public string GetSectionNumber(int x)
        {
            if (x > 0)
            {
                return string.Format("S. {0}", x);
            }
            else
            {
                return "";
            }
        }
        public string GetPeriodType(int PeriodType)
        {
            if (PeriodType == 1)
            {
                return "Lecture";
            }
            else if (PeriodType == 2)
            {
                return "Section";
            }
            else if (PeriodType == 3)
            {
                return "Lab";
            }
            else
            {
                return null;
            }
        }

        public List<Doctor> GetDoctorsOfPeriod(int LectureOfGroupID)
        {
            return LecturesOfGroupsDoctors.GetDoctorsOfLecturePeriod(LectureOfGroupID);
        }

        public string GetTheProberForm(int LectureOfGroupID)
        {
            int IsRegistered = LecturesOfgroupsStudents.CheckIfStudentALreadyRegisteredInLectureOfGroup(CurrentStudentID, LectureOfGroupID);
            if (IsRegistered != -1 )
            {
                return string.Format(@"<form class=""RemoveRegisteration"">
                                       <input type=""hidden"" name=""CurrentStudentID"" value=""{0}"" />
                                       <input type=""hidden"" name=""LectureOfGroupStudentID"" value=""{1}"" />
                                       <input type=""submit"" onclick=""RemoveRegisteration(this.form);return false;"" value=""Remove"" />
                                       </form>", CurrentStudentID, IsRegistered);
            }
            else
            {
                return string.Format(@"<form class=""RegisterInPeriod"">
                                       <input type=""hidden"" name=""CurrentStudentID"" value=""{0}"" />
                                       <input type=""hidden"" name=""LectureOfGroupID"" value=""{1}"" />
                                       <input type=""submit"" onclick=""RegisterNewPeriod(this.form);return false;"" value=""Add"" />
                                       </form>", CurrentStudentID, LectureOfGroupID);
            }
        }
        public int GetTheRemainingCapacityForPeriod(int LectureOfgroupID)
        {
            int TotalCapacity = LecturesOfGroups.GetAllCapacityOfLectureOfGroup(LectureOfgroupID);
            int StudentsAlreadyRegistered = LecturesOfgroupsStudents.GetNumberOfStudentsRegisteredInLectureOfGroup(LectureOfgroupID);
            return TotalCapacity - StudentsAlreadyRegistered;
        }
        public int GetCurrentStudentID()
        {
            return CurrentStudentID;
        }

    }
}