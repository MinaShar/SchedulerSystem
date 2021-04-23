using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSP.CStudent
{
    public partial class TimeTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int StudentID = int.Parse(Request.QueryString["ID"].ToString());
            CurrentStudentID.Value = StudentID.ToString();
        }
    }
}