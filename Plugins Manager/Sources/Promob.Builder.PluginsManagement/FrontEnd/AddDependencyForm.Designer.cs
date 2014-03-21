using Promob.Builder.Forms;
using Promob.Builder.PluginsManagement.FrontEnd.TreeView;
namespace Promob.Builder.PluginsManagement.FrontEnd
{
    partial class AddDependencyForm
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
            this.tvDependencies = new Promob.Builder.Forms.ExtendedTreeView();
            this.SuspendLayout();
            // 
            // tvDependencies
            // 
            this.tvDependencies.CheckBoxes = true;
            this.tvDependencies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDependencies.Indent = 19;
            this.tvDependencies.ItemHeight = 19;
            this.tvDependencies.Location = new System.Drawing.Point(0, 0);
            this.tvDependencies.Name = "tvDependencies";
            this.tvDependencies.Size = new System.Drawing.Size(434, 566);
            this.tvDependencies.TabIndex = 0;
            this.tvDependencies.TabStop = false;
            // 
            // AddDependencyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 566);
            this.Controls.Add(this.tvDependencies);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AddDependencyForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddDependencyForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedTreeView tvDependencies;
    }
}