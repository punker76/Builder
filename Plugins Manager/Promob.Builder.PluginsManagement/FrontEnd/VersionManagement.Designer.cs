namespace Promob.Builder.PluginsManagement.FrontEnd
{
    partial class VersionManagement
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionManagement));
            this.cmsVersion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiDeleteVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.tvwPlugins = new System.Windows.Forms.TreeView();
            this.tspMenus = new System.Windows.Forms.ToolStrip();
            this.tsbDeleteVersion = new System.Windows.Forms.ToolStripButton();
            this.cmsVersion.SuspendLayout();
            this.tspMenus.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsVersion
            // 
            this.cmsVersion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiDeleteVersion});
            this.cmsVersion.Name = "cmsVersion";
            this.cmsVersion.Size = new System.Drawing.Size(150, 26);
            // 
            // cmiDeleteVersion
            // 
            this.cmiDeleteVersion.Image = global::Promob.Builder.PluginsManagement.Properties.Resources.cross;
            this.cmiDeleteVersion.Name = "cmiDeleteVersion";
            this.cmiDeleteVersion.Size = new System.Drawing.Size(149, 22);
            this.cmiDeleteVersion.Text = "Delete Version";
            this.cmiDeleteVersion.Click += new System.EventHandler(this.cmiDeleteVersion_Click);
            // 
            // tvwPlugins
            // 
            this.tvwPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwPlugins.Location = new System.Drawing.Point(0, 28);
            this.tvwPlugins.Name = "tvwPlugins";
            this.tvwPlugins.Size = new System.Drawing.Size(288, 416);
            this.tvwPlugins.TabIndex = 1;
            this.tvwPlugins.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwPlugins_AfterSelect);
            this.tvwPlugins.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwPlugins_MouseUp);
            // 
            // tspMenus
            // 
            this.tspMenus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDeleteVersion});
            this.tspMenus.Location = new System.Drawing.Point(0, 0);
            this.tspMenus.Name = "tspMenus";
            this.tspMenus.Size = new System.Drawing.Size(288, 25);
            this.tspMenus.TabIndex = 2;
            this.tspMenus.Text = "toolStrip1";
            // 
            // tsbDeleteVersion
            // 
            this.tsbDeleteVersion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeleteVersion.Enabled = false;
            this.tsbDeleteVersion.Image = global::Promob.Builder.PluginsManagement.Properties.Resources.cross;
            this.tsbDeleteVersion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteVersion.Name = "tsbDeleteVersion";
            this.tsbDeleteVersion.Size = new System.Drawing.Size(23, 22);
            this.tsbDeleteVersion.Text = "Delete Version";
            this.tsbDeleteVersion.Click += new System.EventHandler(this.tsbDeleteVersion_Click);
            // 
            // VersionManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 444);
            this.Controls.Add(this.tspMenus);
            this.Controls.Add(this.tvwPlugins);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VersionManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Version Management";
            this.Load += new System.EventHandler(this.VersionManagement_Load);
            this.cmsVersion.ResumeLayout(false);
            this.tspMenus.ResumeLayout(false);
            this.tspMenus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsVersion;
        private System.Windows.Forms.ToolStripMenuItem cmiDeleteVersion;
        private System.Windows.Forms.TreeView tvwPlugins;
        private System.Windows.Forms.ToolStrip tspMenus;
        private System.Windows.Forms.ToolStripButton tsbDeleteVersion;
    }
}