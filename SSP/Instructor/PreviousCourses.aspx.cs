using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.Instructor
{
    public partial class PreviousCourses : System.Web.UI.Page
    {
        public int InstructorID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            InstructorID = int.Parse(Session["instructorID"].ToString());
            PreviousSemesters.DataSource = Semester.GetPreviosSemestersOFDoctor(InstructorID);
            PreviousSemesters.DataBind();
        }

        public List<LecturesOfGroups> GetCoursesOfDoctorAtGivenSemesetr(int SemesterID)
        {
            return LecturesOfGroups.GetCoursesOfDoctorAtGivenSemester(InstructorID, SemesterID);
        }
    }
}