using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.Admin
{
    public partial class CoursesOfSememester : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int DepartmentId = int.Parse(Request.QueryString["ProgramId"].ToString());

            AllCoursesTerm1.DataSource = Course.GetCoursesByTermAndDepartment(1, DepartmentId);
            AllCoursesTerm1.DataBind();

            AllCoursesTerm2.DataSource = Course.GetCoursesByTermAndDepartment(2, DepartmentId);
            AllCoursesTerm2.DataBind();

            AllCoursesTerm3.DataSource = Course.GetCoursesByTermAndDepartment(3, DepartmentId);
            AllCoursesTerm3.DataBind();

            AllCoursesTerm4.DataSource = Course.GetCoursesByTermAndDepartment(4, DepartmentId);
            AllCoursesTerm4.DataBind();

            AllCoursesTerm5.DataSource = Course.GetCoursesByTermAndDepartment(5, DepartmentId);
            AllCoursesTerm5.DataBind();

            AllCoursesTerm6.DataSource = Course.GetCoursesByTermAndDepartment(6, DepartmentId);
            AllCoursesTerm6.DataBind();

            AllCoursesTerm7.DataSource = Course.GetCoursesByTermAndDepartment(7, DepartmentId);
            AllCoursesTerm7.DataBind();

            AllCoursesTerm8.DataSource = Course.GetCoursesByTermAndDepartment(8, DepartmentId);
            AllCoursesTerm8.DataBind();

            AllCoursesTerm9.DataSource = Course.GetCoursesByTermAndDepartment(9, DepartmentId);
            AllCoursesTerm9.DataBind();

            AllCoursesTerm10.DataSource = Course.GetCoursesByTermAndDepartment(10, DepartmentId);
            AllCoursesTerm10.DataBind();


            ///// NOW WITH THE SELECTED
            SelectedTerm1.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 1);
            SelectedTerm1.DataBind();

            SelectedTerm2.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 2);
            SelectedTerm2.DataBind();

            SelectedTerm3.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 3);
            SelectedTerm3.DataBind();

            SelectedTerm4.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 4);
            SelectedTerm4.DataBind();

            SelectedTerm5.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 5);
            SelectedTerm5.DataBind();

            SelectedTerm6.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 6);
            SelectedTerm6.DataBind();

            SelectedTerm7.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 7);
            SelectedTerm7.DataBind();

            SelectedTerm8.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 8);
            SelectedTerm8.DataBind();

            SelectedTerm9.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 9);
            SelectedTerm9.DataBind();

            SelectedTerm10.DataSource = CourseSemester.GetCoursesOfSemesterByDepartmentAndTerm(DepartmentId, 10);
            SelectedTerm10.DataBind();


        }

        public string AddLabelCalledADDED()
        {
            return "<label>ADDED</label>";
        }
        public string AddBuutonToAddCourseToSemester(string CourseName,int CourseID,int Term)
        {
            return string.Format(@"<form id=""courseID-{0}"">
                    <input type=""submit"" data-coursename=""{1}"" data-courseid=""{2}"" data-term=""{3}"" value=""ADD"" class=""AddCourseToSemester"" />
                    </form >", CourseID, CourseName, CourseID, Term);
        }

    }
}