using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.CStudent
{
    public partial class AllStudentGrades : System.Web.UI.Page
    {
        public int StudentID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            StudentID = int.Parse(Request.QueryString["ID"].ToString());
            StudentSemesters.DataSource = Semester.GetStudentSemesters(StudentID);
            StudentSemesters.DataBind();
        }

        public List<StudentCourseSemester> GetStudentGradesInSemester(int SemesterID)
        {
            return StudentCourseSemester.GetGradesOfStudentInSemester(StudentID, SemesterID);
        }

        public int GetStudentGradeINSemester(int SemesterID)
        {
            return StudentCourseSemester.GetStudentGradeInSemester(StudentID, SemesterID);
        }
    }
}