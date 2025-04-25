<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment5WebApp.Login" %>

<%@ Register TagPrefix="uc" TagName="CaptchaCtl" Src="~/CaptchaCtl.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome home, Member!</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 20px;
        }

        .container {
            max-width: 600px;
            margin: 50px auto;
            background-color: #ffffff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            text-align: center;
        }

        h2 {
            color: #333333;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
            text-align: left;
        }

            .form-group label {
                font-weight: bold;
                display: block;
            }

            .form-group input {
                width: 100%;
                padding: 8px;
                margin-top: 5px;
                box-sizing: border-box;
            }

        .btn {
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
        }

            .btn:hover {
                background-color: #0056b3;
            }

        .message {
            margin-top: 15px;
            color: red;
        }

        .catcha-label {
            font-weight: bold;
            display: block;
            margin-bottom: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Welcome home, Member! </h2>
            <div class="form-group">
                <asp:Label ID="labelEmail" runat="server" Text="Email: " />
                <asp:TextBox ID="textEmail" runat="server" Text="" />
            </div>

            <div class="form-group">
                <asp:Label ID="labelPw" runat="server" Text="Password: " />
                <asp:TextBox ID="txtPw" runat="server" Text="IWILLPASS" TextMode="Password" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="Enter the verification code shown below:" CssClass="captcha-label" /><br />
                <uc:CaptchaCtl ID="Captcha1" runat="server" />
            </div>
            <asp:Label ID="labelMessage" runat="server" CssClass="message" />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" />
            <br />
            <br />
            <asp:Button ID="btnRegister" runat="server" Text="Register as New User" CssClass="btn" OnClick="btnRegister_Click" />

            
        </div>
    </form>
</body>
</html>
