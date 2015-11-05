<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToastRequest.aspx.cs" Inherits="PushServerSample.Default" %>

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
         Title : <br />
        <asp:TextBox runat="server" ID="txtTitle" /> <br />
         Message : <br />
        <asp:TextBox runat="server" ID="txtMessage" /> <br />
         <asp:Button runat="server" Text="Send Toast Notification" OnClick="ButtonSendToast_Click" />
    </div>
    </form>
</body>
</html>
