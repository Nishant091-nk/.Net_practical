using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcademicCalendarSystem
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cookies se purana data read karna (Agar user wapas aaye toh naam aur ID auto-fill ho jaye)
                if (Request.Cookies["UserInfo"] != null)
                {
                    txtID.Text = Request.Cookies["UserInfo"]["ID"];
                    txtName.Text = Request.Cookies["UserInfo"]["Name"];
                }

                // Session mein default leave balance set karna (Sirf pehli baar)
                if (Session["LeaveBalance"] == null)
                {
                    Session["LeaveBalance"] = 12; // Default balance
                }
            }
        }

        // Calendar ke weekends ko disable karne ka code
        protected void AcademicCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsWeekend)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.LightGray;
            }
        }

        // Jab Submit button click hoga
        protected void btnSubmitLeave_Click(object sender, EventArgs e)
        {
            // 1. Validation: Check karna ki koi field khali toh nahi hai
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtReason.Text) || ddlWorkType.SelectedValue == "")
            {
                lblStatus.Text = "Please fill in all details (ID, Name, Role, and Reason).";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int requestedDays = AcademicCalendar.SelectedDates.Count;

            // 2. Validation: Check karna ki date select ki hai ya nahi
            if (requestedDays == 0)
            {
                lblStatus.Text = "Please select at least one date from the calendar.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int currentBalance = Convert.ToInt32(Session["LeaveBalance"]);

            // 3. Leave Process Karna
            if (requestedDays <= currentBalance)
            {
                // Balance update karna (Session)
                Session["LeaveBalance"] = currentBalance - requestedDays;

                // User details browser me save karna (Cookies) - Agle 7 din ke liye
                HttpCookie userCookie = new HttpCookie("UserInfo");
                userCookie["ID"] = txtID.Text;
                userCookie["Name"] = txtName.Text;
                userCookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(userCookie);

                // Success Message dikhana (Saari details ke sath)
                lblStatus.Text = $"<b>Application Submitted Successfully!</b><br/>" +
                                 $"Name: {txtName.Text} ({txtID.Text})<br/>" +
                                 $"Role: {ddlWorkType.SelectedValue}<br/>" +
                                 $"Reason: {txtReason.Text}<br/>" +
                                 $"<br/><b>{requestedDays} day(s) leave approved. Remaining Balance: {Session["LeaveBalance"]} days.</b>";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblStatus.Text = $"Leave Denied: Not enough balance. You have {currentBalance} days left.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}