<%@ Page 
    Title="Member Page" 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="MemberPage.aspx.cs" 
    Inherits="Assignment5.MemberPage" 
%>
<!DOCTYPE html>
<html lang="en">
<head runat="server"><meta charset="utf-8" /><title>Member Page</title></head>
<body>
  <form id="form1" runat="server" class="container mt-5">
    <h2>Member Page</h2>
    <p>Welcome to the member area!</p>
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
