using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSP.Instructor
{
    public partial class CousreAndContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int CourseIDFromQueryString = int.Parse(Request.QueryString["CourseID"].ToString());
            int GroupNumberFromQueryString = int.Parse(Request.QueryString["GroupNumber"].ToString());
            CourseID.Value = string.Format("{0}", CourseIDFromQueryString);
            GroupNumber.Value = string.Format("{0}", GroupNumberFromQueryString);
            int InstructorID = int.Parse(Session["instructorID"].ToString());
            DoctorID.Value = string.Format("{0}", InstructorID);

        }
    }
}