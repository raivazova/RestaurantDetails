using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


public class Restaurant
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string UserName { get; set; }

    private DatabaseConnection dataConn;

    public Restaurant()
    {
        dataConn = new DatabaseConnection();
    }

    public DataTable getAllRestaurants()
    {
        string command = "Select * FROM Restaurant";
        return dataConn.executeReader(command);
    }



    // Gets all cuisines that are part of the chef's restaurant

    public DataTable GetCuisines()
    {
        dataConn.addParameter("@RestaurantID", RestaurantId);

        string command = "Select  CuisineRegion, CuisineName from  Cuisine Where CuisineID in " +
           "(Select CuisineID FROM RestaurantCuisine Where RestaurantID = @RestaurantID)";

        DataTable table = dataConn.executeReader(command);
        return table;
    }




    



   

    }

