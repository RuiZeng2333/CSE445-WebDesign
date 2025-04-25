<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Assignment5WebApp.Register" %>
<%@ Register TagPrefix="uc" TagName="CaptchaCtl" Src="~/CaptchaCtl.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Yeah! You found us!!! Congrats on becoming our new member!!!</title>
    <style>
        body{
            font-family:Arial, sans-serif;
            background-color:#f5f5f5;
            margin: 0;
            padding: 20px;
        }

        .container{
            max-width:600px;
            margin:50px auto;
            background-color:#ffffff;
            padding:30px;
            border-radius:10px;
            box-shadow:0 0 10px rgba(0,0,0,0.1);
            text-align: center;
        }

        h2{
        color:#333333;
        margin-bottom: 20px;
        }

        .form-group{
        margin-bottom: 15px;
        text-align:left;
        }
        .form-group label{
        font-weight: bold;
        display:block;
        }

        .form-group input{
        width:100%;
        padding: 8px;
        margin-top:5px;
        box-sizing:border-box;
        }

        .btn{
        padding: 10px 20px;
        background-color:#007bff;
        color: white;
        border:none;
        border-radius:5px;
        font-size: 16px;
        cursor: pointer;
        }

        .btn:hover{
        background-color: #0056b3;

        }

        .message{
        margin-top:15px;
        color:red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
        <h2>Register as New Member</h2>

            <div class="form-group">
        <asp:Label ID="lblEmail" runat ="server" Text="Email:" />
        <asp:TextBox ID="textEmail" runat="server" />
            </div>

            <div class="form-group">
        <asp:Label ID="lblPw" runat="server" Text="Password:" />
        <asp:TextBox ID="txtPw" runat="server" TextMode="Password" />
                </div>

            <div class="form-group">
        <asp:Label ID="lblConfirmPw" runat="server" Text="Confirm Password:" />
        <asp:TextBox ID="txtConfirmPw" runat="server" TextMode="Password" />
                </div>

            <div class="form-group">
        <uc:CaptchaCtl ID="Captcha1" runat="server" />
                </div>

        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn" OnClick="btnReg_Click" />
            <br /><br />
        <asp:Label ID="lblMessage" runat="server" CssClass="message" />
        </div>
    </form>
</body>
</html>
