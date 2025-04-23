<%@ Page 
    Title="Log in" 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="Login.aspx.cs" 
    Inherits="Assignment5.Account.Login"
    UnobtrusiveValidationMode="None"  %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Log in</title>
    <!-- add your CSS references here, e.g. Bootstrap -->
    <link href="~/Scripts/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
      <main aria-labelledby="title" class="container mt-5">
        <h2 id="title">Use a local account to log in</h2>
        <hr />

        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
          <p class="text-danger">
            <asp:Literal runat="server" ID="FailureText" />
          </p>
        </asp:PlaceHolder>

        <div class="form-group row">
          <asp:Label 
            runat="server" 
            AssociatedControlID="Email" 
            CssClass="col-sm-2 col-form-label">
            Email
          </asp:Label>
          <div class="col-sm-6">
            <asp:TextBox 
              runat="server" 
              ID="Email" 
              CssClass="form-control" 
              TextMode="Email" />
            <asp:RequiredFieldValidator 
              runat="server" 
              ControlToValidate="Email"
              CssClass="text-danger" 
              ErrorMessage="The email field is required." />
          </div>
        </div>

        <div class="form-group row">
          <asp:Label 
            runat="server" 
            AssociatedControlID="Password" 
            CssClass="col-sm-2 col-form-label">
            Password
          </asp:Label>
          <div class="col-sm-6">
            <asp:TextBox 
              runat="server" 
              ID="Password" 
              TextMode="Password" 
              CssClass="form-control" />
            <asp:RequiredFieldValidator 
              runat="server" 
              ControlToValidate="Password" 
              CssClass="text-danger" 
              ErrorMessage="The password field is required." />
          </div>
        </div>

        <div class="form-group row">
          <div class="offset-sm-2 col-sm-6">
            <div class="form-check">
              <asp:CheckBox 
                runat="server" 
                ID="RememberMe" 
                CssClass="form-check-input" />
              <asp:Label 
                runat="server" 
                AssociatedControlID="RememberMe" 
                CssClass="form-check-label">
                Remember me?
              </asp:Label>
            </div>
          </div>
        </div>

        <div class="form-group row">
          <div class="offset-sm-2 col-sm-6">
            <asp:Button 
              ID="LogInButton"
              runat="server" 
              OnClick="LogIn" 
              Text="Log in" 
              CssClass="btn btn-primary" />
          </div>
        </div>

        <div class="offset-sm-2 col-sm-6">
          <asp:HyperLink 
            runat="server" 
            ID="RegisterHyperLink" 
            NavigateUrl="~/Account/Register.aspx"
            CssClass="btn btn-link">
            Register as a new user
          </asp:HyperLink>
        </div>

      </main>
    </form>
</body>
</html>
