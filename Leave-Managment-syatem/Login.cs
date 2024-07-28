using Leave_Managment_syatem;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Leave_Management_System
{
    public partial class Login : Form
    {
        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=LeaveMS;Integrated Security=True");
        SqlCommand cmd;
        DataTable dt;

        public Login()
        {
            InitializeComponent();
            txtUserNum.Focus();
            this.FormClosed += new FormClosedEventHandler(OnFormClosed);

        }
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserNum.Text == string.Empty || txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Please enter your username and password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserNum.Select();
                return;
            }

            cnct.Open();
            string log = "SELECT * FROM Employees WHERE EmployeeId=@enumber AND Password=@pswd";
            cmd = new SqlCommand(log, cnct);
            cmd.Parameters.AddWithValue("@enumber", txtUserNum.Text);
            cmd.Parameters.AddWithValue("@pswd", txtPassword.Text);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            cnct.Close();

            if (dt.Rows.Count > 0)
            {
                string name = dt.Rows[0]["Name"].ToString();
                string employeeId = dt.Rows[0]["EmployeeId"].ToString();
                string role = dt.Rows[0]["Role"].ToString();

                if (role == "Admin")
                {
                    AdminForm adminForm = new AdminForm(name, employeeId, role);
                    adminForm.Show();
                    this.Hide();
                }
                else if (role == "Employee")
                {
                    EmployeeForm employeeForm = new EmployeeForm(name, employeeId, role);
                    employeeForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid user role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid login credentials. Please check your username and password and try again.", "Invalid Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserNum.Clear();
                txtPassword.Clear();
                txtUserNum.Select();
            }
        }
    }
}
