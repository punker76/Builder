namespace Promob.Builder.PluginsManagement.FrontEnd.Options
{
    partial class GeneralOptionsContainer
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
            this.txtProductsManagerExecutablePath = new Promob.Builder.Forms.FilenameTextBox();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.btnVersionManagement = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtProductsManagerExecutablePath
            // 
            this.txtProductsManagerExecutablePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProductsManagerExecutablePath.Location = new System.Drawing.Point(13, 54);
            this.txtProductsManagerExecutablePath.Name = "txtProductsManagerExecutablePath";
            this.txtProductsManagerExecutablePath.Path = null;
            this.txtProductsManagerExecutablePath.Size = new System.Drawing.Size(373, 36);
            this.txtProductsManagerExecutablePath.TabIndex = 0;
            this.txtProductsManagerExecutablePath.Text = "Products Manager Executable Path";
            // 
            // cbLanguage
            // 
            this.cbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(13, 26);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(373, 21);
            this.cbLanguage.TabIndex = 1;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(10, 10);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(55, 13);
            this.lblLanguage.TabIndex = 2;
            this.lblLanguage.Text = "Language";
            // 
            // btnVersionManagement
            // 
            this.btnVersionManagement.AutoSize = true;
            this.btnVersionManagement.Location = new System.Drawing.Point(13, 96);
            this.btnVersionManagement.Name = "btnVersionManagement";
            this.btnVersionManagement.Size = new System.Drawing.Size(117, 23);
            this.btnVersionManagement.TabIndex = 3;
            this.btnVersionManagement.Text = "Version Management";
            this.btnVersionManagement.UseVisualStyleBackColor = true;
            this.btnVersionManagement.Click += new System.EventHandler(this.btnVersionManagement_Click);
            // 
            // GeneralOptionsContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnVersionManagement);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.txtProductsManagerExecutablePath);
            this.Name = "GeneralOptionsContainer";
            this.Size = new System.Drawing.Size(399, 267);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Forms.FilenameTextBox txtProductsManagerExecutablePath;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Button btnVersionManagement;

    }
}
