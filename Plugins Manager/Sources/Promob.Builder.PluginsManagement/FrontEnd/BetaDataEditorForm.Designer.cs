using Promob.Builder.PluginsManagement.FrontEnd.TreeView;
namespace Promob.Builder.PluginsManagement.FrontEnd
{
    partial class BetaDataEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BetaDataEditorForm));
            this.btnAction = new System.Windows.Forms.Button();
            this.lblSelectProductsBelow = new System.Windows.Forms.Label();
            this.lbSelected = new System.Windows.Forms.ListBox();
            this.lblSelectedProducts = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.tcBetaData = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.lblValueName = new System.Windows.Forms.Label();
            this.lblCaptionName = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.gbDependencyProducts = new System.Windows.Forms.GroupBox();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsbFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSaveFileAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tssSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tvProducts = new Promob.Builder.PluginsManagement.FrontEnd.TreeView.ExtendedTreeView();
            this.tcBetaData.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.gbInfo.SuspendLayout();
            this.gbDependencyProducts.SuspendLayout();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(606, 422);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(75, 23);
            this.btnAction.TabIndex = 2;
            this.btnAction.Text = "Action";
            this.btnAction.UseVisualStyleBackColor = true;
            // 
            // lblSelectProductsBelow
            // 
            this.lblSelectProductsBelow.AutoSize = true;
            this.lblSelectProductsBelow.Location = new System.Drawing.Point(10, 23);
            this.lblSelectProductsBelow.Name = "lblSelectProductsBelow";
            this.lblSelectProductsBelow.Size = new System.Drawing.Size(115, 13);
            this.lblSelectProductsBelow.TabIndex = 5;
            this.lblSelectProductsBelow.Text = "Select products below:";
            // 
            // lbSelected
            // 
            this.lbSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSelected.FormattingEnabled = true;
            this.lbSelected.Location = new System.Drawing.Point(375, 39);
            this.lbSelected.Name = "lbSelected";
            this.lbSelected.Size = new System.Drawing.Size(247, 212);
            this.lbSelected.TabIndex = 6;
            // 
            // lblSelectedProducts
            // 
            this.lblSelectedProducts.AutoSize = true;
            this.lblSelectedProducts.Location = new System.Drawing.Point(372, 23);
            this.lblSelectedProducts.Name = "lblSelectedProducts";
            this.lblSelectedProducts.Size = new System.Drawing.Size(97, 13);
            this.lblSelectedProducts.TabIndex = 5;
            this.lblSelectedProducts.Text = "Selected Products:";
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(267, 39);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(102, 23);
            this.btnUp.TabIndex = 7;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(267, 68);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(102, 23);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(267, 97);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(102, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // tcBetaData
            // 
            this.tcBetaData.Controls.Add(this.tpGeneral);
            this.tcBetaData.Location = new System.Drawing.Point(11, 40);
            this.tcBetaData.Name = "tcBetaData";
            this.tcBetaData.SelectedIndex = 0;
            this.tcBetaData.Size = new System.Drawing.Size(670, 376);
            this.tcBetaData.TabIndex = 8;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.gbInfo);
            this.tpGeneral.Controls.Add(this.gbDependencyProducts);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(10);
            this.tpGeneral.Size = new System.Drawing.Size(662, 350);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.lblValueName);
            this.gbInfo.Controls.Add(this.lblCaptionName);
            this.gbInfo.Controls.Add(this.btnBrowse);
            this.gbInfo.Location = new System.Drawing.Point(13, 13);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(635, 56);
            this.gbInfo.TabIndex = 11;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Info";
            // 
            // lblValueName
            // 
            this.lblValueName.AutoSize = true;
            this.lblValueName.Location = new System.Drawing.Point(57, 24);
            this.lblValueName.Name = "lblValueName";
            this.lblValueName.Size = new System.Drawing.Size(35, 13);
            this.lblValueName.TabIndex = 11;
            this.lblValueName.Text = "label1";
            // 
            // lblCaptionName
            // 
            this.lblCaptionName.AutoSize = true;
            this.lblCaptionName.Location = new System.Drawing.Point(19, 24);
            this.lblCaptionName.Name = "lblCaptionName";
            this.lblCaptionName.Size = new System.Drawing.Size(38, 13);
            this.lblCaptionName.TabIndex = 9;
            this.lblCaptionName.Text = "Name:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(529, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(93, 23);
            this.btnBrowse.TabIndex = 10;
            this.btnBrowse.Text = "Browse folder...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // gbDependencyProducts
            // 
            this.gbDependencyProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDependencyProducts.Controls.Add(this.lblSelectProductsBelow);
            this.gbDependencyProducts.Controls.Add(this.lbSelected);
            this.gbDependencyProducts.Controls.Add(this.btnRemove);
            this.gbDependencyProducts.Controls.Add(this.btnUp);
            this.gbDependencyProducts.Controls.Add(this.tvProducts);
            this.gbDependencyProducts.Controls.Add(this.lblSelectedProducts);
            this.gbDependencyProducts.Controls.Add(this.btnDown);
            this.gbDependencyProducts.Location = new System.Drawing.Point(13, 75);
            this.gbDependencyProducts.Name = "gbDependencyProducts";
            this.gbDependencyProducts.Padding = new System.Windows.Forms.Padding(10);
            this.gbDependencyProducts.Size = new System.Drawing.Size(635, 262);
            this.gbDependencyProducts.TabIndex = 8;
            this.gbDependencyProducts.TabStop = false;
            this.gbDependencyProducts.Text = "Dependency Products";
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFile,
            this.tsbTools});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(692, 24);
            this.msMenu.TabIndex = 9;
            this.msMenu.Text = "Menu";
            // 
            // tsbFile
            // 
            this.tsbFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewFile,
            this.tsbOpenFile,
            this.tsbSaveFile,
            this.tsbSaveFileAs,
            this.tssSeparator,
            this.tsbClose});
            this.tsbFile.Name = "tsbFile";
            this.tsbFile.Size = new System.Drawing.Size(37, 20);
            this.tsbFile.Text = "&File";
            // 
            // tsbNewFile
            // 
            this.tsbNewFile.Image = global::Promob.Builder.PluginsManagement.Properties.Resources._16x16_new_file;
            this.tsbNewFile.Name = "tsbNewFile";
            this.tsbNewFile.Size = new System.Drawing.Size(194, 22);
            this.tsbNewFile.Text = "&New BetaData File";
            this.tsbNewFile.Click += new System.EventHandler(this.tsbNewFile_Click);
            // 
            // tsbOpenFile
            // 
            this.tsbOpenFile.Image = global::Promob.Builder.PluginsManagement.Properties.Resources._16x16_open_file;
            this.tsbOpenFile.Name = "tsbOpenFile";
            this.tsbOpenFile.Size = new System.Drawing.Size(194, 22);
            this.tsbOpenFile.Text = "&Open BetaData File...";
            this.tsbOpenFile.Click += new System.EventHandler(this.tsbOpenFile_Click);
            // 
            // tsbSaveFile
            // 
            this.tsbSaveFile.Image = global::Promob.Builder.PluginsManagement.Properties.Resources._16x16_save_file;
            this.tsbSaveFile.Name = "tsbSaveFile";
            this.tsbSaveFile.Size = new System.Drawing.Size(194, 22);
            this.tsbSaveFile.Text = "&Save BetaData File";
            this.tsbSaveFile.Click += new System.EventHandler(this.tsbSaveFile_Click);
            // 
            // tsbSaveFileAs
            // 
            this.tsbSaveFileAs.Name = "tsbSaveFileAs";
            this.tsbSaveFileAs.Size = new System.Drawing.Size(194, 22);
            this.tsbSaveFileAs.Text = "S&ave BetaData File As...";
            this.tsbSaveFileAs.Click += new System.EventHandler(this.tsbSaveFileAs_Click);
            // 
            // tssSeparator
            // 
            this.tssSeparator.Name = "tssSeparator";
            this.tssSeparator.Size = new System.Drawing.Size(191, 6);
            // 
            // tsbClose
            // 
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(194, 22);
            this.tsbClose.Text = "&Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tsbTools
            // 
            this.tsbTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOptions});
            this.tsbTools.Name = "tsbTools";
            this.tsbTools.Size = new System.Drawing.Size(48, 20);
            this.tsbTools.Text = "&Tools";
            // 
            // tsbOptions
            // 
            this.tsbOptions.Name = "tsbOptions";
            this.tsbOptions.Size = new System.Drawing.Size(116, 22);
            this.tsbOptions.Text = "Options";
            this.tsbOptions.Click += new System.EventHandler(this.tsbOptions_Click);
            // 
            // tvProducts
            // 
            this.tvProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvProducts.CheckBoxes = true;
            this.tvProducts.Location = new System.Drawing.Point(13, 39);
            this.tvProducts.Name = "tvProducts";
            this.tvProducts.Size = new System.Drawing.Size(248, 210);
            this.tvProducts.TabIndex = 4;
            this.tvProducts.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvProducts_BeforeCheck);
            this.tvProducts.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvProducts_AfterCheck);
            this.tvProducts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProducts_AfterSelect);
            // 
            // BetaDataEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 453);
            this.Controls.Add(this.tcBetaData);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.msMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BetaDataEditorForm";
            this.Text = "BetaEditorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BetaDataEditorForm_FormClosing);
            this.tcBetaData.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.gbDependencyProducts.ResumeLayout(false);
            this.gbDependencyProducts.PerformLayout();
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAction;
        private ExtendedTreeView tvProducts;
        private System.Windows.Forms.Label lblSelectProductsBelow;
        private System.Windows.Forms.ListBox lbSelected;
        private System.Windows.Forms.Label lblSelectedProducts;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TabControl tcBetaData;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.GroupBox gbDependencyProducts;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblCaptionName;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Label lblValueName;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsbFile;
        private System.Windows.Forms.ToolStripMenuItem tsbOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsbSaveFileAs;
        private System.Windows.Forms.ToolStripMenuItem tsbSaveFile;
        private System.Windows.Forms.ToolStripSeparator tssSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsbClose;
        private System.Windows.Forms.ToolStripMenuItem tsbNewFile;
        private System.Windows.Forms.ToolStripMenuItem tsbTools;
        private System.Windows.Forms.ToolStripMenuItem tsbOptions;
    }
}