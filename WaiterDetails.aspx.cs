using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class WaiterDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("~/UserLogin.aspx");
        }
        else if (Int32.Parse(Session["RoleID"].ToString()) != 1)
        {
            Response.Redirect("~/UserAccount.aspx");
        }
        else if (!Page.IsPostBack)
        {
            User user = new User();
            user.UserName = (string)Session["UserName"];

            DataTable table = user.UserRestaurant();

            if (table != null)
            {
                String restaurant = table.Rows[0]["RestaurantName"].ToString();
                txtRestaurant.Text = restaurant;

            }
            else
            {
                lblError.Text = "Database connection error. - Could not get restaurant name form database.";
            }

            table = user.UserList();

            if (table != null)
            {

                lstChefs.DataSource = table;
                lstChefs.DataTextField = "RealName";
                lstChefs.DataValueField = "UserID";
                lstChefs.DataBind();
            }
            else
            {
                lblError.Text = "Database connection error. - Could not get user list form database.";
            }


           



        }
    }

    protected void btnShowEmail_Click(object sender, EventArgs e)
    {

        if (lstChefs.SelectedIndex != -1)
        {
            User user = new User();
            user.RealName = lstChefs.SelectedItem.Text;

            DataTable table = user.GetEMail();

            if(table.Rows.Count > 0)
            {

                lblEmail.Text = table.Rows[0]["EmailAddress"].ToString();
            }
         
           
        }
        else
        {
            lblError.Text = "You must select a chef.";
        }
    }
}
