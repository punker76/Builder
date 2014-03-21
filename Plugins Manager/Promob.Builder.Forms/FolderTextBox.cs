using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Promob.Builder.Forms
{
    public partial class FolderTextBox : UserControl
    {
        #region Attributes and Properties

        private Color _tempColor;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.lblName.Text = value;
            }
        }

        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                this.txtPath.Text = value;

                if (!System.IO.Directory.Exists(value))
                {
                    this.txtPath.ForeColorChanged -= txtPath_ForeColorChanged;
                    this.txtPath.ForeColor = Color.Red;
                    this.txtPath.ForeColorChanged += txtPath_ForeColorChanged;
                }
                else
                    this.txtPath.ForeColor = this._tempColor;

                NotifyPathChanged();
            }
        }

        private FolderBrowserDialog _folderBrowserDialog;
        [Browsable(false)]
        public FolderBrowserDialog FolderBrowserDialog
        {
            get
            {
                if (this._folderBrowserDialog == null)
                    this._folderBrowserDialog = new FolderBrowserDialog();

                return _folderBrowserDialog;
            }
            set { _folderBrowserDialog = value; }
        }

        #endregion

        #region Constructors

        public FolderTextBox()
        {
            InitializeComponent();
            this.txtPath.TextChanged += txtPath_TextChanged;
            this.txtPath.ForeColorChanged += txtPath_ForeColorChanged;
            this.btnBrowse.Click += btnBrowse_Click;
        }

        #endregion

        #region Events

        public event EventHandler PathChanged;

        #endregion

        #region Events Methods

        private void txtPath_TextChanged(object sender, System.EventArgs e)
        {
            this.Path = this.txtPath.Text;
        }

        private void txtPath_ForeColorChanged(object sender, EventArgs e)
        {
            this._tempColor = this.txtPath.ForeColor;
        }

        private void btnBrowse_Click(object sender, System.EventArgs e)
        {
            DialogResult dr = this.FolderBrowserDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.Path = this.FolderBrowserDialog.SelectedPath;
            }
        }

        #endregion

        #region Overriden Methods

        protected override void OnResize(System.EventArgs e)
        {
            this.Height = 36;
            base.OnResize(e);
        }

        #endregion

        #region Private Methods

        private void NotifyPathChanged()
        {
            if (PathChanged != null)
                PathChanged(this, new EventArgs());
        }

        #endregion
    }
}
