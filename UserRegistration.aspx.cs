using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class UserRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if request is NOT a post back
        if (!Page.IsPostBack)
        {
            //create instane of middle layer business object
            Restaurant restaurant = new Restaurant();
            //retrieve restaurants from middle layer into a DataTable
            DataTable table = restaurant.getAllRestaurants();

            //check if query was successful
            if (table != null)
            {
                //set DropDownList's data source to the DataTable
                ddlRestaurants.DataSource = table;
                //assign RestaurantID database field to the value property
                ddlRestaurants.DataValueField= "RestaurantID";
                //assign RestaurantName database field to the text property
                ddlRestaurants.DataTextField = "RestaurantName";
                //bind data
                ddlRestaurants.DataBind();
            }
            else
            {
                lblError.Text = "Database connection error - cannot display restaurants.";
            }

        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Inside eWOOOOScalar method...");


        //validate input
        if(rblRole.SelectedValue == "")
        {
            lblError.Text = "You must select your role";
        }
        else if (txtUsername.Text.Length < 5 || txtUsername.Text.Length > 20)
        {
            lblError.Text = "Username must be at between 5 and 20 characters.";
        }
        else if (txtPassword.Text.Length < 6)
        {
            lblError.Text = "Password must be at least 6 characters long.";
        }
        else if (!txtConfirmPassword.Text.Equals(txtPassword.Text))
        {
            lblError.Text = "Please confirm password.";
        }
        else if (txtRealName.Text.Equals(""))
        {
            lblError.Text = "Please enter your full name.";
        }
        else
        {
            //create instanse of middle layer business object
            User user = new User();

            //set username property, so it  can be used as a parameter for the query
            user.UserName = txtUsername.Text;
            bool email = user.IsValidEmailAddress(txtEmailAddress.Text);
            //check if the username exists
            if (!email)
            {
                //email is not in the appropriate format
                lblError.Text = "The email address provided is not in the appropriate format.";
            }
            else if (user.userNameExists())
            {
                //already exists so output error
                lblError.Text = "Username already exists, please select another";
            }
            else
            {
                //INSERT NEW WORKER...   

                //set properties, so it can be used as a parameter for the query
                user.UserName = txtUsername.Text;
                user.UserPassword = txtPassword.Text;
                user.RealName = txtRealName.Text;
                user.RestaurantId = Int32.Parse(ddlRestaurants.SelectedValue);
                user.EmailAddress = txtEmailAddress.Text;
                if(rblRole.SelectedValue=="Chef")
                {
                    user.RoleId = 2;
                }
                else if(rblRole.SelectedValue=="Waiter")
                {
                    user.RoleId = 1;
                }
                

                //attempt to add a worker and test if it is successful
                if (user.addUser())
                {
                    //redirect user to login page
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    //exception thrown so display error
                    lblError.Text = "Database connection error - failed to insert record.";
                }
            }
        }
    }

    protected void txtPassword_TextChanged(object sender, EventArgs e)
    {
        if (txtPassword.Text.Length == 6)
        {
            lblPasswrdStren.Text = "Password strenght : Low";
            lblPasswrdStren.ForeColor = System.Drawing.Color.Red;
        }
        else if (txtPassword.Text.Length > 6 && txtPassword.Text.Length < 8)
        {
            lblPasswrdStren.Text = "Password strenght : Medium";
            lblPasswrdStren.ForeColor = System.Drawing.Color.Yellow;
        }
        else if (lblPassword.Text.Length > 8)
        {
            lblPasswrdStren.Text = "Password strenght : High";
            lblPasswrdStren.ForeColor = System.Drawing.Color.Green;


        }
        else
        {
            lblPasswrdStren.Text = "Password too short";
            lblPasswrdStren.ForeColor = System.Drawing.Color.Red;
        }
    }



   
}
 