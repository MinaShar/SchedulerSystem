using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.Admin
{
    public partial class ModifiedScheduleCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int DepartmentId = int.Parse(Request.QueryString["ProgramId"].ToString());

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
    }
}