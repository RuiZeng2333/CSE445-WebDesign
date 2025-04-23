<%@ Page 
    Title="Staff Page" 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="StaffPage.aspx.cs" 
    Inherits="Assignment5.StaffPage" 
%>
<!DOCTYPE html>
<html lang="en">
<head runat="server"><meta charset="utf-8" /><title>Staff Page</title></head>
<body>
  <form id="form1" runat="server" class="container mt-5">
    <h2>Staff Page</h2>
    <p>Welcome to the staff area!</p>
      <p>
  <asp:HyperLink 
    runat="server"
    NavigateUrl="~/Default.aspx"
    CssClass="btn btn-secondary">
    ← Back to Home
  </asp:HyperLink>
</p>

  </form>
</body>
</html>
