<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="gvDashBoard.aspx.cs" Inherits="gvDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <meta charset="UTF-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1"/>
        <link rel="stylesheet" href="https://www.w3schools.com/lib/w3.css"/>
        <link rel="stylesheet" href="https://www.w3schools.com/lib/w3-theme-black.css"/>
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto"/>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
        <style>
            html,body,h1,h2,h3,h5,h6 {font-family: "Roboto", sans-serif ; background-image:url('Images/pattern1.jpg');}
            .w3-sidenav a,.w3-sidenav h4 {padding: 12px;}
            .w3-bar a {
                padding-top: 12px;
                padding-bottom: 12px;
            }
        </style> 
        <%-- Popup for Logout and view Profile --%>
        <style>
            .ribbon {
                position:relative;
                 bottom:-10px;
                right:-80px;
                margin-left:-80px;
            }
            .popup {
  
                position:absolute;top :100px;right:10px;
                display: inline-block;
                cursor: pointer;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
            }

            /* The actual popup */
            .popup .popuptext {
                visibility: hidden;
                width: 160px;
                background-color: #555;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 8px 0;
                position: absolute;
                z-index: 1;
                bottom:-80px;
                right:30%;
                margin-left:-80px;
            }

            /* Popup arrow */
            .popup .popuptext::after {
                content: "";
                position: absolute;
                top: 100%;
                left: 50%;
                margin-left: -5px;
                border-width: 5px;
                border-style: solid;
                border-color: #555 transparent transparent transparent;
            }

            /* Toggle this class - hide and show the popup */
            .popup .show {
                visibility: visible;
                -webkit-animation: fadeIn 1s;
                animation: fadeIn 1s;
            }

            /* Add animation (fade in the popup) */
            @-webkit-keyframes fadeIn {
                from {opacity: 0;} 
                to {opacity: 1;}
            }

            @keyframes fadeIn {
                from {opacity: 0;}
                to {opacity:1 ;}
            }
     </style>   
    <%-- Enf of Popup --%>
    <%-- pop up window display for fileupload --%>
    <script src="scripts/jquery-1.4.3.min.js" type="text/javascript"></script>
    <style type="text/css">
        .web_dialog_overlay
        {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            background: #000000;
            opacity: .15;
            filter: alpha(opacity=15);
            -moz-opacity: .15;
            z-index: 101;
            display: none;
        }
        .web_dialog
        {
            display: none;
            position: fixed;
            width: 380px;
            height: 200px;
            top: 50%;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 102;
            font-family: Verdana;
            font-size: 10pt;
        }
        .web_dialog_title
        {
            border-bottom: solid 2px #336699;
            background-color: #336699;
            padding: 4px;
            color: White;
            font-weight:bold;
        }
        .web_dialog_title a
        {
            color: White;
            text-decoration: none;
        }
        .align_right
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#btnShowSimple").click(function (e) {
                ShowDialog(false);
                e.preventDefault();
            });

            $("#btnShowModal").click(function (e) {
                ShowDialog(true);
                e.preventDefault();
            });

            $("#btnClose").click(function (e) {
                HideDialog();
                e.preventDefault();
            });

            $("#btnSubmit").click(function (e) {
                var brand = $("#brands input:radio:checked").val();
                $("#output").html("<b>Your favorite mobile brand: </b>" + brand);
                HideDialog();
                e.preventDefault();
            });
        });

        function ShowDialog(modal) {
            $("#overlay").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#overlay").unbind("click");
            }
            else {
                $("#overlay").click(function (e) {
                    HideDialog();
                });
            }
        }

        function HideDialog() {
            $("#overlay").hide();
            $("#dialog").fadeOut(300);
        }

    </script>
    <%-- Search Button Design --%>
    <style> 
        input[type=text] {
            width: 130px;
            box-sizing: border-box;
            border: 2px solid #ccc;
            border-radius: 4px;
            font-size: 16px;
            background-color: white;
            background-image: url('searchicon.png');
            background-position: 10px 10px; 
            background-repeat: no-repeat;
            padding: 12px 20px 12px 40px;
            -webkit-transition: width 0.4s ease-in-out;
            transition: width 0.4s ease-in-out;
        }

        input[type=text]:focus {
            width: 50%;
        }
    </style>
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    
    <div id="overlay" class="web_dialog_overlay"></div>
    <%-- Upload File PopUp --%>
    <div id="dialog" class="web_dialog">
        <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
            <tr>
                <td class="web_dialog_title">File Upload</td>
                <td class="web_dialog_title align_right">
                    <a href="#" id="btnClose">Close</a>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 15px;">
                    <b>Please Browse File to Upload </b>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 15px;">
                    <div id="brands">
                        <asp:FileUpload ID="FileUpload1" runat="server"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"/>
                </td>
            </tr>
        </table>
    </div>
    <%-- End of Pop Up --%>
    
   
    
            <div class="w3-top"">
               
              <div class="w3-bar w3-theme w3-top w3-left-align w3-large">
                
                <asp:Button ID="btnZip" runat="server" Text="Zip Folder" Width="110px" OnClick="btnZip_Click" CssClass="w3-bar-item w3-button w3-hide-small w3-hover-white" Font-Bold="True" Font-Size="Medium" Height="38px"/>
                <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="w3-bar-item w3-button w3-hide-small w3-hover-white" Font-Bold="True" Font-Size="Medium" OnClick="btnCopy_Click" Height="40px"/>
                <asp:Button ID="btnPaste" runat="server" Text="Paste" CssClass="w3-bar-item w3-button w3-hide-small w3-hover-white" Font-Bold="True" Font-Size="Medium" OnClick="btnPaste_Click" Height="40px"/>
                <asp:Button runat="server" Text="Move To" ID="btnMoveTo" CssClass="w3-bar-item w3-button w3-hide-small w3-hover-white" Font-Bold="True" Font-Size="Medium" OnClick="btnMoveTo_Click" Height="41px" Width="132px"/>
                <asp:Button ID="btnMoveHere" runat="server" OnClick="btnMoveHere_Click" CssClass="w3-bar-item w3-button w3-hide-small w3-hover-white" Text="Move Here" Font-Bold="True" Font-Size="Medium" Height="41px" Width="116px"/>
                <asp:Button ID="btnNewFolder" runat="server" Text="New Folder" OnClick="btnNewFolder_Click" CssClass="w3-bar-item w3-button w3-hide-small w3-hover-white" Font-Bold="True" Font-Size="Medium" Height="40px" Width="124px"/>
                <input type="button" id="btnShowSimple" value="Upload" class="w3-bar-item w3-button w3-hide-small w3-hover-white"  style="font-size:medium;font-weight:800; height: 34px; width: 93px;"/>
                <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" class="w3-bar-item w3-button w3-hide-small w3-hover-white" OnClick="btnEncrypt_Click"/>
              </div>            
                              
            </div>
         
                <br /><br />
                 <!-- Sidenav -->
           <div id="bg3" runat="server" style="background-image: url('Images/pattern1.jpg'); background-attachment: scroll" > 
                 <nav class="w3-sidenav w3-collapse w3-theme-l5 w3-animate-left" style="border-style: solid; z-index:3; width:250px; margin-top:51px; height:450px; top: 42px; left: 0px; background-color: #999966;" id="mySidenav">
                       <a href="javascript:void(0)" onclick="w3_close()" class="w3-right w3-xlarge w3-padding-large w3-hover-black w3-hide-large" title="close menu">
                            <i class="fa fa-remove"></i>
                       </a>
                       <h4><b>TreeView</b></h4>
                   <!-- TreeView -->
       
                        <asp:TreeView ID="TreeView1" runat="server" NodeIndent="15"   ShowLines="True"  ParentNodeStyle-ImageUrl="~/Images/ic.png" ShowExpandCollapse="true" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" Height="50px" Font-Bold="True" Font-Size="Large" Width="188px">
                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA"  />
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" ImageUrl="~/Images/ic.png"></NodeStyle>
                            <ParentNodeStyle Font-Bold="True" />
                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
                        </asp:TreeView>
                  <!-- End TreeView -->
                </nav>
   
                <!-- Overlay effect when opening sidenav on small screens -->
                <div class="w3-overlay w3-hide-large" onclick="w3_close()" style="cursor:pointer" title="close side menu" id="myOverlay"></div>

                <!-- Main content: shift it to the right by 250 pixels when the sidenav is visible -->
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Image ID="Image1" runat="server" CssClass="ribbon" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <div style="margin-left: 320px">
                    <input type="text" name="txtSearch" placeholder="Search.." id="txtSearch"/>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"  CssClass="btn btn-info" BackColor="Black" ForeColor="White"/> &nbsp;&nbsp;
                    <asp:ImageButton ID="btnSort" runat="server" ImageUrl="~/Images/sort.png" OnClick="btnSort_Click" />&nbsp;&nbsp;
                    <asp:ImageButton ID="btnTheme" runat="server" ImageUrl="~/Images/themes.jpg" OnClick="btnTheme_Click" Height="34px" Width="34px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    
                   
                </div>
                    <div class="w3-main" style="margin-left:290px ">
                    <br />
                    <br />
                     <br />   
                        <div class="popup" onclick="myFunction()" style=" font-weight:bolder"><% =Session["uname"] %>
                            <span class="popuptext" id="myPopup">
                                  <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="w3-bar-item w3-button w3-hide-small w3-hover-white" Font-Bold="True" Font-Size="Medium" BackColor="Black" ForeColor="White" Height="31px" OnClick="btnLogout_Click" />            
                                  <asp:Button ID="btnViewProfile" runat="server" Text="View Profile" CssClass="w3-bar-item w3-button w3-hide-small w3-hover-white" Font-Bold="True" Font-Size="Medium" BackColor="Black" ForeColor="White" Height="31px" OnClick="btnViewProfile_Click"/>
                            </span>
                        </div>
                        <script>
                            // When the user clicks on div, open the popup
                            function myFunction() {
                                var popup = document.getElementById("myPopup");
                                popup.classList.toggle("show");
                            }
                        </script>

                    <br />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="803px" OnRowCommand="GridView1_RowCommand" CellPadding="10" CellSpacing="5"   GridLines="Vertical" AllowPaging="True" AllowSorting="True" PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" BorderColor="Black" BorderWidth="2px" >
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        Select
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("imgUrl") %>' CommandName="Folder" CommandArgument='<%# Eval("fileName") %>' />
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("fileName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        Date Modified
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("fileLastModified") %>' ></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("fileType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        Size
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("fileSize") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        Download
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/download.png" CommandName="xDownload" CommandArgument='<% #Eval("fileName") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        View
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/view.png" CommandName="xView" CommandArgument='<% #Eval("fileName") %>'/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        Delete
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/delete.png" CommandName ="xDelete" CommandArgument='<% #Eval("fileName") %>'  OnClientClick="return confirm('Are you sure you want to delete the file ?');" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        Share
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/delete.png" CommandName ="xShare" CommandArgument='<% #Eval("fileName") %>'  />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
             
                    </div>
                            <br /><br /><br /><br />
               
              

                        <script>
                            // Get the Sidenav
                            var mySidenav = document.getElementById("mySidenav");

                            // Get the DIV with overlay effect
                            var overlayBg = document.getElementById("myOverlay");

                            // Toggle between showing and hiding the sidenav, and add overlay effect
                            function w3_open() {
                                if (mySidenav.style.display === 'block') {
                                    mySidenav.style.display = 'none';
                                    overlayBg.style.display = "none";
                                } else {
                                    mySidenav.style.display = 'block';
                                    overlayBg.style.display = "block";
                                }
                            }

                            // Close the sidenav with the close button
                            function w3_close() {
                                mySidenav.style.display = "none";
                                overlayBg.style.display = "none";
                            }
                        </script>

    
    
                <script type="text/javascript">
                    function OnTreeClick(evt) {
                        var src = window.event != window.undefined ? window.event.srcElement : evt.target;
                        var nodeClick = src.tagName.toLowerCase() == "a";
                        if (nodeClick) {
                            //innerText works in IE but fails in Firefox (I'm sick of browser anomalies), so use innerHTML as well
                            var nodeText = src.innerText || src.innerHTML;
                            var nodeValue = GetNodeValue(src);
                           
                            document.getElementById("TextBox1").value = nodeValue;

                            return true; //comment this if you want postback on node click
                        }
                    }

                    function GetNodeValue(node) {
                        var nodeValue = "";
                        var nodePath = node.href.substring(node.href.indexOf(",") + 2, node.href.length - 2);
                        var nodeValues = nodePath.split("\\");
                        if (nodeValues.length > 1)
                            nodeValue = nodeValues[nodeValues.length - 1];
                        else
                            nodeValue = nodeValues[0].substr(1);

                        return nodeValue;
                    }
                </script>
        
    </div>

</asp:Content>

