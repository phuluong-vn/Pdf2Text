<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvertPdf2Text.aspx.cs" Inherits="Pdf2Text.ConvertPdf2Text" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:FileUpload ID="flUploadPdf" runat="server" />
    <br />
    <%--<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
    <br />
    <br />
    <asp:Button ID="btnCovert" runat="server" OnClick="btnCovert_Click" Text="Convert" />
        <br />
    <br />
    <asp:Button ID="btnAddtext" runat="server" OnClick="btnAddtext1_Click" Text="Addtext 1" />
        <br />
    <br />--%>
    <asp:Button ID="btnAddtexttwo" runat="server" OnClick="btnAddtext2_Click" Text="Addtext 2" />
         <br />
    <%--<asp:Button ID="Button1" runat="server" OnClick="btnAddtext3_Click" Text="Addtext 3" />
    </div>--%>
    </form>
</body>
</html>
