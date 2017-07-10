using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

public partial class UpdatePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("~/UserLogin.aspx");
        }
        
    }

    protected void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        if (txtCurrentPassword.Text.Length < 6)
        {
            lblError.Text = "Password is invalid length. Please insert a passowrd longer than 6 symbols.";
        }
        else if (txtNewPassword.Text.Length < 6)
        {
            lblError.Text = "New password is invalid length. Please insert a passowrd longer than 6 symbols.";
        }
        else if (!txtNewPassword.Text.Equals(txtConfirmPassword.Text))
        {
            lblError.Text = "Passwords don't match.";
        }
        else
        {
            
            Password pass = new Password();

            pass.UserName = (string)Session["UserName"];
           
            if(pass.authenticatePassword(txtCurrentPassword.Text))
            {

                pass.Salt = Convert.FromBase64String(pass.GenerateSalt());
                pass.UserPassword = pass.PasswordHash(txtNewPassword.Text);

                if (pass.UpdatePassword() > 0)
                {
                    Response.Redirect("~/UserAccount.aspx?UpdateSuccess=Password");
                }
                else
                {
                    lblError.Text = "Database connection error.";
                }
            }
            else
            {
                lblError.Text = "Password is incorrect.";

            }


        }
    }

    protected void txtNewPassword_TextChanged(object sender, EventArgs e)
    {


        if(txtNewPassword.Text.Length == 6)
        {
            lblPassword.Text = "Password strenght : Low";
            lblPassword.ForeColor = System.Drawing.Color.Red; 
        }
        else if(txtNewPassword.Text.Length >6 && txtNewPassword.Text.Length<8)
        {
            lblPassword.Text = "Password strenght : Medium";
            lblPassword.ForeColor = System.Drawing.Color.Yellow;
        }
        else if (lblPassword.Text.Length > 8)
        {
            lblPassword.Text = "Password strenght : High";
            lblPassword.ForeColor = System.Drawing.Color.Green;


        }
        else
        {
            lblPassword.Text = "Password too short";
            lblPassword.ForeColor = System.Drawing.Color.Red;
        }
    }

    
}
