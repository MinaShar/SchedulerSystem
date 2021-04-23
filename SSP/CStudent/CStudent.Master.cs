using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.CStudent
{
    public partial class CStudent : System.Web.UI.MasterPage
    {
        public int CurrentStudentID;
        public int CurrentStudentDepartmentID;
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentStudentID = int.Parse(Request.QueryString["ID"].ToString());
            Student CurrentS = Student.GetStudentById(CurrentStudentID);
            CurrentStudentDepartmentID = CurrentS.DepartmentId;

        }
    }
}