<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SSO_GAD.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GOanySSO portal</title>

    <style type="text/css">
               /* Basics */
        html, body {
            margin:0;
            padding:0;
            height: 100%;
            font-family: "Helvetica Neue", Helvetica, sans-serif;
            color: #444;
            -webkit-font-smoothing: antialiased;
            background: #fff;
        }

        #PanelPadre {

                    }

        #panel {
            position:absolute;
            left:50%;
            top:50%;
            height: 350px;
           width: 340px;
           margin-top:-150px;
           margin-left:-170px;
           overflow:hidden;
            /*border: 2px solid Red;*/
            text-align: center;
  
        }

        #container {
            width: 335px;
            height: 210px;
            background-color: #f3f8ff;
            border: 1px solid #93b8d8;
            border-radius: 3px;
            /*box-shadow: 0 1px 2px rgba(0, 0, 0, .1);*/
            text-align: left;
            box-shadow: inset 0 1.5px 3px rgba(190, 190, 190, .4), 0 0 0 5px #f5f7f8;
        }

        form {
            margin: 0 auto;
            margin-top: 0px;
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
            font-size: 18px;
            outline: none;
        }

            input[type=text],
            input[type=password] {
                color: #777;
                padding-left: 10px;
                margin-right: 5px;
                margin-top: 7px;
                margin-left: 18px;
                width: 280px;
                height: 35px;
                border: 1px solid #c7d0d2;
                border-radius: 2px;
                box-shadow: inset 0 1.5px 3px rgba(190, 190, 190, .4), 0 0 0 5px #f5f7f8;
                -webkit-transition: all .4s ease;
                -moz-transition: all .4s ease;
                transition: all .4s ease;
            }

                input[type=text]:hover,
                input[type=password]:hover {
                    border: 1px solid #b6bfc0;
                    box-shadow: inset 0 1.5px 3px rgba(190, 190, 190, .7), 0 0 0 5px #f5f7f8;
                }

                input[type=text]:focus,
                input[type=password]:focus {
                    border: 1px solid #a8c9e4;
                    box-shadow: inset 0 1.5px 3px rgba(190, 190, 190, .4), 0 0 0 5px #e6f2f9;
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

        input[type=checkbox] {
            margin-left: 20px;
            margin-top: 30px;
        }

        .check {
            margin-left: 3px;
            font-size: 11px;
            color: #444;
            text-shadow: 0 1px 0 #fff;
        }

        input[type=submit] {
            float: right;
            margin-right: 15px;
            margin-top: 10px;
            width: 100px;
            height: 30px;
            font-size: 14px;
            font-weight: bold;
            color: #fff;
            background-color: #acd6ef; /*IE fallback*/
            background-image: -webkit-gradient(linear, left top, left bottom, from(#acd6ef), to(#6ec2e8));
            background-image: -moz-linear-gradient(top left 90deg, #acd6ef 0%, #6ec2e8 100%);
            background-image: linear-gradient(top left 90deg, #acd6ef 0%, #6ec2e8 100%);
            border: 1px solid #66add6;
            box-shadow: 0 1px 2px rgba(0, 0, 0, .3), inset 0 1px 0 rgba(255, 255, 255, .5);
            cursor: pointer;
        }

            input[type=submit]:hover {
                background-image: -webkit-gradient(linear, left top, left bottom, from(#b6e2ff), to(#6ec2e8));
                background-image: -moz-linear-gradient(top left 90deg, #b6e2ff 0%, #6ec2e8 100%);
                background-image: linear-gradient(top left 90deg, #b6e2ff 0%, #6ec2e8 100%);
            }

            input[type=submit]:active {
                background-image: -webkit-gradient(linear, left top, left bottom, from(#6ec2e8), to(#b6e2ff));
                background-image: -moz-linear-gradient(top left 90deg, #6ec2e8 0%, #b6e2ff 100%);
                background-image: linear-gradient(top left 90deg, #6ec2e8 0%, #b6e2ff 100%);
            }
            .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
        .loading {
            font-family: Arial;
            font-size: 10pt;
            /*border: 5px solid #67CFF5;
              border: 1px solid #93b8d8;  
            box-shadow: inset 0 1.5px 3px rgba(190, 190, 190, .4), 0 0 0 5px #f5f7f8;*/
            border: 5px solid #93b8d8; 
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }


    </style>
    
    <script src="Scripts/jquery-1.8.2.min.js"></script>

  <script type="text/javascript">
      function ShowProgress() {
          setTimeout(function () {
              var modal = $('<div />');
              modal.addClass("modal");
              $('body').append(modal);
              var loading = $(".loading");
              loading.show();
              var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
              var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
              loading.css({ top: top, left: left });
          }, 200);
      }
      $('form').live("submit", function () {
          ShowProgress();
      });
</script>

</head>
<body><form id="form2" runat="server">
    <div id="PanelPadre">
    <div id="panel">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/logo.png" />
        <br />
        <br />

        <div id="container">
            



                <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/welcome.aspx" OnAuthenticate="Login1_Authenticate" LoginButtonText="Enviar" PasswordLabelText="Token" PasswordRequiredErrorMessage="Requiere Token." TitleText="" UserNameLabelText="Usuario" UserNameRequiredErrorMessage="Requiere Contraseña." DisplayRememberMe="False" FailureText="Usuario o Token Incorrecto" TextLayout="TextOnTop">
                    <LayoutTemplate>
                        <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                            <tr>
                                <td>
                                    <table cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuario</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="color: Red;">
                                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Requiere Contraseña." ToolTip="Requiere Contraseña." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Token</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="color: Red;">
                                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Requiere Token." ToolTip="Requiere Token." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                        <tr id="trbutton">

                                            <td>
                                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Enviar" ValidationGroup="Login1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr id="trerr">
                                            <td align="center" style="color: Red;">
                                                <br />
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>


                                    </table>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <FailureTextStyle HorizontalAlign="Center" />
                </asp:Login>


            
        </div>
    </div></div>
    
    <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="img/loader.gif" alt="" />
</div>

</form>
</body>
</html>
