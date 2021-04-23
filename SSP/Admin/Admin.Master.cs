using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                AdminModel current_admin = AdminModel.GetAdmin(int.Parse(Session["ID"].ToString()));
                CurrentAdminName.InnerHtml = current_admin.Name;
            }
        }
    }
}