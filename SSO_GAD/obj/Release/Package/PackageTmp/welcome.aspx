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
        /* Basics */
html, body {
    width: 100%;
    height: 100%;
    font-family: "Helvetica Neue", Helvetica, sans-serif;
    color: #444;
    -webkit-font-smoothing: antialiased;
    background: #f0f0f0;
}
#container {
    position: fixed;
    width: 679px;
    height: 300px;
    top: 50%;
    left: 40%;
    margin-top: -140px;
    margin-left: -170px;
	background: #fff;
    border-radius: 3px;
    border: 1px solid #ccc;
    box-shadow: 0 1px 2px rgba(0, 0, 0, .1);
            text-align: center;
        }
form {
    margin: 0 auto;
    margin-top: 20px;
}
label {
    color: #555;
    display: inline-block;
    margin-left: 18px;
    padding-top: 10px;
    font-size: 14px;
}
p a {
    font-size: 11px;
    color: #aaa;
    float: right;
    margin-top: -13px;
    margin-right: 20px;
 -webkit-transition: all .4s ease;
    -moz-transition: all .4s ease;
    transition: all .4s ease;
}
p a:hover {
    color: #555;
}
input {
    font-family: "Helvetica Neue", Helvetica, sans-serif;
    font-size: 12px;
    outline: none;
    border:hidden;
}

#lower {
    background: #ecf2f5;
    width: 100%;
    height: 69px;
    margin-top: 20px;
	  box-shadow: inset 0 1px 1px #fff;
    border-top: 1px solid #ccc;
    border-bottom-right-radius: 3px;
    border-bottom-left-radius: 3px;
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <asp:Image ID="Image1" runat="server" Height="194px" ImageUrl="http://74.208.69.121:8089/webdenial2_archivos/image002.png" Width="276px" />
    
    </div>
        <div id="container">
            <br />
            <asp:Label ID="Label1" runat="server" style="text-align: left" Text="Label"></asp:Label>
            <br />
            
            <asp:TextBox ID="TextBox1" runat="server" Height="250px"  TextMode="MultiLine" Width="675px" BorderStyle="None"></asp:TextBox>
            <br />
            
            <div id="lower">
             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://74.208.69.121:8080/webclient/WebClient.jsf">ir a GoAny WebClient</asp:HyperLink>
           
            </div>
        </div>
    </form>
</body>
</html>

