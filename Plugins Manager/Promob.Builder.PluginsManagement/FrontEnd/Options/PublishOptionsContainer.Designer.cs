namespace Promob.Builder.PluginsManagement.FrontEnd.Options
{
    partial class PublishOptionsContainer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtInstallPath = new Promob.Builder.Forms.FolderTextBox();
            this.txtExecutablePath = new Promob.Builder.Forms.FilenameTextBox();
            this.txtLocalMediaPath = new Promob.Builder.Forms.FolderTextBox();
            this.txtBetasPath = new Promob.Builder.Forms.FolderTextBox();
            this.txtEmailList = new System.Windows.Forms.TextBox();
            this.btnContacts = new System.Windows.Forms.Button();
            this.lblEmailList = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(13, 196);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(279, 20);
            this.txtEmail.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(11, 180);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email:";
            // 
            // txtInstallPath
            // 
            this.txtInstallPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstallPath.Location = new System.Drawing.Point(13, 57);
            this.txtInstallPath.Name = "txtInstallPath";
            this.txtInstallPath.Path = null;
            this.txtInstallPath.Size = new System.Drawing.Size(278, 36);
            this.txtInstallPath.TabIndex = 4;
            this.txtInstallPath.Text = "Install Path:";
            // 
            // txtExecutablePath
            // 
            this.txtExecutablePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExecutablePath.Location = new System.Drawing.Point(13, 14);
            this.txtExecutablePath.Name = "txtExecutablePath";
            this.txtExecutablePath.Path = null;
            this.txtExecutablePath.Size = new System.Drawing.Size(278, 36);
            this.txtExecutablePath.TabIndex = 3;
            this.txtExecutablePath.Text = "Executable Path:";
            // 
            // txtLocalMediaPath
            // 
            this.txtLocalMediaPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalMediaPath.Location = new System.Drawing.Point(13, 99);
            this.txtLocalMediaPath.Name = "txtLocalMediaPath";
            this.txtLocalMediaPath.Path = null;
            this.txtLocalMediaPath.Size = new System.Drawing.Size(279, 36);
            this.txtLocalMediaPath.TabIndex = 5;
            this.txtLocalMediaPath.Text = "Local Media Path:";
            // 
            // txtBetasPath
            // 
            this.txtBetasPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBetasPath.Location = new System.Drawing.Point(13, 141);
            this.txtBetasPath.Name = "txtBetasPath";
            this.txtBetasPath.Path = null;
            this.txtBetasPath.Size = new System.Drawing.Size(279, 36);
            this.txtBetasPath.TabIndex = 6;
            this.txtBetasPath.Text = "Betas Path:";
            // 
            // txtEmailList
            // 
            this.txtEmailList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailList.Location = new System.Drawing.Point(13, 235);
            this.txtEmailList.Multiline = true;
            this.txtEmailList.Name = "txtEmailList";
            this.txtEmailList.Size = new System.Drawing.Size(278, 86);
            this.txtEmailList.TabIndex = 8;
            this.txtEmailList.Leave += new System.EventHandler(this.txtEmailList_Leave);
            // 
            // btnContacts
            // 
            this.btnContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContacts.Location = new System.Drawing.Point(227, 327);
            this.btnContacts.Name = "btnContacts";
            this.btnContacts.Size = new System.Drawing.Size(64, 22);
            this.btnContacts.TabIndex = 9;
            this.btnContacts.Text = "Emails ...";
            this.btnContacts.UseVisualStyleBackColor = true;
            this.btnContacts.Click += new System.EventHandler(this.btnContacts_Click);
            // 
            // lblEmailList
            // 
            this.lblEmailList.AutoSize = true;
            this.lblEmailList.Location = new System.Drawing.Point(10, 219);
            this.lblEmailList.Name = "lblEmailList";
            this.lblEmailList.Size = new System.Drawing.Size(54, 13);
            this.lblEmailList.TabIndex = 10;
            this.lblEmailList.Text = "Email List:";
            // 
            // PublishOptionsContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblEmailList);
            this.Controls.Add(this.btnContacts);
            this.Controls.Add(this.txtEmailList);
            this.Controls.Add(this.txtBetasPath);
            this.Controls.Add(this.txtLocalMediaPath);
            this.Controls.Add(this.txtInstallPath);
            this.Controls.Add(this.txtExecutablePath);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Name = "PublishOptionsContainer";
            this.Size = new System.Drawing.Size(304, 361);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private Forms.FilenameTextBox txtExecutablePath;
        private Forms.FolderTextBox txtInstallPath;
        private Forms.FolderTextBox txtLocalMediaPath;
        private Forms.FolderTextBox txtBetasPath;
        private System.Windows.Forms.TextBox txtEmailList;
        private System.Windows.Forms.Button btnContacts;
        private System.Windows.Forms.Label lblEmailList;
    }
}
