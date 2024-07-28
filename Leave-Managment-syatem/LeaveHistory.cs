using Leave_Managment_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using SortOrder = System.Windows.Forms.SortOrder;

namespace Leave_Management_System
{
    public partial class LeaveHistory : Form
    {
        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=LeaveMS;Integrated Security=True");
        PrintDocument printDocument = new PrintDocument();
        Font headerFont = new Font("Arial", 12, FontStyle.Bold);
        Font regularFont = new Font("Arial", 10, FontStyle.Regular);
        int currentPageIndex = 0;

        public LeaveHistory()
        {
            InitializeComponent();
            InitializeComboBox();
            InitializeListView();
            this.FormClosed += new FormClosedEventHandler(OnFormClosed);

            // Clear date pickers initially
            dtpStartDate.Value = DateTimePicker.MinimumDateTime;
            dtpEndDate.Value = DateTimePicker.MinimumDateTime;

            // Subscribe to ComboBox SelectedIndexChanged event
            comboBoxEmployees.SelectedIndexChanged += ComboBoxEmployees_SelectedIndexChanged;

            // Initialize PrintDocument
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitializeComboBox()
        {
            comboBoxEmployees.Items.Add("All");

            string query = "SELECT Name FROM Employees";
            using (SqlCommand cmd = new SqlCommand(query, cnct))
            {
                try
                {
                    cnct.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBoxEmployees.Items.Add(reader["Name"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching employee names: " + ex.Message);
                }
                finally
                {
                    if (cnct.State == ConnectionState.Open)
                    {
                        cnct.Close();
                    }
                }
            }

            comboBoxEmployees.SelectedIndex = 0; // Select "All" by default
        }

        private void InitializeListView()
        {
            listViewLeaveHistory.View = View.Details;
            listViewLeaveHistory.Columns.Add("Employee Name", 120, HorizontalAlignment.Left);
            listViewLeaveHistory.Columns.Add("Leave Type", 100, HorizontalAlignment.Left);
            listViewLeaveHistory.Columns.Add("Leave Period", 150, HorizontalAlignment.Left);
            listViewLeaveHistory.Columns.Add("Status", 80, HorizontalAlignment.Left);

            // Set ListView to automatically number rows
            listViewLeaveHistory.FullRowSelect = true;
            listViewLeaveHistory.GridLines = true;
            listViewLeaveHistory.Sorting = SortOrder.Descending;
        }

        private void LoadLeaveHistory(string selectedEmployee)
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            string query;
            if (selectedEmployee == "All")
            {
                query = @"
                    SELECT 
                        (SELECT Name FROM Employees WHERE Employees.EmployeeId = LeaveApplications.EmployeeId) AS EmployeeName, 
                        LeaveType, StartDate, EndDate, Status 
                    FROM LeaveApplications 
                    WHERE StartDate >= @startDate AND EndDate <= @endDate
                    ORDER BY StartDate DESC";
            }
            else
            {
                query = @"
                    SELECT 
                        (SELECT Name FROM Employees WHERE Employees.EmployeeId = LeaveApplications.EmployeeId) AS EmployeeName, 
                        LeaveType, StartDate, EndDate, Status 
                    FROM LeaveApplications 
                    WHERE EmployeeId = (SELECT EmployeeId FROM Employees WHERE Name = @selectedEmployee) 
                          AND StartDate >= @startDate AND EndDate <= @endDate
                    ORDER BY StartDate DESC";
            }

            using (SqlCommand cmd = new SqlCommand(query, cnct))
            {
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                if (selectedEmployee != "All")
                {
                    cmd.Parameters.AddWithValue("@selectedEmployee", selectedEmployee);
                }

                try
                {
                    cnct.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        listViewLeaveHistory.Items.Clear();
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["EmployeeName"].ToString());
                            item.SubItems.Add(reader["LeaveType"].ToString());
                            DateTime start = Convert.ToDateTime(reader["StartDate"]);
                            DateTime end = Convert.ToDateTime(reader["EndDate"]);
                            item.SubItems.Add(start.ToString("yyyy-MM-dd") + " to " + end.ToString("yyyy-MM-dd"));
                            item.SubItems.Add(reader["Status"].ToString());

                            listViewLeaveHistory.Items.Add(item);
                        }
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

        private void ComboBoxEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEmployee = comboBoxEmployees.SelectedItem.ToString();
            LoadLeaveHistory(selectedEmployee);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = "Admin";
            string employeeId = "AdminId";
            string role = "Administrator";

            AdminForm adminForm = new AdminForm(name, employeeId, role);
            adminForm.Show();
            this.Hide();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            EmployeeManageForm form = new EmployeeManageForm();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float startX = e.MarginBounds.Left;
            float startY = e.MarginBounds.Top;
            float currentY = startY;

            // Print header
            string header = "Leave History";
            e.Graphics.DrawString(header, headerFont, Brushes.Black, startX, currentY);
            currentY += headerFont.GetHeight();

            currentY += regularFont.GetHeight(); // Spacing

            // Calculate total width of columns
            float totalColumnWidth = 0;
            foreach (ColumnHeader column in listViewLeaveHistory.Columns)
            {
                totalColumnWidth += column.Width;
            }

            // Calculate available width
            float availableWidth = e.MarginBounds.Width;

            // Calculate scaling ratio
            float scaleRatio = availableWidth / totalColumnWidth;

            // Print ListView headers and items with adjusted widths
            startX = e.MarginBounds.Left;
            foreach (ColumnHeader column in listViewLeaveHistory.Columns)
            {
                int adjustedWidth = (int)(column.Width * scaleRatio);
                e.Graphics.DrawString(column.Text, regularFont, Brushes.Black, startX, currentY);
                startX += adjustedWidth;
            }
            currentY += regularFont.GetHeight(); // Move to next line

            foreach (ListViewItem item in listViewLeaveHistory.Items)
            {
                startX = e.MarginBounds.Left;
                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    int adjustedWidth = (int)(listViewLeaveHistory.Columns[item.SubItems.IndexOf(subItem)].Width * scaleRatio);
                    e.Graphics.DrawString(subItem.Text, regularFont, Brushes.Black, startX, currentY);
                    startX += adjustedWidth; // Use adjusted width
                }
                currentY += regularFont.GetHeight(); // Move to next line
            }

            // Check if more pages are needed
            currentPageIndex++;
            e.HasMorePages = (currentPageIndex * listViewLeaveHistory.Items.Count < listViewLeaveHistory.Items.Count);
        }



    }
}
