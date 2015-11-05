<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="ContactService.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery.js"></script>
    <script src="js/Main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Roll</td>
                    <td><input type="text" id="roll" /></td>
                </tr>
                 <tr>
                    <td>Name</td>
                    <td><input type="text" id="name" /></td>
                </tr>
                 <tr>
                    <td>Age</td>
                    <td><input type="text" id="age" /></td>
                </tr>
                 <tr>
                    <td>Address</td>
                    <td><input type="text" id="address" /></td>
                </tr>
            </table>
            <input type="button" onclick="returnContacts()" value="Get Contacts" />
            <input type="button" onclick="returnContact()" value="Get Contact (by roll)" />
            <input type="button" onclick="postContact()" value="Add contact" />
            <input type="button" onclick="putContact()" value="Update contact" />
            <input type="button" onclick="deleteContact()" value="Delete contact" />
            <table id="output">
                <tr>
                    <th>Roll</th>
                    <th>Name</th>
                    <th>Age</th>
                    <th>Address</th>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
