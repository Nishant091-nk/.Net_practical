using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace InventoryApp
{
    public partial class Inventory : System.Web.UI.Page
    {
        // Database connection string
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; Database=B2026; Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRecord();
            }
        }

        // Database se data lakar GridView mein dikhane ka function
        void LoadRecord()
        {
            SqlCommand comm = new SqlCommand("select * from Inventory", con);
            SqlDataAdapter d = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            d.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        // Insert Button
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand comm = new SqlCommand("INSERT INTO Inventory VALUES('" + txtItemID.Text + "', '" + txtItemName.Text + "', '" + txtQuantity.Text + "', '" + txtPrice.Text + "')", con);
            comm.ExecuteNonQuery();
            con.Close();
            LoadRecord();
        }

        // Update Button
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand comm = new SqlCommand("UPDATE Inventory SET ItemName='" + txtItemName.Text + "', Quantity='" + txtQuantity.Text + "', Price='" + txtPrice.Text + "' WHERE ItemID='" + txtItemID.Text + "'", con);
            comm.ExecuteNonQuery();
            con.Close();
            LoadRecord();
        }

        // Delete Button
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand comm = new SqlCommand("DELETE FROM Inventory WHERE ItemID='" + txtItemID.Text + "'", con);
            comm.ExecuteNonQuery();
            con.Close();
            LoadRecord();
        }
    }
}