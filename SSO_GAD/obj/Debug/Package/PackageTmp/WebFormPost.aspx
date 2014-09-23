<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormPost.aspx.cs" Inherits="SSO_GAD.WebFormPost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
         <style type="text/css">
        
        /* Basics */
html, body {
    width: 100%;
    height: 100%;
    background-color:#fff;
    

}

 #panel {
            position: absolute;
            left: 50%;
            top: 50%;
            height: 473px;
            width: 680px;
            margin-top: -225px;
            margin-left: -340px;
            overflow: hidden;
            /*border: 2px solid Red;*/
            text-align: center;
        }
#container {
    padding-right:1px;
    width: 675px;
    height: 300px;
    background-color: #f3f8ff;
    border: 1px solid #93b8d8;
    border-radius: 3px;
    box-shadow: 0 1px 2px rgba(0, 0, 0, .1);
            text-align: center;
        }

#TextBox1 {
            height: 298px;
            width: 99%;
            background-color: #f3f8ff;
            border:none;
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





    </style>  
 <%=redirectHtML %>
</head>
<body runat="server" id ="body">
    <form id="form1" runat="server">
    <div id="panel" runat="server">
    
        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/logo.png" />
        
        <br />
        
    <br /><asp:Label ID="titulo" runat="server" style="text-align: left; font-weight: 700; color: #FF3300;" Text="Ha Ocurrido un Error!" Visible="False"></asp:Label>
    
        <br />
    
        <div id="container">
            <asp:TextBox ID="TextBox1" runat="server"   TextMode="MultiLine" ReadOnly="True" ></asp:TextBox>
            </div>
        <div>
             <br />
             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Index.aspx">Auntenticar con Token</asp:HyperLink>
           
            
        </div>

        

    </div>  
    </form>
</body>
</html>
