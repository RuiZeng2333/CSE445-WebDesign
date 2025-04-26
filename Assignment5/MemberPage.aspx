<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberPage.aspx.cs" Inherits="Assignment5WebApp.MemberPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome home, members!</title>
    <style>
        body{
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
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
        text-align:center;
        }

        h2{
        color: #333333;
        margin-bottom:20px;
        }

        .card{
        border:1px solid #dddddd;
        border-radius:10px;
        padding:20px;
        text-align:left;
        background-color:#fefefe;
        }

        .card-header{
        background-color:#007bff;
        color:white;
        padding:10px;
        border-top-left-radius: 10px;
        border-top-right-radius:10px;
        font-weight:bold;
        }

        .card-body{
        padding:15px;
        }
        .info-item{
        margin-bottom:10px;
        }

        .info-item strong{
        color:#333333;
        }
        .logout-btn, .home-btn{
            padding:10px 20px;
            /*bckground-color: #dc3545;
            color:white;
            border:none;
            border-radius:5px;
            font-size: 16px;
            cursor:pointer;
            margin-top:20px;*/
            border:none;
            border-radius:5px;
            font-size:16px;
            cursor:pointer;
            margin:10px;
        }

            .logout-btn{
                background-color:#dc3545;
                color:white;
            }

            .logout-btn:hover {
                background-color: #c82333;
            }

            .home-btn{
                background-color: #28a745;
                color : white;
            }

            .home-btn:hover{
                background-color: #218838;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class ="container">
            <h2>Welcome to the Member Page!</h2>
            <asp:Label ID="lblWelcome" runat="server" CssClass="h4 d-block mb-4" />

            <div class="card">
                <div class="card-header">
                    Your Membership Information
                    </div>
                <div class="card-body">
                    <p><strong>Email Address:</strong> <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></p>
                    <p><strong>Membership Tier:</strong> <asp:Label ID="lblTier" runat="server" Text=""></asp:Label></p>
                    <p><strong>Discount Percentage:</strong> <asp:Label ID="lblDiscount" runat="server" Text=""></asp:Label></p>
                    </div>
                </div>
            <div style ="text-align:center; margin-top:20px;">
                <!-- <asp:HyperLink NavigateUrl="http://webstrar66.fulton.asu.edu/Page6/CombinedCoupon.aspx" -->
                <asp:HyperLink NavigateUrl="~/CombinedCoupon.aspx" 
              CssClass="home-btn" 
              Text="Go Back to Home Page" 
              runat="server" />
                <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="logout-btn" />
                </div>
        </div>
    </form>
</body>
</html>
