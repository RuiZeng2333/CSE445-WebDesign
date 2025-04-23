<%@ Page 
    Title="Home" 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="Default.aspx.cs" 
    Inherits="Assignment5.Default" 
%>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Service Directory</title>
    <!-- your CSS references here (e.g. Bootstrap) -->
</head>
<body>
    <form id="form1" runat="server">
      <main aria-labelledby="title">
        <h2 id="title">Service Directory</h2>

        <!-- Authentication Status -->
        <asp:Panel ID="pnlLoggedIn" runat="server" Visible="false">
          <div class="alert alert-info">
            Welcome, <asp:Label ID="lblUsername" runat="server"></asp:Label>!
            <asp:Button 
              ID="btnLogout" 
              runat="server" 
              Text="Logout" 
              OnClick="btnLogout_Click" 
              CssClass="btn btn-sm btn-secondary ml-2" />
          </div>
        </asp:Panel>

        <!-- Hash Function Tester -->
        <div class="card mb-4">
          <div class="card-body">
            <h4 class="card-title">Hash Function Test</h4>
            <div class="form-group">
              <asp:TextBox 
                ID="txtInput" 
                runat="server" 
                CssClass="form-control" 
                placeholder="Enter text to hash" />
            </div>
            <asp:Button 
              ID="btnTryIt" 
              runat="server" 
              Text="TryIt - Generate Hash" 
              OnClick="btnTryIt_Click" 
              CssClass="btn btn-primary" />
            <div class="mt-2">
              <strong>Result:</strong>
              <asp:Label 
                ID="lblHashResult" 
                runat="server" 
                CssClass="text-monospace" />
                <br /><br /><br />
            </div>
          </div>
        </div>

          <!-- WebDownload Buttons -->
        <div class="mt-5 mb-5">
            <asp:Button 
               ID="btnWebDownloadTryIt" 
                runat="server"
                Text="TryIt – Web Download" 
                CssClass="btn btn-info d-block py-3" 
                OnClick="btnWebDownloadTryIt_Click" />
            <br />
            <br />
            <br />
        </div>

          <div style="background: black; height: 5px; margin: 20px 0;"></div>

        <!-- Service Access Buttons -->
<div class="d-flex gap-2">
    <asp:Button 
        ID="btnMember" 
        runat="server" 
        Text="Member Services" 
        CssClass="btn btn-success"
        OnClick="btnMember_Click" />

    <asp:Button 
        ID="btnStaff" 
        runat="server" 
        Text="Staff Services" 
        CssClass="btn btn-warning"
        OnClick="btnStaff_Click" />
</div>

        <!-- Service Directory Table -->
        <div class="mt-4">
          <h4>Component Directory</h4>
          <table class="table table-bordered">
            <thead class="thead-light">
              <tr>
                <th>Component</th>
                <th>Type</th>
                <th>Provider</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>Password Hashing</td>
                <td>DLL Library</td>
                <td>[Rui Zeng]</td>
              </tr>
              <tr>
                <td>Session Management</td>
                <td>Global.asax</td>
                <td>[Rui Zeng]</td>
              </tr>
                <tr>
                    <td>Website Download</td>
                    <td>WebDownloadTryIt.aspx</td>
                    <td>[Rui Zeng]</td>
                </tr>
            </tbody>
          </table>
        </div>
      </main>
    </form>
</body>
</html>
