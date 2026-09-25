<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EventRegistrationPortal.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Online Event Registration Portal</h2>

<p>
    Full Name: <br/>
    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required!" ForeColor="Red"></asp:RequiredFieldValidator>
</p>

<p>
    Email Address: <br/>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format!" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
</p>

<p>
    Phone Number: <br/>
    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone is required!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    <!-- Ye check karega ki phone number exactly 10 digits ka ho -->
    <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Enter valid 10-digit number!" ForeColor="Red" ValidationExpression="^[0-9]{10}$" Display="Dynamic"></asp:RegularExpressionValidator>
</p>

<p>
    Age: <br/>
    <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge" ErrorMessage="Age is required!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    <!-- Ye check karega ki age 18 se 60 ke beech mein ho -->
    <asp:RangeValidator ID="rvAge" runat="server" ControlToValidate="txtAge" ErrorMessage="Age must be between 18 and 60!" ForeColor="Red" MinimumValue="18" MaximumValue="60" Type="Integer" Display="Dynamic"></asp:RangeValidator>
</p>

<p>
    Select Event City: <br/>
    <asp:DropDownList ID="ddlCity" runat="server">
        <asp:ListItem Value="">--Select City--</asp:ListItem>
        <asp:ListItem Value="Rajkot">Rajkot</asp:ListItem>
        <asp:ListItem Value="Vadodara">Vadodara</asp:ListItem>
        <asp:ListItem Value="New Delhi">New Delhi</asp:ListItem>
    </asp:DropDownList>
    <!-- Ye check karega ki user ne dropdown list se koi city select ki hai ya nahi -->
    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" ErrorMessage="Please select a city!" ForeColor="Red" InitialValue=""></asp:RequiredFieldValidator>
</p>

    <p>
    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
</p>
<!-- Naya Success Label Yahan Add Karein -->
<p>
    <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Font-Bold="true" Font-Size="Large"></asp:Label>
</p>

        </div>
    </form>
</body>
</html>
