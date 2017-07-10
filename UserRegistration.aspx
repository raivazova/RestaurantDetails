<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="UserRegistration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

          <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>    
        
         <asp:TextBox ID="txtPassword" runat="server" 
        style="z-index: 1; left: 184px; top: 111px; position: absolute" 
        TextMode="Password" OnTextChanged="txtPassword_TextChanged" AutoPostBack="True"></asp:TextBox>
           
<asp:UpdatePanel ID="UpdatePanel1" runat="server" 	ChildrenAsTriggers="False" UpdateMode="Conditional">
     <ContentTemplate>
               <asp:Label ID="lblPasswrdStren" runat="server" ForeColor="Red" 
        style="z-index: 1; left: 365px; top: 117px; position: absolute"></asp:Label>
     </ContentTemplate>
     <Triggers>
           <asp:AsyncPostBackTrigger ControlID="txtPassword" 	   EventName="TextChanged"/>
     </Triggers>
</asp:UpdatePanel>

    
        <asp:Label ID="lblRegister" runat="server" 
            style="z-index: 1; left: 98px; top: 40px; position: absolute" 
            Text="Waiter Registration Form" Font-Bold="True" Font-Size="Larger" 
            Font-Underline="True"></asp:Label>
    
    <asp:Label ID="lblUsername" runat="server" 
        style="z-index: 1; left: 102px; top: 77px; position: absolute; height: 19px" 
        Text="Username:"></asp:Label>
    <asp:Label ID="lblPassword" runat="server" 
        style="z-index: 1; left: 103px; top: 111px; position: absolute" 
        Text="Password:"></asp:Label>
    <asp:Label ID="lblConfirmPassword" runat="server" 
        style="z-index: 1; left: 54px; top: 148px; position: absolute" 
        Text="Confirm Password:"></asp:Label>
    <asp:TextBox ID="txtUsername" runat="server" 
        style="z-index: 1; left: 184px; top: 76px; position: absolute"></asp:TextBox>
   
    <asp:TextBox ID="txtConfirmPassword" runat="server" 
        style="z-index: 1; left: 184px; top: 149px; position: absolute" 
        TextMode="Password"></asp:TextBox>
    <asp:DropDownList ID="ddlRestaurants" runat="server" 
        style="z-index: 1; left: 184px; top: 267px; position: absolute; height: 49px;">
    </asp:DropDownList>
    <asp:Button ID="btnRegister" runat="server" 
        style="z-index: 1; left: 182px; top: 359px; position: absolute" 
        Text="Register" onclick="btnRegister_Click" />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" 
        style="z-index: 1; left: 184px; top: 391px; position: absolute"></asp:Label>
     </div>
    <asp:Label ID="lblRealName" runat="server" 
        style="z-index: 1; left: 100px; top: 186px; position: absolute" 
        Text="Full Name:"></asp:Label>
    <asp:TextBox ID="txtRealName" runat="server" 
        style="z-index: 1; left: 184px; top: 184px; position: absolute"></asp:TextBox>
    <asp:TextBox ID="txtEmailAddress" runat="server" 
        style="z-index: 1; left: 184px; top: 225px; position: absolute"></asp:TextBox>
    <asp:Label ID="lblEmailAddress" runat="server" 
        style="z-index: 1; left: 75px; top: 226px; position: absolute" 
        Text="Email Address:"></asp:Label>


      
    <p>
    <asp:Label ID="lblRestaurant" runat="server" 
        style="z-index: 1; left: 100px; top: 267px; position: absolute" 
        Text="Restaurant:"></asp:Label>

        <asp:Label ID="lblRole" runat="server" 
        style="z-index: 1; left: 131px; top: 316px; position: absolute" 
        Text="Role:"></asp:Label>

    </p>
        <asp:RadioButtonList ID="rblRole" runat="server"
             style="z-index: 1; left: 180px; top: 316px; position: absolute" 
         RepeatDirection="Horizontal" RepeatLayout="Table">
                <asp:ListItem Text="Chef" Value="Chef"></asp:ListItem>
                <asp:ListItem Text="Waiter" Value="Waiter"></asp:ListItem>
               
            </asp:RadioButtonList>            
        
       
      
    </form>
</body>
</html>
