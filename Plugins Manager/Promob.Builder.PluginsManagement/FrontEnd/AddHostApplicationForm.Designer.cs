namespace Promob.Builder.PluginsManagement.FrontEnd
{
    partial class AddHostApplicationForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMinVersion = new System.Windows.Forms.Label();
            this.lblAllowedDistributions = new System.Windows.Forms.Label();
            this.txtMinVersion = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.clbAllowedDistributions = new System.Windows.Forms.CheckedListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.96935F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.03065F));
            this.tableLayoutPanel1.Controls.Add(this.lblMinVersion, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAllowedDistributions, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtMinVersion, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.clbAllowedDistributions, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(261, 212);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblMinVersion
            // 
            this.lblMinVersion.AutoSize = true;
            this.lblMinVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMinVersion.Location = new System.Drawing.Point(3, 26);
            this.lblMinVersion.Name = "lblMinVersion";
            this.lblMinVersion.Size = new System.Drawing.Size(67, 26);
            this.lblMinVersion.TabIndex = 1;
            this.lblMinVersion.Text = "Min. Version";
            this.lblMinVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAllowedDistributions
            // 
            this.lblAllowedDistributions.AutoSize = true;
            this.lblAllowedDistributions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAllowedDistributions.Location = new System.Drawing.Point(3, 52);
            this.lblAllowedDistributions.Name = "lblAllowedDistributions";
            this.lblAllowedDistributions.Size = new System.Drawing.Size(67, 160);
            this.lblAllowedDistributions.TabIndex = 2;
            this.lblAllowedDistributions.Text = "Allowed Distributions";
            this.lblAllowedDistributions.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMinVersion
            // 
            this.txtMinVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMinVersion.Location = new System.Drawing.Point(76, 29);
            this.txtMinVersion.Name = "txtMinVersion";
            this.txtMinVersion.Size = new System.Drawing.Size(182, 20);
            this.txtMinVersion.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(76, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(182, 20);
            this.txtName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(67, 26);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // clbAllowedDistributions
            // 
            this.clbAllowedDistributions.CheckOnClick = true;
            this.clbAllowedDistributions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbAllowedDistributions.FormattingEnabled = true;
            this.clbAllowedDistributions.Location = new System.Drawing.Point(76, 55);
            this.clbAllowedDistributions.Name = "clbAllowedDistributions";
            this.clbAllowedDistributions.Size = new System.Drawing.Size(182, 154);
            this.clbAllowedDistributions.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(199, 231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(118, 231);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddHostApplicationForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddHostApplicationForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblMinVersion;
        private System.Windows.Forms.Label lblAllowedDistributions;
        private System.Windows.Forms.TextBox txtMinVersion;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckedListBox clbAllowedDistributions;
    }
}