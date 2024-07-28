namespace Leave_Management_System
{
    partial class EmployeeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAnnualLeaves = new System.Windows.Forms.Label();
            this.lblCasualLeaves = new System.Windows.Forms.Label();
            this.lblShortLeaves = new System.Windows.Forms.Label();
            this.btnApplyLeave = new System.Windows.Forms.Button();
            this.cmbLeaveType = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.listViewLeaveHistory = new System.Windows.Forms.ListView();
            this.lblName = new System.Windows.Forms.Label();
            this.lblEmployeeId = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.btnDeleteLeave = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAnnualLeaves
            // 
            this.lblAnnualLeaves.AutoSize = true;
            this.lblAnnualLeaves.Location = new System.Drawing.Point(60, 66);
            this.lblAnnualLeaves.Name = "lblAnnualLeaves";
            this.lblAnnualLeaves.Size = new System.Drawing.Size(35, 13);
            this.lblAnnualLeaves.TabIndex = 1;
            this.lblAnnualLeaves.Text = "label1";
            // 
            // lblCasualLeaves
            // 
            this.lblCasualLeaves.AutoSize = true;
            this.lblCasualLeaves.Location = new System.Drawing.Point(60, 112);
            this.lblCasualLeaves.Name = "lblCasualLeaves";
            this.lblCasualLeaves.Size = new System.Drawing.Size(35, 13);
            this.lblCasualLeaves.TabIndex = 2;
            this.lblCasualLeaves.Text = "label2";
            // 
            // lblShortLeaves
            // 
            this.lblShortLeaves.AutoSize = true;
            this.lblShortLeaves.Location = new System.Drawing.Point(60, 162);
            this.lblShortLeaves.Name = "lblShortLeaves";
            this.lblShortLeaves.Size = new System.Drawing.Size(35, 13);
            this.lblShortLeaves.TabIndex = 3;
            this.lblShortLeaves.Text = "label3";
            // 
            // btnApplyLeave
            // 
            this.btnApplyLeave.Location = new System.Drawing.Point(148, 352);
            this.btnApplyLeave.Name = "btnApplyLeave";
            this.btnApplyLeave.Size = new System.Drawing.Size(75, 23);
            this.btnApplyLeave.TabIndex = 4;
            this.btnApplyLeave.Text = "Apply";
            this.btnApplyLeave.UseVisualStyleBackColor = true;
            this.btnApplyLeave.Click += new System.EventHandler(this.btnApplyLeave_Click_1);
            // 
            // cmbLeaveType
            // 
            this.cmbLeaveType.FormattingEnabled = true;
            this.cmbLeaveType.Items.AddRange(new object[] {
            "Annual Leave",
            "Casual Leaves",
            "Short Leaves"});
            this.cmbLeaveType.Location = new System.Drawing.Point(67, 220);
            this.cmbLeaveType.Name = "cmbLeaveType";
            this.cmbLeaveType.Size = new System.Drawing.Size(121, 21);
            this.cmbLeaveType.TabIndex = 5;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(143, 276);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(169, 20);
            this.dtpStartDate.TabIndex = 6;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(143, 302);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(169, 20);
            this.dtpEndDate.TabIndex = 7;
            // 
            // listViewLeaveHistory
            // 
            this.listViewLeaveHistory.HideSelection = false;
            this.listViewLeaveHistory.Location = new System.Drawing.Point(395, 58);
            this.listViewLeaveHistory.Name = "listViewLeaveHistory";
            this.listViewLeaveHistory.Size = new System.Drawing.Size(393, 157);
            this.listViewLeaveHistory.TabIndex = 8;
            this.listViewLeaveHistory.UseCompatibleStateImageBehavior = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(146, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "label1";
            // 
            // lblEmployeeId
            // 
            this.lblEmployeeId.AutoSize = true;
            this.lblEmployeeId.Location = new System.Drawing.Point(333, 20);
            this.lblEmployeeId.Name = "lblEmployeeId";
            this.lblEmployeeId.Size = new System.Drawing.Size(35, 13);
            this.lblEmployeeId.TabIndex = 10;
            this.lblEmployeeId.Text = "label2";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(494, 20);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(35, 13);
            this.lblRole.TabIndex = 11;
            this.lblRole.Text = "label3";
            // 
            // btnDeleteLeave
            // 
            this.btnDeleteLeave.Location = new System.Drawing.Point(279, 358);
            this.btnDeleteLeave.Name = "btnDeleteLeave";
            this.btnDeleteLeave.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteLeave.TabIndex = 12;
            this.btnDeleteLeave.Text = "Delete Leave";
            this.btnDeleteLeave.UseVisualStyleBackColor = true;
            this.btnDeleteLeave.Click += new System.EventHandler(this.btnDeleteLeave_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(713, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Logout";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // EmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnDeleteLeave);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblEmployeeId);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.listViewLeaveHistory);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.cmbLeaveType);
            this.Controls.Add(this.btnApplyLeave);
            this.Controls.Add(this.lblShortLeaves);
            this.Controls.Add(this.lblCasualLeaves);
            this.Controls.Add(this.lblAnnualLeaves);
            this.Name = "EmployeeForm";
            this.Text = "Employee";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblAnnualLeaves;
        private System.Windows.Forms.Label lblCasualLeaves;
        private System.Windows.Forms.Label lblShortLeaves;
        private System.Windows.Forms.Button btnApplyLeave;
        private System.Windows.Forms.ComboBox cmbLeaveType;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ListView listViewLeaveHistory;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmployeeId;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Button btnDeleteLeave;
        private System.Windows.Forms.Button button2;
    }
}