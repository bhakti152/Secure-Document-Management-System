<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThemeChange.aspx.cs" Inherits="ThemeChange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 40px">
      <asp:ImageButton ID="ImageButton1" runat="server" Height="200px" Width="200px" ImageUrl="~/Images/t1.jpg" OnClick="ImageButton1_Click"/> &nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/t2.jpg" Height="200px" Width="200px" OnClick="ImageButton2_Click" />&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/t3.jpg" Height="200px" Width="200px" OnClick="ImageButton3_Click" />&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/t4.jpg" Height="200px" Width="200px" OnClick="ImageButton4_Click" />&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/t5.jpg" Height="200px" Width="200px" OnClick="ImageButton5_Click" />
        <br />
        <br/>
        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/t6.jpg" Height="200px" Width="200px" OnClick="ImageButton6_Click" />&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/t7.jpg" Height="200px" Width="200px" OnClick="ImageButton7_Click" />&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Images/t8.jpg" Height="200px" Width="200px" OnClick="ImageButton8_Click"/>&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/t9.jpg" Height="200px" Width="200px" OnClick="ImageButton9_Click"/>&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Images/t10.jpg" Height="200px" Width="200px" OnClick="ImageButton10_Click" /><br/>
       &nbsp;<br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="214px" />
    
    </div>
    </form>
</body>
</html>
