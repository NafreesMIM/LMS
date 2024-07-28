using System;

namespace Leave_Management_System
{
    partial class AdminForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblEmployeeId = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.listViewLeaveApplications = new System.Windows.Forms.ListView();
            this.btnApproveLeave = new System.Windows.Forms.Button();
            this.btnRejectLeave = new System.Windows.Forms.Button();
            this.btnEmployee = new System.Windows.Forms.Button();
            this.btnLeaves = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(466, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "label1";
            // 
            // lblEmployeeId
            // 
            this.lblEmployeeId.AutoSize = true;
            this.lblEmployeeId.Location = new System.Drawing.Point(656, 9);
            this.lblEmployeeId.Name = "lblEmployeeId";
            this.lblEmployeeId.Size = new System.Drawing.Size(35, 13);
            this.lblEmployeeId.TabIndex = 4;
            this.lblEmployeeId.Text = "label2";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(834, 9);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(35, 13);
            this.lblRole.TabIndex = 5;
            this.lblRole.Text = "label3";
            // 
            // listViewLeaveApplications
            // 
            this.listViewLeaveApplications.HideSelection = false;
            this.listViewLeaveApplications.Location = new System.Drawing.Point(806, 87);
            this.listViewLeaveApplications.Name = "listViewLeaveApplications";
            this.listViewLeaveApplications.Size = new System.Drawing.Size(121, 97);
            this.listViewLeaveApplications.TabIndex = 6;
            this.listViewLeaveApplications.UseCompatibleStateImageBehavior = false;
            this.listViewLeaveApplications.Visible = false;
            // 
            // btnApproveLeave
            // 
            this.btnApproveLeave.Location = new System.Drawing.Point(806, 397);
            this.btnApproveLeave.Name = "btnApproveLeave";
            this.btnApproveLeave.Size = new System.Drawing.Size(96, 23);
            this.btnApproveLeave.TabIndex = 9;
            this.btnApproveLeave.Text = "Leave Aprove";
            this.btnApproveLeave.UseVisualStyleBackColor = true;
            this.btnApproveLeave.Visible = false;
            this.btnApproveLeave.Click += new System.EventHandler(this.btnApproveLeave_Click_1);
            // 
            // btnRejectLeave
            // 
            this.btnRejectLeave.Location = new System.Drawing.Point(806, 432);
            this.btnRejectLeave.Name = "btnRejectLeave";
            this.btnRejectLeave.Size = new System.Drawing.Size(96, 23);
            this.btnRejectLeave.TabIndex = 10;
            this.btnRejectLeave.Text = "Reject Leave";
            this.btnRejectLeave.UseVisualStyleBackColor = true;
            this.btnRejectLeave.Visible = false;
            // 
            // btnEmployee
            // 
            this.btnEmployee.Location = new System.Drawing.Point(44, 529);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Size = new System.Drawing.Size(208, 107);
            this.btnEmployee.TabIndex = 11;
            this.btnEmployee.Text = "Manage Employee";
            this.btnEmployee.UseVisualStyleBackColor = true;
            this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            // 
            // btnLeaves
            // 
            this.btnLeaves.Location = new System.Drawing.Point(275, 529);
            this.btnLeaves.Name = "btnLeaves";
            this.btnLeaves.Size = new System.Drawing.Size(208, 107);
            this.btnLeaves.TabIndex = 12;
            this.btnLeaves.Text = "Leave History";
            this.btnLeaves.UseVisualStyleBackColor = true;
            this.btnLeaves.Click += new System.EventHandler(this.btnLeaves_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(827, 600);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Logout";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 648);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnLeaves);
            this.Controls.Add(this.btnEmployee);
            this.Controls.Add(this.btnRejectLeave);
            this.Controls.Add(this.btnApproveLeave);
            this.Controls.Add(this.listViewLeaveApplications);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblEmployeeId);
            this.Controls.Add(this.lblName);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin";
            this.Load += new System.EventHandler(this.Admin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmployeeId;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ListView listViewLeaveApplications;
        private System.Windows.Forms.Button btnApproveLeave;
        private System.Windows.Forms.Button btnRejectLeave;
        private System.Windows.Forms.Button btnEmployee;
        private System.Windows.Forms.Button btnLeaves;
        private System.Windows.Forms.Button button2;
    }
}