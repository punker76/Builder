using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Promob.Builder.Forms
{
    public partial class FilenameTextBox : UserControl
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

                if (!System.IO.File.Exists(value))
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

        private OpenFileDialog _openFileDialog;
        [Browsable(false)]
        public OpenFileDialog OpenFileDialog
        {
            get
            {
                if (this._openFileDialog == null)
                    this._openFileDialog = new OpenFileDialog();

                return _openFileDialog;
            }
            set { _openFileDialog = value; }
        }

        private bool _allowMultipleFileSelection = false;

        #endregion

        #region Constructors

        public FilenameTextBox()
        {
            InitializeComponent();
            this.txtPath.TextChanged += txtPath_TextChanged;
            this.txtPath.ForeColorChanged += txtPath_ForeColorChanged;
            this.btnBrowse.Click += btnBrowse_Click;

            this.OpenFileDialog.CheckFileExists = true;
            this.OpenFileDialog.CheckPathExists = true;
            this.OpenFileDialog.Multiselect = this._allowMultipleFileSelection;
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
            DialogResult dr = this.OpenFileDialog.ShowDialog();

            if (dr == DialogResult.OK)
                this.Path = this.OpenFileDialog.FileName;
        }

        #endregion

        #region Overriden Methods

        protected override void OnTextChanged(System.EventArgs e)
        {
            this.lblName.Text = this.Text;
        }

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
