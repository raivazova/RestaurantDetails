using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Collections;

public partial class ChefDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("~/UserLogin.aspx");
        }
        else if(Int32.Parse(Session["RoleID"].ToString()) != 2)
        {
            Response.Redirect("~/UserAccount.aspx");
        }
        else if(!Page.IsPostBack)
        {
            User user =  new User();
            user.UserName = (string)Session["UserName"];
            user.RoleId = Int32.Parse(Session["RoleID"].ToString());
            DataTable table = user.UserRestaurant();

            if(table != null)
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


                lstWaiters.DataSource = table;
                lstWaiters.DataTextField = "RealName";
                lstWaiters.DataValueField = "UserID";
                lstWaiters.DataBind();
                
               
            }
            else
            {
                lblError.Text = "Database connection error. - Could not get restaurant name form database.";
            }

            Restaurant rest = new Restaurant();

            rest.RestaurantId = Int32.Parse(Session["RestaurantID"].ToString());


            DataTable dtable = rest.GetCuisines();
            if (dtable != null)
            {
                rptCuisines.DataSource = dtable;
                rptCuisines.DataBind();
            }
            else
            {
                lblError.Text = "Database connection error. - Could not get cuisine details.";
            }




        }
    }

    protected void btnRemoveWaiter_Click(object sender, EventArgs e)
    {
        if(lstWaiters.SelectedIndex != -1)
        {
            User user = new User();
            user.RealName = lstWaiters.SelectedItem.Text;
            int result = user.RemoveWaiter();

            if(result > 0)
            {
                lstWaiters.Items.RemoveAt(lstWaiters.SelectedIndex);
                lblSuccess.Text = "Waiter "+user.UserName+" succesfuly removed from database";
            }
            else
            {

                lblError.Text = "Database connection error. - Could not delete waiter  form database.";
            }

        }
        else
        {
            lblError.Text = "You must select a waiter to remove from the list.";
        }
    }
}
