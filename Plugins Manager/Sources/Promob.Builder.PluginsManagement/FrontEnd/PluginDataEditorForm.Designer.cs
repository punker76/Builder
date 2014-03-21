namespace Promob.Builder.PluginsManagement.FrontEnd
{
    partial class PluginDataEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginDataEditorForm));
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblCaptionName = new System.Windows.Forms.Label();
            this.lblValueVersion = new System.Windows.Forms.Label();
            this.lblValueName = new System.Windows.Forms.Label();
            this.lblCaptionVersion = new System.Windows.Forms.Label();
            this.lblValueDescription = new System.Windows.Forms.Label();
            this.lblCaptionDescription = new System.Windows.Forms.Label();
            this.lblCaptionId = new System.Windows.Forms.Label();
            this.lblValueId = new System.Windows.Forms.Label();
            this.lblCaptionProductId = new System.Windows.Forms.Label();
            this.lblValueProductId = new System.Windows.Forms.Label();
            this.dgvHostApplications = new System.Windows.Forms.DataGridView();
            this.colHostApplicationsChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colHostDistributionsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHostDistributionsMinVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHostDistributionsAllowedDistributions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHostDistributionsDummy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.tcPluginData = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.gbHostApplications = new System.Windows.Forms.GroupBox();
            this.btnEditHostApplication = new System.Windows.Forms.Button();
            this.btnRemoveHostApplication = new System.Windows.Forms.Button();
            this.btnAddHostApplication = new System.Windows.Forms.Button();
            this.tpDependencies = new System.Windows.Forms.TabPage();
            this.dgvDependencies = new System.Windows.Forms.DataGridView();
            this.colDepencencyChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDependencyFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDependencyLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDependencyDummy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsDependenciesMenu = new System.Windows.Forms.ToolStrip();
            this.tsbAddDependency = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveSelectedDependency = new System.Windows.Forms.ToolStripButton();
            this.tpContent = new System.Windows.Forms.TabPage();
            this.tsContentMenu = new System.Windows.Forms.ToolStrip();
            this.tsbAddContent = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveSelectedContent = new System.Windows.Forms.ToolStripButton();
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.colContentChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colContentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContentLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContentDestiny = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContentShared = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colContentDummy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbInfo.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHostApplications)).BeginInit();
            this.tcPluginData.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.gbHostApplications.SuspendLayout();
            this.tpDependencies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDependencies)).BeginInit();
            this.tsDependenciesMenu.SuspendLayout();
            this.tpContent.SuspendLayout();
            this.tsContentMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.SuspendLayout();
            // 
            // gbInfo
            // 
            this.gbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInfo.Controls.Add(this.btnBrowse);
            this.gbInfo.Controls.Add(this.tlpInfo);
            this.gbInfo.Location = new System.Drawing.Point(13, 13);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Padding = new System.Windows.Forms.Padding(10);
            this.gbInfo.Size = new System.Drawing.Size(636, 119);
            this.gbInfo.TabIndex = 0;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Info";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(529, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(93, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse folder...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // tlpInfo
            // 
            this.tlpInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpInfo.ColumnCount = 2;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfo.Controls.Add(this.lblCaptionName, 0, 0);
            this.tlpInfo.Controls.Add(this.lblValueVersion, 1, 2);
            this.tlpInfo.Controls.Add(this.lblValueName, 1, 0);
            this.tlpInfo.Controls.Add(this.lblCaptionVersion, 0, 2);
            this.tlpInfo.Controls.Add(this.lblValueDescription, 1, 1);
            this.tlpInfo.Controls.Add(this.lblCaptionDescription, 0, 1);
            this.tlpInfo.Controls.Add(this.lblCaptionId, 0, 3);
            this.tlpInfo.Controls.Add(this.lblValueId, 1, 3);
            this.tlpInfo.Controls.Add(this.lblCaptionProductId, 0, 4);
            this.tlpInfo.Controls.Add(this.lblValueProductId, 1, 4);
            this.tlpInfo.Location = new System.Drawing.Point(10, 23);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 6;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfo.Size = new System.Drawing.Size(513, 86);
            this.tlpInfo.TabIndex = 3;
            // 
            // lblCaptionName
            // 
            this.lblCaptionName.AutoSize = true;
            this.lblCaptionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaptionName.Location = new System.Drawing.Point(3, 0);
            this.lblCaptionName.Name = "lblCaptionName";
            this.lblCaptionName.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblCaptionName.Size = new System.Drawing.Size(63, 17);
            this.lblCaptionName.TabIndex = 1;
            this.lblCaptionName.Text = "Name:";
            this.lblCaptionName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblValueVersion
            // 
            this.lblValueVersion.AutoSize = true;
            this.lblValueVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValueVersion.Location = new System.Drawing.Point(72, 34);
            this.lblValueVersion.Name = "lblValueVersion";
            this.lblValueVersion.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblValueVersion.Size = new System.Drawing.Size(438, 17);
            this.lblValueVersion.TabIndex = 2;
            this.lblValueVersion.Text = "[version]";
            // 
            // lblValueName
            // 
            this.lblValueName.AutoSize = true;
            this.lblValueName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValueName.Location = new System.Drawing.Point(72, 0);
            this.lblValueName.Name = "lblValueName";
            this.lblValueName.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblValueName.Size = new System.Drawing.Size(438, 17);
            this.lblValueName.TabIndex = 2;
            this.lblValueName.Text = "[name]";
            // 
            // lblCaptionVersion
            // 
            this.lblCaptionVersion.AutoSize = true;
            this.lblCaptionVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaptionVersion.Location = new System.Drawing.Point(3, 34);
            this.lblCaptionVersion.Name = "lblCaptionVersion";
            this.lblCaptionVersion.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblCaptionVersion.Size = new System.Drawing.Size(63, 17);
            this.lblCaptionVersion.TabIndex = 1;
            this.lblCaptionVersion.Text = "Version:";
            this.lblCaptionVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblValueDescription
            // 
            this.lblValueDescription.AutoSize = true;
            this.lblValueDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValueDescription.Location = new System.Drawing.Point(72, 17);
            this.lblValueDescription.Name = "lblValueDescription";
            this.lblValueDescription.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblValueDescription.Size = new System.Drawing.Size(438, 17);
            this.lblValueDescription.TabIndex = 2;
            this.lblValueDescription.Text = "[description]";
            // 
            // lblCaptionDescription
            // 
            this.lblCaptionDescription.AutoSize = true;
            this.lblCaptionDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaptionDescription.Location = new System.Drawing.Point(3, 17);
            this.lblCaptionDescription.Name = "lblCaptionDescription";
            this.lblCaptionDescription.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblCaptionDescription.Size = new System.Drawing.Size(63, 17);
            this.lblCaptionDescription.TabIndex = 1;
            this.lblCaptionDescription.Text = "Description:";
            this.lblCaptionDescription.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCaptionId
            // 
            this.lblCaptionId.AutoSize = true;
            this.lblCaptionId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaptionId.Location = new System.Drawing.Point(3, 51);
            this.lblCaptionId.Name = "lblCaptionId";
            this.lblCaptionId.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblCaptionId.Size = new System.Drawing.Size(63, 17);
            this.lblCaptionId.TabIndex = 3;
            this.lblCaptionId.Text = "Id:";
            this.lblCaptionId.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblValueId
            // 
            this.lblValueId.AutoSize = true;
            this.lblValueId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValueId.Location = new System.Drawing.Point(72, 51);
            this.lblValueId.Name = "lblValueId";
            this.lblValueId.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblValueId.Size = new System.Drawing.Size(438, 17);
            this.lblValueId.TabIndex = 4;
            this.lblValueId.Text = "[id]";
            // 
            // lblCaptionProductId
            // 
            this.lblCaptionProductId.AutoSize = true;
            this.lblCaptionProductId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaptionProductId.Location = new System.Drawing.Point(3, 68);
            this.lblCaptionProductId.Name = "lblCaptionProductId";
            this.lblCaptionProductId.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblCaptionProductId.Size = new System.Drawing.Size(63, 17);
            this.lblCaptionProductId.TabIndex = 5;
            this.lblCaptionProductId.Text = "Product Id:";
            this.lblCaptionProductId.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblValueProductId
            // 
            this.lblValueProductId.AutoSize = true;
            this.lblValueProductId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValueProductId.Location = new System.Drawing.Point(72, 68);
            this.lblValueProductId.Name = "lblValueProductId";
            this.lblValueProductId.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblValueProductId.Size = new System.Drawing.Size(438, 17);
            this.lblValueProductId.TabIndex = 6;
            this.lblValueProductId.Text = "[product id]";
            // 
            // dgvHostApplications
            // 
            this.dgvHostApplications.AllowUserToAddRows = false;
            this.dgvHostApplications.AllowUserToDeleteRows = false;
            this.dgvHostApplications.AllowUserToResizeColumns = false;
            this.dgvHostApplications.AllowUserToResizeRows = false;
            this.dgvHostApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHostApplications.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvHostApplications.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHostApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHostApplications.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHostApplicationsChecked,
            this.colHostDistributionsName,
            this.colHostDistributionsMinVersion,
            this.colHostDistributionsAllowedDistributions,
            this.colHostDistributionsDummy});
            this.dgvHostApplications.GridColor = System.Drawing.Color.LightGray;
            this.dgvHostApplications.Location = new System.Drawing.Point(3, 16);
            this.dgvHostApplications.Name = "dgvHostApplications";
            this.dgvHostApplications.RowHeadersVisible = false;
            this.dgvHostApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHostApplications.Size = new System.Drawing.Size(519, 209);
            this.dgvHostApplications.TabIndex = 0;
            // 
            // colHostApplicationsChecked
            // 
            this.colHostApplicationsChecked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colHostApplicationsChecked.FalseValue = "False";
            this.colHostApplicationsChecked.HeaderText = "";
            this.colHostApplicationsChecked.Name = "colHostApplicationsChecked";
            this.colHostApplicationsChecked.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colHostApplicationsChecked.TrueValue = "True";
            this.colHostApplicationsChecked.Width = 5;
            // 
            // colHostDistributionsName
            // 
            this.colHostDistributionsName.DataPropertyName = "Name";
            this.colHostDistributionsName.HeaderText = "Name";
            this.colHostDistributionsName.Name = "colHostDistributionsName";
            this.colHostDistributionsName.Width = 150;
            // 
            // colHostDistributionsMinVersion
            // 
            this.colHostDistributionsMinVersion.DataPropertyName = "MinVersion";
            this.colHostDistributionsMinVersion.HeaderText = "Min. Version";
            this.colHostDistributionsMinVersion.Name = "colHostDistributionsMinVersion";
            this.colHostDistributionsMinVersion.Width = 150;
            // 
            // colHostDistributionsAllowedDistributions
            // 
            this.colHostDistributionsAllowedDistributions.DataPropertyName = "AllowedDistributionsDisplayString";
            this.colHostDistributionsAllowedDistributions.HeaderText = "Allowed Distributions";
            this.colHostDistributionsAllowedDistributions.Name = "colHostDistributionsAllowedDistributions";
            this.colHostDistributionsAllowedDistributions.Width = 150;
            // 
            // colHostDistributionsDummy
            // 
            this.colHostDistributionsDummy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colHostDistributionsDummy.HeaderText = "";
            this.colHostDistributionsDummy.Name = "colHostDistributionsDummy";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(606, 422);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tcPluginData
            // 
            this.tcPluginData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcPluginData.Controls.Add(this.tpGeneral);
            this.tcPluginData.Controls.Add(this.tpDependencies);
            this.tcPluginData.Controls.Add(this.tpContent);
            this.tcPluginData.Location = new System.Drawing.Point(11, 10);
            this.tcPluginData.Name = "tcPluginData";
            this.tcPluginData.SelectedIndex = 0;
            this.tcPluginData.Size = new System.Drawing.Size(670, 406);
            this.tcPluginData.TabIndex = 5;
            // 
            // tpGeneral
            // 
            this.tpGeneral.BackColor = System.Drawing.Color.White;
            this.tpGeneral.Controls.Add(this.gbHostApplications);
            this.tpGeneral.Controls.Add(this.gbInfo);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(10);
            this.tpGeneral.Size = new System.Drawing.Size(662, 380);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            // 
            // gbHostApplications
            // 
            this.gbHostApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHostApplications.Controls.Add(this.btnEditHostApplication);
            this.gbHostApplications.Controls.Add(this.btnRemoveHostApplication);
            this.gbHostApplications.Controls.Add(this.btnAddHostApplication);
            this.gbHostApplications.Controls.Add(this.dgvHostApplications);
            this.gbHostApplications.Location = new System.Drawing.Point(14, 139);
            this.gbHostApplications.Name = "gbHostApplications";
            this.gbHostApplications.Size = new System.Drawing.Size(635, 228);
            this.gbHostApplications.TabIndex = 1;
            this.gbHostApplications.TabStop = false;
            this.gbHostApplications.Text = "Host Applications";
            // 
            // btnEditHostApplication
            // 
            this.btnEditHostApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditHostApplication.Location = new System.Drawing.Point(529, 45);
            this.btnEditHostApplication.Name = "btnEditHostApplication";
            this.btnEditHostApplication.Size = new System.Drawing.Size(100, 23);
            this.btnEditHostApplication.TabIndex = 2;
            this.btnEditHostApplication.Text = "Edit";
            this.btnEditHostApplication.UseVisualStyleBackColor = true;
            this.btnEditHostApplication.Click += new System.EventHandler(this.btnEditHostApplication_Click);
            // 
            // btnRemoveHostApplication
            // 
            this.btnRemoveHostApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveHostApplication.Location = new System.Drawing.Point(528, 74);
            this.btnRemoveHostApplication.Name = "btnRemoveHostApplication";
            this.btnRemoveHostApplication.Size = new System.Drawing.Size(101, 23);
            this.btnRemoveHostApplication.TabIndex = 1;
            this.btnRemoveHostApplication.Text = "Remove";
            this.btnRemoveHostApplication.UseVisualStyleBackColor = true;
            this.btnRemoveHostApplication.Click += new System.EventHandler(this.btnRemoveHostApplication_Click);
            // 
            // btnAddHostApplication
            // 
            this.btnAddHostApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddHostApplication.Location = new System.Drawing.Point(528, 16);
            this.btnAddHostApplication.Name = "btnAddHostApplication";
            this.btnAddHostApplication.Size = new System.Drawing.Size(101, 23);
            this.btnAddHostApplication.TabIndex = 1;
            this.btnAddHostApplication.Text = "Add";
            this.btnAddHostApplication.UseVisualStyleBackColor = true;
            this.btnAddHostApplication.Click += new System.EventHandler(this.btnAddHostApplication_Click);
            // 
            // tpDependencies
            // 
            this.tpDependencies.Controls.Add(this.dgvDependencies);
            this.tpDependencies.Controls.Add(this.tsDependenciesMenu);
            this.tpDependencies.Location = new System.Drawing.Point(4, 22);
            this.tpDependencies.Name = "tpDependencies";
            this.tpDependencies.Padding = new System.Windows.Forms.Padding(3);
            this.tpDependencies.Size = new System.Drawing.Size(662, 380);
            this.tpDependencies.TabIndex = 2;
            this.tpDependencies.Text = "Dependencies";
            this.tpDependencies.UseVisualStyleBackColor = true;
            // 
            // dgvDependencies
            // 
            this.dgvDependencies.AllowUserToAddRows = false;
            this.dgvDependencies.AllowUserToDeleteRows = false;
            this.dgvDependencies.AllowUserToResizeRows = false;
            this.dgvDependencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDependencies.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvDependencies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDependencies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDependencies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDepencencyChecked,
            this.colDependencyFilename,
            this.colDependencyLocal,
            this.colDependencyDummy});
            this.dgvDependencies.GridColor = System.Drawing.Color.LightGray;
            this.dgvDependencies.Location = new System.Drawing.Point(3, 31);
            this.dgvDependencies.MultiSelect = false;
            this.dgvDependencies.Name = "dgvDependencies";
            this.dgvDependencies.RowHeadersVisible = false;
            this.dgvDependencies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDependencies.Size = new System.Drawing.Size(656, 346);
            this.dgvDependencies.TabIndex = 1;
            // 
            // colDepencencyChecked
            // 
            this.colDepencencyChecked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colDepencencyChecked.HeaderText = "";
            this.colDepencencyChecked.Name = "colDepencencyChecked";
            this.colDepencencyChecked.Width = 5;
            // 
            // colDependencyFilename
            // 
            this.colDependencyFilename.DataPropertyName = "Filename";
            this.colDependencyFilename.HeaderText = "Filename";
            this.colDependencyFilename.Name = "colDependencyFilename";
            this.colDependencyFilename.Width = 300;
            // 
            // colDependencyLocal
            // 
            this.colDependencyLocal.DataPropertyName = "Local";
            this.colDependencyLocal.HeaderText = "Local";
            this.colDependencyLocal.Name = "colDependencyLocal";
            this.colDependencyLocal.Width = 300;
            // 
            // colDependencyDummy
            // 
            this.colDependencyDummy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDependencyDummy.HeaderText = "";
            this.colDependencyDummy.Name = "colDependencyDummy";
            this.colDependencyDummy.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDependencyDummy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tsDependenciesMenu
            // 
            this.tsDependenciesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddDependency,
            this.tsbRemoveSelectedDependency});
            this.tsDependenciesMenu.Location = new System.Drawing.Point(3, 3);
            this.tsDependenciesMenu.Name = "tsDependenciesMenu";
            this.tsDependenciesMenu.Size = new System.Drawing.Size(656, 25);
            this.tsDependenciesMenu.TabIndex = 0;
            this.tsDependenciesMenu.Text = "toolStrip1";
            // 
            // tsbAddDependency
            // 
            this.tsbAddDependency.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddDependency.Image = global::Promob.Builder.PluginsManagement.Properties.Resources.plus;
            this.tsbAddDependency.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddDependency.Name = "tsbAddDependency";
            this.tsbAddDependency.Size = new System.Drawing.Size(23, 22);
            this.tsbAddDependency.Text = "Add dependency";
            this.tsbAddDependency.Click += new System.EventHandler(this.tsbAddDependency_Click);
            // 
            // tsbRemoveSelectedDependency
            // 
            this.tsbRemoveSelectedDependency.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemoveSelectedDependency.Image = global::Promob.Builder.PluginsManagement.Properties.Resources.minus;
            this.tsbRemoveSelectedDependency.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveSelectedDependency.Name = "tsbRemoveSelectedDependency";
            this.tsbRemoveSelectedDependency.Size = new System.Drawing.Size(23, 22);
            this.tsbRemoveSelectedDependency.Text = "Remove selected dependencies";
            this.tsbRemoveSelectedDependency.Click += new System.EventHandler(this.tsbRemoveSelectedDependency_Click);
            // 
            // tpContent
            // 
            this.tpContent.Controls.Add(this.tsContentMenu);
            this.tpContent.Controls.Add(this.dgvContent);
            this.tpContent.Location = new System.Drawing.Point(4, 22);
            this.tpContent.Name = "tpContent";
            this.tpContent.Padding = new System.Windows.Forms.Padding(3);
            this.tpContent.Size = new System.Drawing.Size(662, 380);
            this.tpContent.TabIndex = 1;
            this.tpContent.Text = "Content";
            this.tpContent.UseVisualStyleBackColor = true;
            // 
            // tsContentMenu
            // 
            this.tsContentMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddContent,
            this.tsbRemoveSelectedContent});
            this.tsContentMenu.Location = new System.Drawing.Point(3, 3);
            this.tsContentMenu.Name = "tsContentMenu";
            this.tsContentMenu.Size = new System.Drawing.Size(656, 25);
            this.tsContentMenu.TabIndex = 5;
            this.tsContentMenu.Text = "toolStrip1";
            // 
            // tsbAddContent
            // 
            this.tsbAddContent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddContent.Image = global::Promob.Builder.PluginsManagement.Properties.Resources.plus;
            this.tsbAddContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddContent.Name = "tsbAddContent";
            this.tsbAddContent.Size = new System.Drawing.Size(23, 22);
            this.tsbAddContent.Text = "Add content";
            this.tsbAddContent.ToolTipText = "Add content";
            this.tsbAddContent.Click += new System.EventHandler(this.tsbAddContent_Click);
            // 
            // tsbRemoveSelectedContent
            // 
            this.tsbRemoveSelectedContent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemoveSelectedContent.Image = global::Promob.Builder.PluginsManagement.Properties.Resources.minus;
            this.tsbRemoveSelectedContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveSelectedContent.Name = "tsbRemoveSelectedContent";
            this.tsbRemoveSelectedContent.Size = new System.Drawing.Size(23, 22);
            this.tsbRemoveSelectedContent.Text = "Remove selected content";
            this.tsbRemoveSelectedContent.Click += new System.EventHandler(this.tsbRemoveSelectedContent_Click);
            // 
            // dgvContent
            // 
            this.dgvContent.AllowUserToAddRows = false;
            this.dgvContent.AllowUserToDeleteRows = false;
            this.dgvContent.AllowUserToResizeRows = false;
            this.dgvContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvContent.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colContentChecked,
            this.colContentName,
            this.colContentLocal,
            this.colContentDestiny,
            this.colContentShared,
            this.colContentDummy});
            this.dgvContent.GridColor = System.Drawing.Color.LightGray;
            this.dgvContent.Location = new System.Drawing.Point(3, 31);
            this.dgvContent.MultiSelect = false;
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.RowHeadersVisible = false;
            this.dgvContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContent.Size = new System.Drawing.Size(656, 346);
            this.dgvContent.TabIndex = 0;
            // 
            // colContentChecked
            // 
            this.colContentChecked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colContentChecked.FalseValue = "False";
            this.colContentChecked.HeaderText = "";
            this.colContentChecked.Name = "colContentChecked";
            this.colContentChecked.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colContentChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colContentChecked.TrueValue = "True";
            this.colContentChecked.Width = 19;
            // 
            // colContentName
            // 
            this.colContentName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colContentName.DataPropertyName = "Name";
            this.colContentName.HeaderText = "Name";
            this.colContentName.Name = "colContentName";
            this.colContentName.ToolTipText = "Name";
            this.colContentName.Width = 60;
            // 
            // colContentLocal
            // 
            this.colContentLocal.DataPropertyName = "Local";
            this.colContentLocal.HeaderText = "Local";
            this.colContentLocal.Name = "colContentLocal";
            this.colContentLocal.ToolTipText = "Local";
            this.colContentLocal.Width = 180;
            // 
            // colContentDestiny
            // 
            this.colContentDestiny.DataPropertyName = "Destiny";
            this.colContentDestiny.HeaderText = "Destiny";
            this.colContentDestiny.Name = "colContentDestiny";
            this.colContentDestiny.ToolTipText = "Destiny";
            this.colContentDestiny.Width = 180;
            // 
            // colContentShared
            // 
            this.colContentShared.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colContentShared.DataPropertyName = "Shared";
            this.colContentShared.FalseValue = "false";
            this.colContentShared.HeaderText = "Shared";
            this.colContentShared.Name = "colContentShared";
            this.colContentShared.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colContentShared.ToolTipText = "Shared";
            this.colContentShared.TrueValue = "true";
            this.colContentShared.Width = 47;
            // 
            // colContentDummy
            // 
            this.colContentDummy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colContentDummy.HeaderText = "";
            this.colContentDummy.Name = "colContentDummy";
            // 
            // PluginDataEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 453);
            this.Controls.Add(this.tcPluginData);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PluginDataEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PluginDataEditorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginDataEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.PluginDataEditorForm_Load);
            this.gbInfo.ResumeLayout(false);
            this.tlpInfo.ResumeLayout(false);
            this.tlpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHostApplications)).EndInit();
            this.tcPluginData.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.gbHostApplications.ResumeLayout(false);
            this.tpDependencies.ResumeLayout(false);
            this.tpDependencies.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDependencies)).EndInit();
            this.tsDependenciesMenu.ResumeLayout(false);
            this.tsDependenciesMenu.PerformLayout();
            this.tpContent.ResumeLayout(false);
            this.tpContent.PerformLayout();
            this.tsContentMenu.ResumeLayout(false);
            this.tsContentMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcPluginData;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpContent;

        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label lblCaptionName;
        private System.Windows.Forms.Label lblValueVersion;
        private System.Windows.Forms.Label lblValueName;
        private System.Windows.Forms.Label lblCaptionVersion;
        private System.Windows.Forms.Label lblValueDescription;
        private System.Windows.Forms.Label lblCaptionDescription;
        private System.Windows.Forms.Label lblCaptionId;
        private System.Windows.Forms.Label lblValueId;
        private System.Windows.Forms.Label lblCaptionProductId;
        private System.Windows.Forms.Label lblValueProductId;

        private System.Windows.Forms.GroupBox gbHostApplications;

        private System.Windows.Forms.DataGridView dgvHostApplications;

        private System.Windows.Forms.ToolStrip tsContentMenu;
        private System.Windows.Forms.ToolStripButton tsbAddContent;
        private System.Windows.Forms.ToolStripButton tsbRemoveSelectedContent;

        private System.Windows.Forms.DataGridView dgvContent;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabPage tpDependencies;
        private System.Windows.Forms.DataGridView dgvDependencies;
        private System.Windows.Forms.ToolStrip tsDependenciesMenu;
        private System.Windows.Forms.ToolStripButton tsbAddDependency;
        private System.Windows.Forms.ToolStripButton tsbRemoveSelectedDependency;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDepencencyChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDependencyFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDependencyLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDependencyDummy;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colContentChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContentLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContentDestiny;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colContentShared;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContentDummy;
        private System.Windows.Forms.Button btnRemoveHostApplication;
        private System.Windows.Forms.Button btnAddHostApplication;
        private System.Windows.Forms.Button btnEditHostApplication;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colHostApplicationsChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHostDistributionsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHostDistributionsMinVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHostDistributionsAllowedDistributions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHostDistributionsDummy;

    }
}