using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace SSP.Admin
{
    public partial class AddNewUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string NewUserName = Request.Form["NewUserName"].ToString();
                string NewUserPassword = Request.Form["NewUserPassword"].ToString();
                if (int.Parse(Request.Form["NewUserType"].ToString()) == 1)
                {///admin type
                    AdminModel.InsertAdmin(NewUserName, NewUserPassword);
                }
                else if (int.Parse(Request.Form["NewUserType"].ToString()) == 2)
                {///doctor type
                    int Rank = int.Parse(Request.Form["Rank"].ToString());
                    Doctor.InsertNewDoctor(NewUserName, NewUserPassword, Rank);
                }
                else if (int.Parse(Request.Form["NewUserType"].ToString()) == 3)
                {///student type
                    int DepartmentID = int.Parse(Request.Form["NewUserDepartmentID"].ToString());
                    Student.InsertNewStudent(NewUserName, NewUserPassword, DepartmentID);
                }
                ConfirmInsertion.Text = "New user inserted successfully";
            }
            Departments.DataSource = Department.GetAll();
            Departments.DataBind();
        }
    }
}