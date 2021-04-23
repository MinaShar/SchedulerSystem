using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.Admin
{
    public partial class AddNewSemester : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string NewSemesterName = Request.Form["NewSemesterName"].ToString();
                string start = Request.Form["StartDate"].ToString();
                string end = Request.Form["EndDate"].ToString();
                Semester.InsertNewSemester(NewSemesterName, start, end);
                ConfirmInsertion.Text = "The new semester inserted successfully";
            }
        }
    }
}