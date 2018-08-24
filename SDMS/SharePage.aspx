<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SharePage.aspx.cs" Inherits="SharePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="background-image: url('Images/pattern1.jpg')">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" Height="19px" Width="143px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
        &nbsp;&nbsp;<br />
        <br />
        &nbsp;
        <asp:Button ID="Button1" runat="server" Text="Share" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
