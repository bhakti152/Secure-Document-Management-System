<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPage.aspx.cs" Inherits="ViewPage" %>
<%@ Register TagPrefix="GleamTech" Namespace="GleamTech.DocumentUltimate.Web" Assembly="GleamTech.DocumentUltimate" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <GleamTech:DocumentViewer ID="DocumentViewer1" runat="server"   
            Width="800"   
            Height="600"   
             />
        </div>
    </div>
    </form>
</body>
</html>
