namespace Promob.Builder.PluginsManagement.FrontEnd
{
    partial class PublishForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublishForm));
            this.btnAction = new System.Windows.Forms.Button();
            this.cbSaveLocalMediaCopy = new System.Windows.Forms.CheckBox();
            this.txtLocalMediaPath = new System.Windows.Forms.TextBox();
            this.gbGeneral = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.gbPlugins = new System.Windows.Forms.GroupBox();
            this.dgvPublishVersions = new System.Windows.Forms.DataGridView();
            this.colVersionProgressImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colPluginName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVersionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReleaseVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreatePatch = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colVersionStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cbxSendEmail = new System.Windows.Forms.CheckBox();
            this.gbGeneral.SuspendLayout();
            this.gbPlugins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPublishVersions)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(605, 459);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(75, 23);
            this.btnAction.TabIndex = 1;
            this.btnAction.Text = "Publish";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // cbSaveLocalMediaCopy
            // 
            this.cbSaveLocalMediaCopy.AutoSize = true;
            this.cbSaveLocalMediaCopy.Checked = true;
            this.cbSaveLocalMediaCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSaveLocalMediaCopy.Location = new System.Drawing.Point(13, 26);
            this.cbSaveLocalMediaCopy.Name = "cbSaveLocalMediaCopy";
            this.cbSaveLocalMediaCopy.Size = new System.Drawing.Size(111, 17);
            this.cbSaveLocalMediaCopy.TabIndex = 2;
            this.cbSaveLocalMediaCopy.Text = "Save a local copy";
            this.cbSaveLocalMediaCopy.UseVisualStyleBackColor = true;
            // 
            // txtLocalMediaPath
            // 
            this.txtLocalMediaPath.Location = new System.Drawing.Point(13, 49);
            this.txtLocalMediaPath.Name = "txtLocalMediaPath";
            this.txtLocalMediaPath.Size = new System.Drawing.Size(611, 20);
            this.txtLocalMediaPath.TabIndex = 3;
            // 
            // gbGeneral
            // 
            this.gbGeneral.Controls.Add(this.cbxSendEmail);
            this.gbGeneral.Controls.Add(this.btnBrowse);
            this.gbGeneral.Controls.Add(this.cbSaveLocalMediaCopy);
            this.gbGeneral.Controls.Add(this.txtLocalMediaPath);
            this.gbGeneral.Location = new System.Drawing.Point(12, 12);
            this.gbGeneral.Name = "gbGeneral";
            this.gbGeneral.Padding = new System.Windows.Forms.Padding(10);
            this.gbGeneral.Size = new System.Drawing.Size(668, 107);
            this.gbGeneral.TabIndex = 4;
            this.gbGeneral.TabStop = false;
            this.gbGeneral.Text = "General";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(630, 47);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // gbPlugins
            // 
            this.gbPlugins.Controls.Add(this.dgvPublishVersions);
            this.gbPlugins.Location = new System.Drawing.Point(12, 125);
            this.gbPlugins.Name = "gbPlugins";
            this.gbPlugins.Size = new System.Drawing.Size(668, 328);
            this.gbPlugins.TabIndex = 7;
            this.gbPlugins.TabStop = false;
            this.gbPlugins.Text = "Versions";
            // 
            // dgvPublishVersions
            // 
            this.dgvPublishVersions.AllowUserToAddRows = false;
            this.dgvPublishVersions.AllowUserToDeleteRows = false;
            this.dgvPublishVersions.AllowUserToResizeRows = false;
            this.dgvPublishVersions.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvPublishVersions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPublishVersions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPublishVersions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVersionProgressImage,
            this.colPluginName,
            this.colVersionName,
            this.colReleaseVersion,
            this.colCreatePatch,
            this.colVersionStatus,
            this.colMessage});
            this.dgvPublishVersions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPublishVersions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvPublishVersions.GridColor = System.Drawing.Color.LightGray;
            this.dgvPublishVersions.Location = new System.Drawing.Point(3, 16);
            this.dgvPublishVersions.MultiSelect = false;
            this.dgvPublishVersions.Name = "dgvPublishVersions";
            this.dgvPublishVersions.RowHeadersVisible = false;
            this.dgvPublishVersions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPublishVersions.Size = new System.Drawing.Size(662, 309);
            this.dgvPublishVersions.TabIndex = 0;
            this.dgvPublishVersions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPublishVersions_CellContentClick);
            this.dgvPublishVersions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvPublishVersions_DataError);
            // 
            // colVersionProgressImage
            // 
            this.colVersionProgressImage.DataPropertyName = "Image";
            this.colVersionProgressImage.HeaderText = "";
            this.colVersionProgressImage.Name = "colVersionProgressImage";
            this.colVersionProgressImage.ReadOnly = true;
            this.colVersionProgressImage.Width = 22;
            // 
            // colPluginName
            // 
            this.colPluginName.DataPropertyName = "PluginName";
            this.colPluginName.HeaderText = "Plugin";
            this.colPluginName.Name = "colPluginName";
            this.colPluginName.ReadOnly = true;
            this.colPluginName.Width = 150;
            // 
            // colVersionName
            // 
            this.colVersionName.DataPropertyName = "Version";
            this.colVersionName.HeaderText = "Version";
            this.colVersionName.Name = "colVersionName";
            this.colVersionName.ReadOnly = true;
            this.colVersionName.Width = 125;
            // 
            // colReleaseVersion
            // 
            this.colReleaseVersion.DataPropertyName = "ReleaseVersion";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colReleaseVersion.DefaultCellStyle = dataGridViewCellStyle1;
            this.colReleaseVersion.HeaderText = "Release Version";
            this.colReleaseVersion.Name = "colReleaseVersion";
            this.colReleaseVersion.Width = 125;
            // 
            // colCreatePatch
            // 
            this.colCreatePatch.DataPropertyName = "CreatePatch";
            this.colCreatePatch.FalseValue = "False";
            this.colCreatePatch.HeaderText = "Create Patch";
            this.colCreatePatch.Name = "colCreatePatch";
            this.colCreatePatch.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCreatePatch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCreatePatch.TrueValue = "True";
            // 
            // colVersionStatus
            // 
            this.colVersionStatus.DataPropertyName = "Status";
            this.colVersionStatus.HeaderText = "Status";
            this.colVersionStatus.Name = "colVersionStatus";
            this.colVersionStatus.ReadOnly = true;
            this.colVersionStatus.Width = 80;
            // 
            // colMessage
            // 
            this.colMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMessage.HeaderText = "Message";
            this.colMessage.Name = "colMessage";
            this.colMessage.ReadOnly = true;
            this.colMessage.Text = "...";
            // 
            // cbxSendEmail
            // 
            this.cbxSendEmail.AutoSize = true;
            this.cbxSendEmail.Checked = true;
            this.cbxSendEmail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSendEmail.Location = new System.Drawing.Point(13, 77);
            this.cbxSendEmail.Name = "cbxSendEmail";
            this.cbxSendEmail.Size = new System.Drawing.Size(134, 17);
            this.cbxSendEmail.TabIndex = 5;
            this.cbxSendEmail.Text = "Send email when finish";
            this.cbxSendEmail.UseVisualStyleBackColor = true;
            // 
            // PublishForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 489);
            this.Controls.Add(this.gbPlugins);
            this.Controls.Add(this.gbGeneral);
            this.Controls.Add(this.btnAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PublishForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Publish";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PublishForm_FormClosing);
            this.gbGeneral.ResumeLayout(false);
            this.gbGeneral.PerformLayout();
            this.gbPlugins.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPublishVersions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.CheckBox cbSaveLocalMediaCopy;
        private System.Windows.Forms.TextBox txtLocalMediaPath;
        private System.Windows.Forms.GroupBox gbGeneral;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox gbPlugins;
        private System.Windows.Forms.DataGridView dgvPublishVersions;
        private System.Windows.Forms.DataGridViewImageColumn colVersionProgressImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPluginName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReleaseVersion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCreatePatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersionStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colMessage;
        private System.Windows.Forms.CheckBox cbxSendEmail;
    }
}