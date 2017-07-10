using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAccount : System.Web.UI.Page
{

    int RoleID;
    protected void Page_Load(object sender, EventArgs e)
    {

        
        //check if Session has expired or user has not logged in
        if (Session.Count == 0)
        {
            Response.Redirect("~/UserLogin.aspx");
        }
        else
        {

            if (Request.QueryString.HasKeys())
            {
                if (Request.QueryString["UpdateSuccess"].Equals("Password"))
                {
                    lblUpdateSuccess.Text = "You successfully changed your password";
                }
                else if (Request.QueryString["UpdateSuccess"].Equals("Restaurant"))
                {
                    lblUpdateSuccess.Text = "You successfully changed user's restaurant.";
                }
            }

            if (Page.IsPostBack)
            {
                lblUpdateSuccess.Text = "";
            }

            //retrieve nesseccary session data, casting into variables
            string UserName = (string)Session["UserName"];
            RoleID = Int32.Parse(Session["RoleID"].ToString());
            User user = new User();
            user.RoleId = RoleID;
            string RoleName = user.GetRoleName();
            //assign the worker's real name to the welcome label
            lblWelcome.Text = "Welcome " +RoleName+" "+ UserName + ".";

            if (RoleID == 1) //i.e. User is Waiter
            {
                //make the changeCHefDetails button and change password lbl invisible
                lblChangePassword.Visible = false;
                btnUpdateChefRestaurant.Visible = false;

                
            }
            else if (RoleID == 2) //i.e. User is Chef
            {
                // Change the text of WaiterDetails to ChefDetails
                btnUserDetails.Text = "Chef Details";
                btnUpdateChefRestaurant.Visible = true;
                lblChangePassword.Visible = true;
            }
        }
    }



    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Logout.aspx");
    }


    protected void btnUserDetails_Click(object sender, EventArgs e)
    {
        if(RoleID == 1 )
        {
            Response.Redirect("~/WaiterDetails.aspx");
        }
        else
        {
            Response.Redirect("~/ChefDetails.aspx");
        }
        
    }



    protected void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ChangePassword.aspx");
    }



    protected void btnUpdateChefRestaurant_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UpdateChefRestaurant.aspx");
    }


}
