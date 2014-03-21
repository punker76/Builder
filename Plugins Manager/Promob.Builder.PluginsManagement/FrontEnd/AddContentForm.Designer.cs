using Promob.Builder.Forms;
namespace Promob.Builder.PluginsManagement.FrontEnd
{
    partial class AddContentForm
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
            this.tvContent = new Promob.Builder.Forms.ExtendedTreeView();
            this.SuspendLayout();
            // 
            // tvContent
            // 
            this.tvContent.CheckBoxes = true;
            this.tvContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvContent.Location = new System.Drawing.Point(0, 0);
            this.tvContent.Name = "tvContent";
            this.tvContent.Size = new System.Drawing.Size(434, 566);
            this.tvContent.TabIndex = 2;
            this.tvContent.TabStop = false;
            // 
            // AddContentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 566);
            this.Controls.Add(this.tvContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AddContentForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddContentForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedTreeView tvContent;
    }
}