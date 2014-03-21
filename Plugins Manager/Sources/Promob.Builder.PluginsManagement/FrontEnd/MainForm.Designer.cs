using System.Windows.Forms;
using Promob.Builder.Forms;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.scLayout = new System.Windows.Forms.SplitContainer();
            this.tvwPlugins = new Promob.Builder.Forms.ExtendedTreeView();
            this.pgProperties = new System.Windows.Forms.PropertyGrid();
            this.tlpLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbBetas = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbCreateBetas = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbCreateTestBetas = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCreateCustomBeta = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPublish = new System.Windows.Forms.ToolStripButton();
            this.tsbInstall = new System.Windows.Forms.ToolStripButton();
            this.tsbOptions = new System.Windows.Forms.ToolStripButton();
            this.scLayout.Panel1.SuspendLayout();
            this.scLayout.Panel2.SuspendLayout();
            this.scLayout.SuspendLayout();
            this.tlpLayout.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // scLayout
            // 
            this.scLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLayout.Location = new System.Drawing.Point(7, 77);
            this.scLayout.Margin = new System.Windows.Forms.Padding(7);
            this.scLayout.Name = "scLayout";
            // 
            // scLayout.Panel1
            // 
            this.scLayout.Panel1.Controls.Add(this.tvwPlugins);
            // 
            // scLayout.Panel2
            // 
            this.scLayout.Panel2.Controls.Add(this.pgProperties);
            this.scLayout.Size = new System.Drawing.Size(770, 478);
            this.scLayout.SplitterDistance = 365;
            this.scLayout.SplitterWidth = 7;
            this.scLayout.TabIndex = 0;
            // 
            // tvwPlugins
            // 
            this.tvwPlugins.CheckBoxes = true;
            this.tvwPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwPlugins.Location = new System.Drawing.Point(0, 0);
            this.tvwPlugins.Name = "tvwPlugins";
            this.tvwPlugins.Size = new System.Drawing.Size(365, 478);
            this.tvwPlugins.TabIndex = 0;
            this.tvwPlugins.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwPlugins_BeforeCheck);
            this.tvwPlugins.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvwPlugins_AfterCheck);
            this.tvwPlugins.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwPlugins_AfterSelect);
            // 
            // pgProperties
            // 
            this.pgProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProperties.Location = new System.Drawing.Point(0, 0);
            this.pgProperties.Name = "pgProperties";
            this.pgProperties.Size = new System.Drawing.Size(398, 478);
            this.pgProperties.TabIndex = 0;
            // 
            // tlpLayout
            // 
            this.tlpLayout.ColumnCount = 1;
            this.tlpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLayout.Controls.Add(this.tsMenu, 0, 0);
            this.tlpLayout.Controls.Add(this.scLayout, 0, 1);
            this.tlpLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpLayout.Name = "tlpLayout";
            this.tlpLayout.RowCount = 2;
            this.tlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLayout.Size = new System.Drawing.Size(784, 562);
            this.tlpLayout.TabIndex = 2;
            // 
            // tsMenu
            // 
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBetas,
            this.tsbPublish,
            this.tsbInstall,
            this.tsbOptions});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(784, 70);
            this.tsMenu.TabIndex = 0;
            this.tsMenu.Text = "tsMenu";
            // 
            // tsbBetas
            // 
            this.tsbBetas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCreateBetas,
            this.tsbCreateTestBetas,
            this.toolStripSeparator1,
            this.tsbCreateCustomBeta});
            this.tsbBetas.Image = global::Promob.Builder.PluginsManagement.Properties.Resources._48x48_lego;
            this.tsbBetas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBetas.Name = "tsbBetas";
            this.tsbBetas.Size = new System.Drawing.Size(61, 67);
            this.tsbBetas.Text = "Betas";
            this.tsbBetas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbCreateBetas
            // 
            this.tsbCreateBetas.Name = "tsbCreateBetas";
            this.tsbCreateBetas.Size = new System.Drawing.Size(179, 22);
            this.tsbCreateBetas.Text = "Create Betas";
            this.tsbCreateBetas.Click += new System.EventHandler(this.tsbCreateBetas_Click);
            // 
            // tsbCreateTestBetas
            // 
            this.tsbCreateTestBetas.Name = "tsbCreateTestBetas";
            this.tsbCreateTestBetas.Size = new System.Drawing.Size(179, 22);
            this.tsbCreateTestBetas.Text = "Create Test Betas";
            this.tsbCreateTestBetas.Click += new System.EventHandler(this.tsbCreateTestBetas_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // tsbCreateCustomBeta
            // 
            this.tsbCreateCustomBeta.Name = "tsbCreateCustomBeta";
            this.tsbCreateCustomBeta.Size = new System.Drawing.Size(179, 22);
            this.tsbCreateCustomBeta.Text = "Create Custom Beta";
            this.tsbCreateCustomBeta.Click += new System.EventHandler(this.tsbCreateCustomBeta_Click);
            // 
            // tsbPublish
            // 
            this.tsbPublish.Image = global::Promob.Builder.PluginsManagement.Properties.Resources._48x48_box;
            this.tsbPublish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublish.Name = "tsbPublish";
            this.tsbPublish.Size = new System.Drawing.Size(52, 67);
            this.tsbPublish.Text = "Publish";
            this.tsbPublish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbPublish.Click += new System.EventHandler(this.tsmPublish_Click);
            // 
            // tsbInstall
            // 
            this.tsbInstall.Image = global::Promob.Builder.PluginsManagement.Properties.Resources._48x48_install;
            this.tsbInstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInstall.Name = "tsbInstall";
            this.tsbInstall.Size = new System.Drawing.Size(52, 67);
            this.tsbInstall.Text = "Install";
            this.tsbInstall.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbOptions
            // 
            this.tsbOptions.Image = global::Promob.Builder.PluginsManagement.Properties.Resources._48x48_gear1;
            this.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOptions.Name = "tsbOptions";
            this.tsbOptions.Size = new System.Drawing.Size(53, 67);
            this.tsbOptions.Text = "Options";
            this.tsbOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbOptions.Click += new System.EventHandler(this.tsbOptions_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tlpLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plugins Release Manager";
            this.scLayout.Panel1.ResumeLayout(false);
            this.scLayout.Panel2.ResumeLayout(false);
            this.scLayout.ResumeLayout(false);
            this.tlpLayout.ResumeLayout(false);
            this.tlpLayout.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scLayout;
        private ExtendedTreeView tvwPlugins;
        private System.Windows.Forms.TableLayoutPanel tlpLayout;
        private System.Windows.Forms.ToolStrip tsMenu;
        private PropertyGrid pgProperties;
        private ToolStripButton tsbPublish;
        private ToolStripButton tsbOptions;
        private ToolStripButton tsbInstall;
        private ToolStripDropDownButton tsbBetas;
        private ToolStripMenuItem tsbCreateBetas;
        private ToolStripMenuItem tsbCreateTestBetas;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsbCreateCustomBeta;
    }
}
