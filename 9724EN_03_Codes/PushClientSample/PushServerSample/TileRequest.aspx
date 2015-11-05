<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TileRequest.aspx.cs" Inherits="PushServerSample.TileRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Server URI :<br /> 
        <asp:Label runat="server" ID="lblSubscription" /> <br />
         Front Title : <br />
        <asp:TextBox runat="server" ID="txtTitle" /> <br />
          Front Background Image : <br />
        <asp:TextBox runat="server" ID="txtBackgroundImage" /> <br />
          Front Badge Counter : <br />
        <asp:TextBox runat="server" ID="txtBadgeCounter" /> <br />
          Back Title : <br />
        <asp:TextBox runat="server" ID="txtBackTitle" /> <br />
        Back Background Image : <br />
        <asp:TextBox runat="server" ID="txtBackgroundBackImage" /> <br />
        Back Content : <br />
        <asp:TextBox runat="server" ID="txtBackContent" /> <br />
         <asp:Button ID="btnTile" runat="server" Text="Send Tile Notification" OnClick="ButtonSendTile_Click" />
    </div>
    </form>
</body>
</html>
