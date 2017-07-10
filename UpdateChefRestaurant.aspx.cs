using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class UpdateChefRestaurant : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("~/UserLogin.aspx");
        }
        else if (Int32.Parse(Session["RoleID"].ToString()) != 2)
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

            Restaurant rest = new Restaurant();

            table = rest.getAllRestaurants();

            if (table != null)
            {

               

                   lstRestaurants.DataSource = table;
                    lstRestaurants.DataTextField = "RestaurantName";
                    lstRestaurants.DataValueField = "RestaurantID";
                    lstRestaurants.DataBind();

               
            }
            else
            {
                lblError.Text = "Database connection error. - Could not get restaurant list form database.";
            }

        }
    }

    protected void btnUpdateRestaurant_Click(object sender, EventArgs e)
    {

        if (lstRestaurants.SelectedIndex != -1)
        {
            User user = new User();
            user.RestaurantId= Int32.Parse(lstRestaurants.SelectedItem.Value);
            user.UserName = (string)Session["UserName"];

            int result = user.ChangeRestaurant();

            if (result > 0)
            {
                HttpContext.Current.Session["RestaurantID"] = user.RestaurantId;

                Response.Redirect("~/UserAccount.aspx?UpdateSuccess=Restaurant");


            }
            else
            {

                lblError.Text = "Database connection error. - Could not update database to selected restaurant.";
            }

        }
        else
        {
            lblError.Text = "You must select a restaurant from the list.";
        }
    }
   
}
