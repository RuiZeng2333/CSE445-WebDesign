<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="WebDownloadTryIt.aspx.cs" 
    Inherits="Assignment5.WebForm1" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>WebDownload TryIt</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>WebDownload Service - TryIt Page</h2>

        <!-- 1) URL input -->
        <asp:TextBox 
            ID="txtURL" 
            runat="server" 
            Width="400px" 
            placeholder="Enter a URL (e.g. https://example.com)" />

        <br /><br/>

        <!-- 2) Buttons -->
        <asp:Button 
            ID="btnShowContent" 
            runat="server" 
            Text="Show Content" 
            OnClick="btnShowContent_Click" />

        <asp:Button 
            ID="btnDownload" 
            runat="server" 
            Text="Download" 
            OnClick="btnDownload_Click" />

        <br /><br/>

        <!-- 3) Error label -->
        <asp:Label 
            ID="lblError" 
            runat="server" 
            ForeColor="Red" />

        <br /><br/>

        <!-- 4) Multiline content textbox -->
        <asp:TextBox 
            ID="txtContent" 
            runat="server" 
            TextMode="MultiLine" 
            Rows="15" 
            Columns="80" 
            ReadOnly="true" 
            ValidateRequestMode="Disabled" />
    </form>
</body>
</html>
