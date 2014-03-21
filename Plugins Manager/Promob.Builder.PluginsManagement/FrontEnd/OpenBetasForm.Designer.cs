namespace Promob.Builder.PluginsManagement.FrontEnd
{
    partial class OpenBetasForm
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
            this.dgvBetas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBetas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBetas
            // 
            this.dgvBetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBetas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBetas.Location = new System.Drawing.Point(0, 0);
            this.dgvBetas.Name = "dgvBetas";
            this.dgvBetas.Size = new System.Drawing.Size(429, 316);
            this.dgvBetas.TabIndex = 0;
            // 
            // OpenBetasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 316);
            this.Controls.Add(this.dgvBetas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OpenBetasForm";
            this.Text = "OpenBetasForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBetas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBetas;
    }
}