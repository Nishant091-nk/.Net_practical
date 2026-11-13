<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="InventoryApp.Inventory" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory Management</title>
    <style>
        /* CSS styling for a modern look */
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f7f6;
            margin: 0;
            padding: 40px;
            display: flex;
            justify-content: center;
        }
        
        .container {
            background-color: #ffffff;
            width: 100%;
            max-width: 800px;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 5px 15px rgba(0, 0, 0, 0.1);
        }

        h2 {
            text-align: center;
            color: #333333;
            margin-bottom: 25px;
            border-bottom: 2px solid #007bff;
            display: inline-block;
            padding-bottom: 5px;
        }

        .form-group {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 15px;
            margin-bottom: 25px;
        }

        /* ASP.NET Textboxes par class lagane ke liye humein code mein CssClass use karna hota hai */
        .input-field {
            width: 100%;
            padding: 12px;
            border: 1px solid #ced4da;
            border-radius: 5px;
            box-sizing: border-box;
            font-size: 14px;
            transition: border-color 0.3s;
        }
        
        .input-field:focus {
            border-color: #007bff;
            outline: none;
        }

        .button-group {
            display: flex;
            gap: 15px;
            justify-content: center;
            margin-bottom: 30px;
        }

        .btn {
            padding: 10px 20px;
            font-size: 15px;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s;
            font-weight: bold;
        }

        .btn-insert { background-color: #28a745; }
        .btn-insert:hover { background-color: #218838; }

        .btn-update { background-color: #ffc107; color: #212529; }
        .btn-update:hover { background-color: #e0a800; }

        .btn-delete { background-color: #dc3545; }
        .btn-delete:hover { background-color: #c82333; }

        /* GridView Styling */
        .styled-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 15px;
            text-align: left;
        }

        .styled-table th {
            background-color: #343a40;
            color: #ffffff;
            padding: 12px 15px;
        }

        .styled-table td {
            padding: 12px 15px;
            border-bottom: 1px solid #dddddd;
        }

        .styled-table tr:nth-of-type(even) {
            background-color: #f8f9fa;
        }

        .styled-table tr:hover {
            background-color: #f1f1f1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div style="text-align: center;">
                <h2>Inventory Dashboard</h2>
            </div>

            <!-- Input Fields -->
            <div class="form-group">
                <asp:TextBox ID="txtItemID" runat="server" CssClass="input-field" Placeholder="Enter Item ID"></asp:TextBox>
                <asp:TextBox ID="txtItemName" runat="server" CssClass="input-field" Placeholder="Enter Item Name"></asp:TextBox>
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="input-field" Placeholder="Enter Quantity"></asp:TextBox>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="input-field" Placeholder="Enter Price"></asp:TextBox>
            </div>

            <!-- Action Buttons -->
            <div class="button-group">
                <asp:Button ID="btnInsert" runat="server" Text="➕ Insert" CssClass="btn btn-insert" OnClick="btnInsert_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="✏️ Update" CssClass="btn btn-update" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="🗑️ Delete" CssClass="btn btn-delete" OnClick="btnDelete_Click" />
            </div>

            <!-- Data Table (GridView) -->
            <asp:GridView ID="GridView1" runat="server" CssClass="styled-table" AutoGenerateColumns="true">
            </asp:GridView>
        </div>
    </form>
</body>
</html>