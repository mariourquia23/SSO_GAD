<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="welcome.aspx.cs" Inherits="SSO_GAD.welcome" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GOanySSO portal</title>
    <style type="text/css">
        #TextArea1 {
            height: 143px;
            width: 456px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <asp:Image ID="Image1" runat="server" Height="194px" ImageUrl="~/img/logo vector green-  Transparent.png" Width="276px" />
    
        <br />
        <div>
        </div>
    
    </div>
        <div style="text-align: center">
            <br />
            <asp:Label ID="Label1" runat="server" style="text-align: left" Text="Label"></asp:Label>
            <br />
            
            <asp:TextBox ID="TextBox1" runat="server" Height="169px" style="margin-top: 18px" TextMode="MultiLine" Width="679px"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://74.208.69.121:8080/webclient/WebClient.jsf">GoAny WebClient</asp:HyperLink>
            <div>
            </div>
        </div>
    </form>
</body>
</html>

