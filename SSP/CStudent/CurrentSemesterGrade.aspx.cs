using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSP.CStudent
{
    public partial class CurrentSemesterGrade : System.Web.UI.Page
    {
        public int StudentID;
        protected void Page_Load(object sender, EventArgs e)
        {
            StudentID = int.Parse(Request.QueryString["ID"].ToString());
            StudentIDHidden.Value = StudentID.ToString();
        }
    }
}