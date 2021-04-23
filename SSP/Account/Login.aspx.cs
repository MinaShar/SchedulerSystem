using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Models;

namespace SSP.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void LogIn(object sender, EventArgs e)
        {
            int ID;
            bool isInteger = int.TryParse(IdField.Text.ToString(),out ID);
            if (isInteger == false)
            {
                ErrorMessageContainer.Text = "ID must be a number";
                return;
            }
            string UserPassword = Password.Text.ToString();
            int SelectedType = UserType.SelectedIndex;
            if (SelectedType == 0)
            {
                AdminModel x = AdminModel.GetAdmin(ID);
                if (String.Compare(UserPassword, x.Password) == 0)
                {
                    Session["ID"] = x.Id;
                    Response.Redirect("~/Admin/AddNewUser.aspx");
                }
                else
                {
                    PasswordError.Text = "The Password typed isnot correct";
                    return;
                }
            }
            else if (SelectedType == 1)
            {
                Doctor x = Doctor.GetDoctorByID(ID);
                
                if (String.Compare(UserPassword, x.Password) == 0)
                {
                    Session["instructorID"] = x.Id;
                    Response.Redirect("~/Instructor/TimeTable.aspx");
                }
            }
            else if (SelectedType == 2)
            {
                Student x = Student.GetStudentById(ID);
            }
        }
    }
}