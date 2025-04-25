<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaCtl.ascx.cs" Inherits="Assignment5WebApp.WebUserControl1" %>
<!-- Let's implement our code here -->
<asp:Label ID="labelCaptcha" runat="server" Font-Bold="true" />
<asp:TextBox ID="txtCaptchaIn" runat="server" />
<asp:Label ID="labelError" runat="server" ForeColor="Red" />