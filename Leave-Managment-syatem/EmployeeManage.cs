using Leave_Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Leave_Managment_System
{
    public partial class EmployeeManageForm : Form
    {
        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=LeaveMS;Integrated Security=True");

        public EmployeeManageForm()
        {
            InitializeComponent();
            InitializeListView();
            LoadEmployeeData();
            this.FormClosed += new FormClosedEventHandler(OnFormClosed);

        }

        private void InitializeListView()
        {
            listViewEmployees.View = View.Details;
            listViewEmployees.FullRowSelect = true;
            listViewEmployees.Columns.Add("Employee ID", 100, HorizontalAlignment.Left);
            listViewEmployees.Columns.Add("Name", 150, HorizontalAlignment.Left);
            listViewEmployees.Columns.Add("Annual Leaves", 100, HorizontalAlignment.Left);
            listViewEmployees.Columns.Add("Casual Leaves", 100, HorizontalAlignment.Left);
            listViewEmployees.Columns.Add("Short Leaves", 100, HorizontalAlignment.Left);
            listViewEmployees.Columns.Add("Role", 100, HorizontalAlignment.Left);
            listViewEmployees.Columns.Add("Password", 100, HorizontalAlignment.Left);
            listViewEmployees.SelectedIndexChanged += ListViewEmployees_SelectedIndexChanged;
        }
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void LoadEmployeeData()
        {
            string query = "SELECT EmployeeId, Name, AnnualLeaves, CasualLeaves, ShortLeaves, Role, Password FROM Employees";
            using (SqlCommand cmd = new SqlCommand(query, cnct))
            {
                try
                {
                    if (cnct.State == ConnectionState.Closed)
                    {
                        cnct.Open();
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        listViewEmployees.Items.Clear();
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["EmployeeId"].ToString());
                            item.SubItems.Add(reader["Name"].ToString());
                            item.SubItems.Add(reader["AnnualLeaves"].ToString());
                            item.SubItems.Add(reader["CasualLeaves"].ToString());
                            item.SubItems.Add(reader["ShortLeaves"].ToString());
                            item.SubItems.Add(reader["Role"].ToString());
                            item.SubItems.Add(reader["Password"].ToString());

                            listViewEmployees.Items.Add(item);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching employee data: " + ex.Message);
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

        private void ListViewEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEmployees.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewEmployees.SelectedItems[0];
                txtName.Text = selectedItem.SubItems[1].Text;
                txtAnnualLeaves.Text = selectedItem.SubItems[2].Text;
                txtCasualLeaves.Text = selectedItem.SubItems[3].Text;
                txtShortLeaves.Text = selectedItem.SubItems[4].Text;
                comboBoxRole.Text = selectedItem.SubItems[5].Text;
                txtPassword.Text = selectedItem.SubItems[6].Text;
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtAnnualLeaves.Text, out int annualLeaves) &&
                int.TryParse(txtCasualLeaves.Text, out int casualLeaves) &&
                int.TryParse(txtShortLeaves.Text, out int shortLeaves))
            {
                string name = txtName.Text;
                string role = comboBoxRole.Text;
                string password = txtPassword.Text;

                string query = @"
                    INSERT INTO Employees (Name, AnnualLeaves, CasualLeaves, ShortLeaves, Role, Password)
                    VALUES (@name, @annualLeaves, @casualLeaves, @shortLeaves, @role, @password)";

                using (SqlCommand cmd = new SqlCommand(query, cnct))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@annualLeaves", annualLeaves);
                    cmd.Parameters.AddWithValue("@casualLeaves", casualLeaves);
                    cmd.Parameters.AddWithValue("@shortLeaves", shortLeaves);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@password", password);

                    try
                    {
                        if (cnct.State == ConnectionState.Closed)
                        {
                            cnct.Open();
                        }
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee added successfully!");
                        LoadEmployeeData(); // Refresh the employee list
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding employee: " + ex.Message);
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
            else
            {
                MessageBox.Show("Please enter valid numeric values for leaves.");
            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (listViewEmployees.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an employee to update.");
                return;
            }

            if (int.TryParse(txtAnnualLeaves.Text, out int annualLeaves) &&
                int.TryParse(txtCasualLeaves.Text, out int casualLeaves) &&
                int.TryParse(txtShortLeaves.Text, out int shortLeaves))
            {
                int employeeId = int.Parse(listViewEmployees.SelectedItems[0].Text);
                string name = txtName.Text;
                string role = comboBoxRole.Text;
                string password = txtPassword.Text;

                string query = @"
                    UPDATE Employees 
                    SET Name=@name, AnnualLeaves=@annualLeaves, CasualLeaves=@casualLeaves, ShortLeaves=@shortLeaves, Role=@role, Password=@password
                    WHERE EmployeeId=@employeeId";

                using (SqlCommand cmd = new SqlCommand(query, cnct))
                {
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@annualLeaves", annualLeaves);
                    cmd.Parameters.AddWithValue("@casualLeaves", casualLeaves);
                    cmd.Parameters.AddWithValue("@shortLeaves", shortLeaves);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@password", password);

                    try
                    {
                        if (cnct.State == ConnectionState.Closed)
                        {
                            cnct.Open();
                        }
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee updated successfully!");
                        LoadEmployeeData(); // Refresh the employee list
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating employee: " + ex.Message);
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
            else
            {
                MessageBox.Show("Please enter valid numeric values for leaves.");
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (listViewEmployees.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an employee to delete.");
                return;
            }

            int employeeId = int.Parse(listViewEmployees.SelectedItems[0].Text);

            string query = "DELETE FROM Employees WHERE EmployeeId=@employeeId";

            using (SqlCommand cmd = new SqlCommand(query, cnct))
            {
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                try
                {
                    if (cnct.State == ConnectionState.Closed)
                    {
                        cnct.Open();
                    }
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee deleted successfully!");
                    LoadEmployeeData(); // Refresh the employee list
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting employee: " + ex.Message);
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Example with default values or placeholders
            string name = "Admin";
            string employeeId = "AdminId";
            string role = "Administrator";

            AdminForm adminForm = new AdminForm(name, employeeId, role);
            adminForm.Show();
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
    }
}
