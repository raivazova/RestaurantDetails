using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Password
/// </summary>
public class Password
{
  
    
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    
    
    public byte[] Salt { get; set; }
    public byte[] hashPlusSalt { get; set; }
  






    private DatabaseConnection dataConn;


    // Constructor for User creates an instance of DatabaseConnection
    public Password()
    {
        dataConn = new DatabaseConnection();
    }


    // Hashing and salting the password to store it securely
    public string PasswordHash(string password)
    {
       

        // Convert the plain string pwd into bytes
        byte[] passwordBytes = System.Text.UnicodeEncoding.Unicode.GetBytes(password);

        // Append salt to pwd before hashing
        byte[] combinedBytes = new byte[passwordBytes.Length + Salt.Length];
        System.Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
        System.Buffer.BlockCopy(Salt, 0, combinedBytes, passwordBytes.Length, Salt.Length);

        // Create hash for the pwd+salt
        System.Security.Cryptography.HashAlgorithm hashAlgo = new System.Security.Cryptography.SHA256Managed();
        byte[] hash = hashAlgo.ComputeHash(combinedBytes);


        // Append the salt to the hash
        hashPlusSalt = new byte[hash.Length + Salt.Length];
        System.Buffer.BlockCopy(hash, 0, hashPlusSalt, 0, hash.Length);
        System.Buffer.BlockCopy(Salt, 0, hashPlusSalt, hash.Length, Salt.Length);


       
        //To base64 string from bytes
        return  Convert.ToBase64String(hashPlusSalt);

    }


    // authenticate password 
    public bool authenticatePassword(String password)
    {
        dataConn.addParameter("@UserName", UserName);

        string command = "Select UserPassword, Salt from RestaurantUser Where UserName= @UserName";

        DataTable table = dataConn.executeReader(command);
       
            Salt = Convert.FromBase64String(table.Rows[0]["Salt"].ToString());
            UserPassword = table.Rows[0]["UserPassword"].ToString();

             
            string checkedPass = PasswordHash(password);    
            if(UserPassword.Equals(checkedPass))
            {
                return true;
            }
            else
            {
                return false;
            }

    }

    // Generate salt for new password
    public string GenerateSalt()
    {
        byte[] salt = new byte[32];
        System.Security.Cryptography.RNGCryptoServiceProvider.Create().GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    // Checks in the Database if that is the correct passoword for this user.
    public int PasswordCorrect()
    {
        dataConn.addParameter("@UserName", UserName);


        string command = "Select UserPassword from RestaurantUser where UserName = @UserName";

        DataTable table = dataConn.executeReader(command);


        if (table.Rows.Count > 0)
        {
            if (table.Rows[0]["UserPassword"].ToString().Equals(UserPassword))
            {
                // return 0 if the password for that user in the database 
                //is the same as the one provided in the current password txtfield
                return 0;
            }
            else
            {
                // if the provided password doesnt mach the one in the database return 1
                return 1;
            }


        }
        else
        {
            // if table has no rows there was a database error - return 2
            return 2;
        }

    }




    //Update new password for the current user in the records
    public int UpdatePassword()
    {
        dataConn.addParameter("@UserPassword", UserPassword);
        dataConn.addParameter("@UserName", UserName);
        dataConn.addParameter("@Salt", Convert.ToBase64String(Salt));
        string command = "Update RestaurantUser SET Salt = @Salt, UserPassword= @UserPassword Where UserName=@UserName";

        // returns number of rows affected by the query or -1 if exception was caught
        return dataConn.executeNonQuery(command);


    }




}
