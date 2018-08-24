<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProfile.aspx.cs" Inherits="ViewProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
         html,body,h2,h3,h5,h6 {font-family: "Roboto", sans-serif ; background-image:url('Images/pattern1.jpg');}

        .loginmodal-container {
          padding: 30px;
          max-width: 500px;
          width: 100% !important;
          background-color: #F7F7F7;
          margin: 0 auto;
          border-radius: 2px;
          box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
          overflow: hidden;
          font-family: roboto;
        }

        .loginmodal-container h1 {
          text-align: center;
          font-size: 1.8em;
          font-family: roboto;
        }

        .loginmodal-container input[type=submit] {
          width: 100%;
          display: block;
          margin-bottom: 10px;
          position: relative;
                    top: 0px;
                    left: 0px;
                    height: 59px;
                }

        .loginmodal-container input[type=text], input[type=password] {
          height: 44px;
          font-size: 16px;
          width: 100%;
          margin-bottom: 10px;
          -webkit-appearance: none;
          background: #fff;
          border: 1px solid #d9d9d9;
          border-top: 1px solid #c0c0c0;
          /* border-radius: 2px; */
          padding: 0 8px;
          box-sizing: border-box;
          -moz-box-sizing: border-box;
        }

        .loginmodal-container input[type=text]:hover, input[type=password]:hover {
          border: 1px solid #b9b9b9;
          border-top: 1px solid #a0a0a0;
          -moz-box-shadow: inset 0 1px 2px rgba(0,0,0,0.1);
          -webkit-box-shadow: inset 0 1px 2px rgba(0,0,0,0.1);
          box-shadow: inset 0 1px 2px rgba(0,0,0,0.1);
        }

        .loginmodal {
          text-align: center;
          font-size: 14px;
          font-family: 'Arial', sans-serif;
          font-weight: 700;
          height: 36px;
          padding: 0 8px;
        /* border-radius: 3px; */
        /* -webkit-user-select: none;
          user-select: none; */
        }

        .loginmodal-submit {
          /* border: 1px solid #3079ed; */
          border: 0px;
          color: #fff;
          text-shadow: 0 1px rgba(0,0,0,0.1); 
          background-color: #4d90fe;
          padding: 17px 0px;
          font-family: roboto;
          font-size: 14px;
          /* background-image: -webkit-gradient(linear, 0 0, 0 100%,   from(#4d90fe), to(#4787ed)); */
        }

        .loginmodal-submit:hover {
          /* border: 1px solid #2f5bb7; */
          border: 0px;
          text-shadow: 0 1px rgba(0,0,0,0.3);
          background-color: #357ae8;
          /* background-image: -webkit-gradient(linear, 0 0, 0 100%,   from(#4d90fe), to(#357ae8)); */
        }

        .loginmodal-container a {
          text-decoration: none;
          color: #666;
          font-weight: 400;
          text-align: center;
          display: inline-block;
          opacity: 0.6;
          transition: opacity ease 0.5s;
        } 

        .login-help{
          font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="loginmodal-container" style="width:500px">
		    <h1>Profile</h1><br/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image1" runat="server" />&nbsp;&nbsp;&nbsp; <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:TextBox ID="txtfname" runat="server" placeholder ="First Name"></asp:TextBox>			  
            <asp:TextBox ID="txtlname" runat="server" placeholder ="Last Name"></asp:TextBox>
            <asp:TextBox ID="txtemail" runat="server" placeholder ="Email Address"></asp:TextBox>
            <asp:TextBox ID="txtphone" runat="server" placeholder ="Telephone No"></asp:TextBox>
            <asp:TextBox ID="txtuname" runat="server" placeholder ="Username" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="txtpswd" runat="server" placeholder ="Password" ></asp:TextBox>
        
           <br />
           <br />
       
            <asp:Button ID="btnsave" runat="server" Text="Save"   CssClass="login loginmodal-submit" OnClick="btnsave_Click"  />					
				  
	    </div>
    </div>
    </form>
</body>
</html>
