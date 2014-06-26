<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SSO_GAD.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GOanySSO portal</title>
</head>
<body>
    <div style="text-align: center">
    
        <asp:Image ID="Image1" runat="server" Height="194px" ImageUrl="http://74.208.69.121:8089/webdenial2_archivos/image002.png" Width="276px" />
    </div>
        <br />
        <div>
        </div>
    <div >
    <form id="form1" runat="server">

    <div style="margin-left: 43%">
    
        <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/welcome.aspx" OnAuthenticate="Login1_Authenticate" Height="145px" LoginButtonText="Iniciar Sesion" PasswordLabelText="Token" PasswordRequiredErrorMessage="Requiere que ingrese token." TitleText="Iniciar Sesion" UserNameLabelText="Usuario" UserNameRequiredErrorMessage="Requiere que ingrese contraseña." Width="297px">
        </asp:Login>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#FF3300" Text="Label" Visible="False"></asp:Label>
    
    </div>
    </form>
    </div>

    <p>
        &nbsp;</p>

</body>
</html>