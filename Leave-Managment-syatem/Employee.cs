using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Leave_Management_System
{
    public partial class EmployeeForm : Form
    {
        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=LeaveMS;Integrated Security=True");
        private string name;
        private string employeeId;
        private string role;

        public EmployeeForm(string name, string employeeId, string role)
        {
            InitializeComponent();
            this.name = name;
            this.employeeId = employeeId;
            this.role = role;
            DisplayUserDetails();
            LoadLeaveHistory();
            InitializeListView();
            LoadRemainingLeaves();
            this.FormClosed += new FormClosedEventHandler(OnFormClosed);
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void DisplayUserDetails()
        {
            lblName.Text = $"Name: {name}";
            lblEmployeeId.Text = $"Employee ID: {employeeId}";
            lblRole.Text = $"Role: {role}";
        }

        private void LoadRemainingLeaves()
        {
            string query = "SELECT AnnualLeaves, CasualLeaves, ShortLeaves FROM Employees WHERE EmployeeId=@employeeId";
            using (SqlCommand cmd = new SqlCommand(query, cnct))
            {
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                try
                {
                    if (cnct.State == ConnectionState.Closed)
                    {
                        cnct.Open();
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblAnnualLeaves.Text = $"Annual Leaves: {reader["AnnualLeaves"]}";
                        lblCasualLeaves.Text = $"Casual Leaves: {reader["CasualLeaves"]}";
                        lblShortLeaves.Text = $"Short Leaves: {reader["ShortLeaves"]}";
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching remaining leaves: " + ex.Message);
                }
                finally
                {
                    if (cnct.State == ConnectionState.Open)
                    {
                        cnct.Close();
                    }
                }
            }
        }

        private void InitializeListView()
        {
            listViewLeaveHistory.View = View.Details;
            listViewLeaveHistory.Columns.Add("Leave Type", 100, HorizontalAlignment.Left);
            listViewLeaveHistory.Columns.Add("Start Date", 100, HorizontalAlignment.Left);
            listViewLeaveHistory.Columns.Add("End Date", 100, HorizontalAlignment.Left);
            listViewLeaveHistory.Columns.Add("Status", 80, HorizontalAlignment.Left);
        }


        private void LoadLeaveHistory()
        {
            string query = "SELECT LeaveId, LeaveType, StartDate, EndDate, Status FROM LeaveApplications WHERE EmployeeId=@employeeId";
            using (SqlCommand cmd = new SqlCommand(query, cnct))
            {
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                try
                {
                    if (cnct.State == ConnectionState.Closed)
                    {
                        cnct.Open();
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        listViewLeaveHistory.Items.Clear();
                        while (reader.Read())
                        {
                            int leaveId = reader.GetInt32(reader.GetOrdinal("LeaveId"));
                            string leaveType = reader.GetString(reader.GetOrdinal("LeaveType"));
                            DateTime startDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                            DateTime endDate = reader.GetDateTime(reader.GetOrdinal("EndDate"));
                            string status = reader.GetString(reader.GetOrdinal("Status"));

                            ListViewItem item = new ListViewItem(leaveType);
                            item.SubItems.Add(startDate.ToString("yyyy-MM-dd"));
                            item.SubItems.Add(endDate.ToString("yyyy-MM-dd"));
                            item.SubItems.Add(status);
                            item.Tag = leaveId; // Storing LeaveId for future reference

                            listViewLeaveHistory.Items.Add(item);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching leave history: " + ex.Message);
                }
                finally
                {
                    if (cnct.State == ConnectionState.Open)
                    {
                        cnct.Close();
                    }
                }
            }
        }



        private void btnDeleteLeave_Click(object sender, EventArgs e)
        {
            if (listViewLeaveHistory.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a leave application to delete.");
                return;
            }

            ListViewItem selectedItem = listViewLeaveHistory.SelectedItems[0];

            // Ensure the Tag property contains a valid integer
            if (selectedItem.Tag == null || !int.TryParse(selectedItem.Tag.ToString(), out int leaveId))
            {
                MessageBox.Show("Invalid leave ID format.");
                return;
            }

            string leaveStatus = selectedItem.SubItems[3].Text; // Assuming status is the 4th column

            if (leaveStatus == "Approved"||leaveStatus=="Rejected")
            {
                MessageBox.Show("Leave application is Stored and cannot be deleted.");
                return;
            }

            string query = "DELETE FROM LeaveApplications WHERE LeaveId = @leaveId AND Status = 'Pending'";

            using (SqlCommand cmd = new SqlCommand(query, cnct))
            {
                cmd.Parameters.AddWithValue("@leaveId", leaveId);

                try
                {
                    if (cnct.State == ConnectionState.Closed)
                    {
                        cnct.Open();
                    }
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Leave application deleted successfully!");
                    LoadLeaveHistory(); // Refresh the leave history
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting leave application: " + ex.Message);
                }
                finally
                {
                    if (cnct.State == ConnectionState.Open)
                    {
                        cnct.Close();
                    }
                }
            }
        }



        private void btnApplyLeave_Click_1(object sender, EventArgs e)
        {
            if (cmbLeaveType.SelectedItem == null || string.IsNullOrEmpty(cmbLeaveType.SelectedItem.ToString()))
            {
                MessageBox.Show("Please select a leave type.");
                return;
            }

            string leaveType = cmbLeaveType.SelectedItem.ToString();
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;
            string status = "Pending";

            if (leaveType == "Annual Leave" && (startDate - DateTime.Now).TotalDays < 7)
            {
                MessageBox.Show("Annual leave must be applied at least 7 days in advance.");
                return;
            }

            if (leaveType == "Short Leave" && (endDate - startDate).TotalMinutes > 90)
            {
                MessageBox.Show("Short leave duration cannot exceed 1 hour and 30 minutes.");
                return;
            }

            string query = @"
        INSERT INTO LeaveApplications (EmployeeId, LeaveType, StartDate, EndDate, Status) 
        VALUES (@employeeId, @leaveType, @startDate, @endDate, @status)";

            using (SqlCommand cmd = new SqlCommand(query, cnct))
            {
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                cmd.Parameters.AddWithValue("@leaveType", leaveType);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@status", status);

                try
                {
                    if (cnct.State == ConnectionState.Closed)
                    {
                        cnct.Open();
                    }
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Leave applied successfully!");
                    LoadLeaveHistory(); // Refresh the leave history
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error applying for leave: " + ex.Message);
                }
                finally
                {
                    if (cnct.State == ConnectionState.Open)
                    {
                        cnct.Close();
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
