<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AcademicCalendarSystem.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Management Form</title>
    <style>
        .form-group { margin-bottom: 15px; }
        label { font-weight: bold; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial; padding: 20px;">
            <h2>Leave Application Form</h2>
            
            <div class="form-group">
                <label>Employee / Student ID:</label><br />
                <asp:TextBox ID="txtID" runat="server" Width="250px"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Full Name:</label><br />
                <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Work Type / Role:</label><br />
                <asp:DropDownList ID="ddlWorkType" runat="server" Width="258px">
                    <asp:ListItem Text="-- Select Role --" Value=""></asp:ListItem>
                    <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                    <asp:ListItem Text="Teaching Faculty" Value="Teaching"></asp:ListItem>
                    <asp:ListItem Text="Non-Teaching Staff" Value="Non-Teaching"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Reason for Leave:</label><br />
                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Rows="3" Width="250px"></asp:TextBox>
            </div>

            <hr />
            <h4>Select Leave Dates from Calendar:</h4>
            
            <asp:Calendar ID="AcademicCalendar" runat="server" 
                OnDayRender="AcademicCalendar_DayRender" 
                BackColor="White" BorderColor="#999999" Font-Names="Verdana" Font-Size="8pt" 
                Height="250px" Width="350px" SelectionMode="DayWeekMonth">
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White"/>
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black"/>
            </asp:Calendar>

            <br />
            <asp:Button ID="btnSubmitLeave" runat="server" Text="Submit Application" OnClick="btnSubmitLeave_Click" Height="30px" />
            <br /><br />
            <asp:Label ID="lblStatus" runat="server" Font-Size="11pt"></asp:Label>
        </div>
    </form>
</body>
</html>