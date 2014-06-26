<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SSO_GAD.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GOanySSO portal</title>
</head>
<body>
    <div style="text-align: center">
    
        <asp:Image ID="Image1" runat="server" Height="194px" ImageUrl="~/img/logo vector green-  Transparent.png" Width="276px" />
    </div>
        <br />
        <div>
        </div>
    <div >
    <form id="form1" runat="server">

    <div style="margin-left: 43%">
    
        <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/welcome.aspx" OnAuthenticate="Login1_Authenticate" BackColor="#F7F7DE" BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt">
            <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        </asp:Login>
    
    </div>
    </form>
    </div>

</body>
</html>