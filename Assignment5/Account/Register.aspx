<%@ Page 
    Title="Register" 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="Register.aspx.cs" 
    Inherits="Assignment5.Account.Register"
    UnobtrusiveValidationMode="None"  %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <meta charset="utf-8" />
  <title>Register</title>
  <link href="~/Scripts/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
  <form id="form1" runat="server" class="container mt-5">
    <main aria-labelledby="regTitle">
      <h2 id="regTitle">Register as a New User</h2>
      <hr />

      <!-- Error message -->
      <p class="text-danger">
        <asp:Label 
          runat="server" 
          ID="ErrorMessage" 
          CssClass="text-danger" 
          Visible="false" />
      </p>

      <!-- Email as Username -->
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
            ErrorMessage="Email is required." />
        </div>
      </div>

      <!-- Password + Confirm -->
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
            ErrorMessage="Password is required." />
        </div>
      </div>

      <div class="form-group row">
        <asp:Label 
          runat="server" 
          AssociatedControlID="ConfirmPassword" 
          CssClass="col-sm-2 col-form-label">
          Confirm Password
        </asp:Label>
        <div class="col-sm-6">
          <asp:TextBox 
            runat="server" 
            ID="ConfirmPassword" 
            TextMode="Password" 
            CssClass="form-control" />
          <asp:RequiredFieldValidator
            runat="server"
            ControlToValidate="ConfirmPassword"
            CssClass="text-danger"
            ErrorMessage="Confirmation is required." />
          <asp:CompareValidator
            runat="server"
            ControlToCompare="Password"
            ControlToValidate="ConfirmPassword"
            CssClass="text-danger"
            ErrorMessage="Passwords must match." />
        </div>
      </div>

      <!-- Register Button -->
      <div class="form-group row">
        <div class="offset-sm-2 col-sm-6">
          <asp:Button
            runat="server"
            ID="btnRegister"
            Text="Register"
            OnClick="RegisterUser"
            CssClass="btn btn-primary" />
        </div>
      </div>

      <!-- Back to Login -->
      <div class="offset-sm-2 col-sm-6">
        <asp:HyperLink
          runat="server"
          NavigateUrl="~/Account/Login.aspx"
          CssClass="btn btn-link">
          Already have an account? Log in
        </asp:HyperLink>
      </div>
    </main>
  </form>
</body>
</html>
