using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.Instructor
{
    public partial class Instructor : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int InstructorID = int.Parse(Session["instructorID"].ToString());
            Doctor CuurentDoctor = Doctor.GetDoctorByID(InstructorID);
            CurrentInstructorName.InnerText = CuurentDoctor.Name;
            CoursesThatDoctorTeaches.DataSource = Course.GetCoursesOfDoctor(InstructorID);
            CoursesThatDoctorTeaches.DataBind();
        }
    }
}