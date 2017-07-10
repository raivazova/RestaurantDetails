using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

public partial class UserLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {

        
        // validate input before connecting to database
        if (txtUsername.Text.Length < 5 || txtUsername.Text.Length>20)
        {
            lblError.Text = "Username is invalid length. Please insert username between 5 and 20 characters";
        }
        else if (txtPassword.Text.Length < 6)
        {
            lblError.Text = "Password is invalid length. Please insert a passowrd longer than 6 symbols.";
        }
        else
        {
            User user = new User();

            user.UserName = txtUsername.Text;
            user.UserPassword = txtPassword.Text;

            int result = user.authenticateUser();

            if ( result == 1)
            {
                //User is redirected to UserAccount.aspx
                Response.Redirect("~/UserAccount.aspx");
            }
            else if (result ==2)
            {
                lblError.Text = "Incorrect username and/or password";
            }
            else if(result ==3)
            {
                lblError.Text = "password incorrect";
            }
            else
            {
                lblError.Text = "Database connection error";
            }

        }



    }



}
