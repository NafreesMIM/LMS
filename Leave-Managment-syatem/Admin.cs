using Leave_Managment_syatem;
using Leave_Managment_System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Leave_Management_System
{
    public partial class AdminForm : Form
    {
        private string name;
        private string employeeId;
        private string role;

        public AdminForm(string name, string employeeId, string role)
        {
            InitializeComponent();
            InitializeLeaveApplicationControls();
            this.name = name;
            this.employeeId = employeeId;
            this.role = role;
            DisplayUserDetails();
            this.FormClosed += new FormClosedEventHandler(OnFormClosed);
            LoadEmployeeNames();
            LoadLeaveApplications();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LoadEmployeeNames()
        {
            List<string> employeeNames = GetEmployeeNames();
            // Uncomment this if you need to populate a ComboBox with employee names
            // comboBoxEmployee.Items.Clear();
            // comboBoxEmployee.Items.AddRange(employeeNames.ToArray());
        }

        private List<string> GetEmployeeNames()
        {
            List<string> employeeNames = new List<string>();

            using (SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=LeaveMS;Integrated Security=True"))
            {
                string query = "SELECT Name FROM Employees WHERE Role='Employee'";
                SqlCommand cmd = new SqlCommand(query, cnct);

                try
                {
                    cnct.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employeeNames.Add(reader["Name"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching employee names: " + ex.Message);
                }
            }

            return employeeNames;
        }

        private void DisplayUserDetails()
        {
            lblName.Text = $"Name: {name}";
            lblEmployeeId.Text = $"Employee ID: {employeeId}";
            lblRole.Text = $"Role: {role}";
        }

        private void LoadLeaveApplications()
        {
            string query = @"
        SELECT la.LeaveId, e.Name AS EmployeeName, la.LeaveType, la.StartDate, la.EndDate, la.Status 
        FROM LeaveApplications la
        INNER JOIN Employees e ON la.EmployeeId = e.EmployeeId
        WHERE la.Status = 'Pending'";

            using (SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=LeaveMS;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand(query, cnct);

                try
                {
                    cnct.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    listViewLeaveApplications.Items.Clear();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["LeaveId"].ToString());
                        item.SubItems.Add(reader["EmployeeName"].ToString()); // Use EmployeeName instead of EmployeeId
                        item.SubItems.Add(reader["LeaveType"].ToString());
                        item.SubItems.Add(((DateTime)reader["StartDate"]).ToString("yyyy-MM-dd"));
                        item.SubItems.Add(((DateTime)reader["EndDate"]).ToString("yyyy-MM-dd"));
                        item.SubItems.Add(reader["Status"].ToString());
                        listViewLeaveApplications.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching leave applications: " + ex.Message);
                }
            }
        }


        private void ApproveLeaveApplication(int leaveId)
        {
            string query = "UPDATE LeaveApplications SET Status = 'Approved' WHERE LeaveId = @leaveId;";
            using (SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=LeaveMS;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand(query, cnct);
                cmd.Parameters.AddWithValue("@leaveId", leaveId);

                try
                {
                    cnct.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Leave application approved successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error approving leave application: " + ex.Message);
                }
            }
        }

        private void RejectLeaveApplication(int leaveId)
        {
            string query = "UPDATE LeaveApplications SET Status = 'Rejected' WHERE LeaveId = @leaveId";
            using (SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=LeaveMS;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand(query, cnct);
                cmd.Parameters.AddWithValue("@leaveId", leaveId);

                try
                {
                    cnct.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Leave application rejected successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error rejecting leave application: " + ex.Message);
                }
            }
        }

        private void btnApproveLeave_Click(object sender, EventArgs e)
        {
            if (listViewLeaveApplications.SelectedItems.Count > 0)
            {
                int leaveId = Convert.ToInt32(listViewLeaveApplications.SelectedItems[0].Text);
                ApproveLeaveApplication(leaveId);
                LoadLeaveApplications(); // Refresh the leave applications list
            }
            else
            {
                MessageBox.Show("Please select a leave application to approve.");
            }
        }

        private void btnRejectLeave_Click(object sender, EventArgs e)
        {
            if (listViewLeaveApplications.SelectedItems.Count > 0)
            {
                int leaveId = Convert.ToInt32(listViewLeaveApplications.SelectedItems[0].Text);
                RejectLeaveApplication(leaveId);
                LoadLeaveApplications(); // Refresh the leave applications list
            }
            else
            {
                MessageBox.Show("Please select a leave application to reject.");
            }
        }

        private void InitializeLeaveApplicationControls()
        {
            // Leave Applications ListView
            listViewLeaveApplications = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new System.Drawing.Point(12, 50),
                Size = new System.Drawing.Size(760, 200)
            };
            listViewLeaveApplications.Columns.Add("ID", 50);
            listViewLeaveApplications.Columns.Add("Employee ID", 100);
            listViewLeaveApplications.Columns.Add("Leave Type", 150);
            listViewLeaveApplications.Columns.Add("Start Date", 150);
            listViewLeaveApplications.Columns.Add("End Date", 150);
            listViewLeaveApplications.Columns.Add("Status", 150);

            // Approve Leave Button
            btnApproveLeave = new Button
            {
                Text = "Approve Leave",
                Location = new System.Drawing.Point(12, 20),
                Size = new System.Drawing.Size(150, 25)
            };
            btnApproveLeave.Click += new EventHandler(btnApproveLeave_Click);

            // Reject Leave Button
            btnRejectLeave = new Button
            {
                Text = "Reject Leave",
                Location = new System.Drawing.Point(180, 20),
                Size = new System.Drawing.Size(150, 25)
            };
            btnRejectLeave.Click += new EventHandler(btnRejectLeave_Click);

            Controls.Add(listViewLeaveApplications);
            Controls.Add(btnApproveLeave);
            Controls.Add(btnRejectLeave);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            // Open the EmployeeManageForm
            EmployeeManageForm form = new EmployeeManageForm();
            form.Show();
            this.Hide();
        }

        private void btnLeaves_Click(object sender, EventArgs e)
        {
            LeaveHistory leaveHistory = new LeaveHistory();
            leaveHistory.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnApproveLeave_Click_1(object sender, EventArgs e)
        {
            // This seems to be a duplicate event handler. Make sure this is correctly set up.
        }
    }
}
