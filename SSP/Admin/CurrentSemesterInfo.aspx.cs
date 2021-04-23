using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.Admin
{
    public partial class CurrentSemesterInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Semester CurrentSemester = Semester.GetLastSemester();
            if (CurrentSemester.IsWorking == true)
            {
                IsClosed0.Checked = true;
            }
            else
            {
                IsClosed1.Checked = true;
            }

            if (CurrentSemester.IsRegistrationOpen == true)
            {
                OpenReg1.Checked = true;
            }
            else
            {
                OpenReg0.Checked = true;
            }
        }
    }
}