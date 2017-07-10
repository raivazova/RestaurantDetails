using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public string RealName { get; set; }
    public string EmailAddress { get; set; }
    public int RoleId { get; set; }
    public int RestaurantId { get; set; }

    private DatabaseConnection dataConn;


    // Constructor for User creates an instance of DatabaseConnection
    public User()
    {
        dataConn = new DatabaseConnection();
    }


    // method that checks if user with the same username already exists in the database
    public bool userNameExists()
    {
        dataConn.addParameter("@UserName", UserName);

        string command = "Select COUNT(UserName) FROM RestaurantUser WHERE UserName=@UserName";

        int result = dataConn.executeScalar(command); //result of count

        // if result >0 - username already exists
        return result > 0 || result == -1; //if record found or exception caught

    }

    // Checks if the Email Address is in valid format
    public bool IsValidEmailAddress(string email)
    {
        try
        {
            var emailChecked = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }


    //Register new user and add to the DataBase
    public bool addUser()
    {
        dataConn.addParameter("@UserName", UserName);
       
        dataConn.addParameter("@RealName", RealName);
        dataConn.addParameter("@RestaurantID", RestaurantId);
        dataConn.addParameter("@RoleID", RoleId);
        dataConn.addParameter("@EmailAddress", EmailAddress);

        // Generate salt
        Password pass = new Password();

        string saltHash = pass.GenerateSalt();
        
        pass.Salt = Convert.FromBase64String(saltHash);

        string password = pass.PasswordHash(UserPassword);
        dataConn.addParameter("@UserPassword", password);
        dataConn.addParameter("@Salt", saltHash);
    

        string command = "INSERT INTO RestaurantUser (UserName, UserPassword, Salt, RealName, EmailAddress, RestaurantID, RoleID) "+
            "VALUES (@UserName, @UserPassword,@Salt, @RealName, @EmailAddress,  @RestaurantID, @RoleID)";

        return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
    }



   





    

    // Change the restaurant a chef works in
    public int ChangeRestaurant()
    {
        dataConn.addParameter("@RestaurantID", RestaurantId);
        dataConn.addParameter("@UserName", UserName);


        string command = "Update RestaurantUser Set RestaurantID = @RestaurantID Where UserName=@UserName ";

        return dataConn.executeNonQuery(command);
    }



    //Gets Iniials from a name string
    public string GetInitials(String name)
    {
        return name[0]+ "" + name[name.IndexOf(" ")+1];
    }



    //Checking which restaurant the user works for
    public DataTable UserRestaurant()
    {
        dataConn.addParameter("@UserName", UserName);

        string command = "Select RestaurantName from  Restaurant Where RestaurantID in " +
            "(Select RestaurantID FROM RestaurantUser Where UserName = @UserName)";

        DataTable table = dataConn.executeReader(command);
        return table;
    }



    //Removes a waiter form the database
    public int RemoveWaiter()
    {
        dataConn.addParameter("@RealName", RealName);

        string command = "Delete from RestaurantUser where RealName = @RealName";


        // returns number of rows affected by the query or -1 if exception was caught
        return dataConn.executeNonQuery(command);
    }
    


    //Getting a list of waiters that work in the same restaurant as given chef
    public DataTable UserList()
    {
        // addint the userName as a parameter of the databaseConnection
        dataConn.addParameter("@UserName", UserName);
        // checking if the logged in user is chef or waiter
        if(RoleId == 2)
        {
            // if chef assign 1 to the parameter RoleID to make sure we get list of the waiters
            dataConn.addParameter("@RoleID", 1);
        }
        else
        {
            // if waiter assign 1 to the parameter RoleID to make sure we get list of the chefs

            dataConn.addParameter("@RoleID", 2);
        }
        


        string command = "Select UserID, RealName from  RestaurantUser Where RoleID = @RoleID and RestaurantID in " +
            "(Select RestaurantID FROM RestaurantUser Where UserName = @UserName)";

        DataTable table = dataConn.executeReader(command);
        return table;
    }



   




    // Show the email adress of selected chef
    public DataTable GetEMail()
    {
        dataConn.addParameter("@RealName", RealName);

        string command = "Select EmailAddress from RestaurantUser Where RealName=@RealName";

        DataTable table = dataConn.executeReader(command);
        return table;
    }

   

    // Gets the role name for the user.
    public string GetRoleName()
    {
        dataConn.addParameter("@RoleId", RoleId);

        string command = "Select RoleName from UserRole  Where RoleID= @RoleId";

        DataTable table = dataConn.executeReader(command);
        if(table.Rows.Count >0)
        {
            // returns the role of the logged in user 
            return table.Rows[0]["RoleName"].ToString();
        }
        else
        {
            return " ";
        }
    }



    /*System queries database to authenticate the username and password 
    and creates Session objects for UserID, UserName, RestaurantID and RoleID
    returns 0 if there is database connection error, 1 if user data is correct 
    and 2 if the username or password provided are incorect */
    public int authenticateUser()
    {

        Password pass = new Password();
        pass.UserName = UserName;
        

        if(pass.authenticatePassword(UserPassword))
        {
            dataConn.addParameter("@UserName", UserName);
            

            string command = "Select UserID, UserName, RealName, RestaurantID, RoleID FROM RestaurantUser " +
                            "WHERE UserName=@UserName";

            DataTable table = dataConn.executeReader(command);


            if (table.Rows.Count > 0)
            {
                HttpContext.Current.Session["UserID"] = table.Rows[0]["UserID"].ToString();
                HttpContext.Current.Session["UserName"] = table.Rows[0]["UserName"].ToString();
                HttpContext.Current.Session["RestaurantID"] = table.Rows[0]["RestaurantID"].ToString();
                HttpContext.Current.Session["RoleID"] = table.Rows[0]["RoleID"].ToString();
                HttpContext.Current.Session["RealName"] = table.Rows[0]["RealName"].ToString();

                return 1;
            }
            else if (table == null)
            {
                return 0;
            }
            else
            {
                return 2;
            }

        }
        else
        {
            return 3;
        }

    }

}
