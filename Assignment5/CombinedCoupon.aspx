<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CombinedCoupon.aspx.cs" Inherits="Assignment5WebApp.TryIt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hotel Coupon download</title>
    <!-- Let's actually make it looks a bit nicer-->
    <style>
        body{
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin:0;
            padding:0;
            height:100vh;
            display:flex;
            justify-content: center;
            align-items: center;
        }
        .container{
            max-width:700px;
            margin:0 auto;
            background-color:#ffffff;
            padding:30px;
            border-radius:10px;
            box-shadow:0 0 10px rgba(0,0,0,0.1);
        }

        h2{
            text-align: center;
            color:#333333;
        }

        .btn-get-hotels{
            background-color:#007bff;
            color: white;
            border: none;
            padding: 10px 20px;
            font-size:16px;
            border-radius:5px;
            cursor: pointer;
            margin-bottom:20px;
        }

        .coupon-btn {
            background-color: #28a745;
            color: white;
            border: none;
            padding: 5px 12px;
            font-size: 14px;
            border-radius: 5px;
            cursor: not-allowed;
            margin-left: 20px;
        }

.coupon-btn:hover {
    background-color: #218838;
}
        .btn-get-hotels:hover{
            background-color:#0056b3;
        }

        .hotel-list{
            list-style-type: none;
            padding-left: 0;
        }

        .hotel-list li{
            padding: 8px;
            border-bottom: 1px solid #ddd;
            display: flex;
            justify-content:space-between;
            align-items: center;
        }

        .hotel-list li:last-child{
            border-bottom: none;
        }

        .no-result{
            color: red;
            font-weight: bold;
        }

        .top-bar {
            overflow: auto;
            margin-bottom: 20px;
        }
        .login-btn {
            float: right;
            background-color: #ffc107;
            border: none;
            padding: 6px 12px;
            border-radius: 4px;
            cursor: pointer;
         }
        .login-btn:hover {
            background-color: #e0a800;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
      <!-- Log in -->
      <div class="top-bar">
        <asp:Button
          ID="btnLogin"
          runat="server"
          CssClass="login-btn"
          Text="Log in"
          OnClick="btnLogin_Click" />
      </div>

            <asp:Button ID="btnGetHotels" CssClass="btn-get-hotels" runat="server" Text="Donwload your coupon here!" OnClick="btnGetHotels_Click" />
            <br /><br />
            <asp:Literal ID="litResult" runat="server" Mode="PassThrough" />
        </div>
    </form>
</body>
</html>