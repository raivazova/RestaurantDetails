<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateChefRestaurant.aspx.cs" Inherits="UpdateChefRestaurant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:Button ID="btnUserAccount" runat="server" PostBackUrl="~/UserAccount.aspx" 
        style="z-index: 1; left: 104px; top: 15px; position: absolute" 
        Text="User Account" />
     
     <asp:Label ID="lblTitle" runat="server" 
            style="z-index: 1; left: 102px; top: 56px; position: absolute" 
            Text="Update Chef Restaurant" Font-Bold="True" Font-Size="Larger" 
            Font-Underline="True"></asp:Label>
     
     <asp:Label ID="lblRestaurant" runat="server" 
        style="z-index: 1; left: 97px; top: 103px; position: absolute" 
            Text="Current Restaurant:"></asp:Label>
    <asp:TextBox ID="txtRestaurant" runat="server" ReadOnly="True" 
        style="z-index: 1; left: 224px; top: 102px; position: absolute"></asp:TextBox>
     
     <asp:Label ID="lblSwitchToRestaurant" runat="server" 
        style="z-index: 1; left: 106px; top: 136px; position: absolute" 
        Text="Switch to Restaurant:"></asp:Label>       
     
    <asp:ListBox ID="lstRestaurants" runat="server"  
        
            
            
            style="z-index: 1; left: 106px; top: 165px; position: absolute; height: 72px; width: 180px">
    </asp:ListBox>
       
    
   
    <asp:Button ID="btnUpdateRestaurant" runat="server" onclick="btnUpdateRestaurant_Click" 
        style="z-index: 1; left: 106px; top: 254px; position: absolute" 
        Text="Update Restaurant" />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" 
        style="z-index: 1; left: 106px; top: 294px; position: absolute"></asp:Label>
        
         </div>
    </form>
</body>
</html>
