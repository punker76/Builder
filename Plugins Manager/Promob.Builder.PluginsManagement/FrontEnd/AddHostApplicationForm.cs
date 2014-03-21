using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class AddHostApplicationForm : Form
    {
        #region Attributes and Properties

        private HostApplication _hostApplication;
        public HostApplication HostApplication
        {
            get { return _hostApplication; }
            set { _hostApplication = value; }
        }

        private HostApplications _hostApplications;
        public HostApplications HostApplications
        {
            get { return _hostApplications; }
            set { _hostApplications = value; }
        }

        private bool _editMode;

        #endregion

        #region Constructors

        public AddHostApplicationForm(HostApplications hostApplications)
        {
            this._hostApplications = hostApplications;
            this._editMode = false;
            this.Init();
        }

        public AddHostApplicationForm(HostApplication hostApplication)
        {
            this._hostApplication = hostApplication;
            this._editMode = true;
            this.Init();
        }

        #endregion

        #region Event Methods

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            this.Save();
            this.Close();
        }

        #endregion

        #region Overriden Methods

        protected override void OnLoad(System.EventArgs e)
        {
            if (!this._editMode)
                this._hostApplication = new HostApplication();

            this.LoadHostApplication();
        }

        protected override void OnShown(System.EventArgs e)
        {
            this.txtName.Focus();
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            this.InitializeComponent();

            this.Text = !this._editMode ? "Add Host Application" : "Edit Host Application";
            this.clbAllowedDistributions.DataSource = HostApplication.ALLOWED_DISTRIBUTIONS;
        }

        private void LoadHostApplication()
        {
            this.txtName.Text = this._hostApplication.Name;
            this.txtMinVersion.Text = this._hostApplication.MinVersion;

            for (int i = 0; i < HostApplication.ALLOWED_DISTRIBUTIONS.Count; i++)
                if (this._hostApplication.AllowedDistributions.Contains(HostApplication.ALLOWED_DISTRIBUTIONS[i]))
                    this.clbAllowedDistributions.SetItemChecked(i, true);
        }

        private void Save()
        {
            this._hostApplication.Name = this.txtName.Text;
            this._hostApplication.MinVersion = this.txtMinVersion.Text;

            AllowedDistributions allowedDistributions = new AllowedDistributions();

            for (int i = 0; i < this.clbAllowedDistributions.CheckedItems.Count; i++)
                allowedDistributions.Add(this.clbAllowedDistributions.CheckedItems[i].ToString());

            this._hostApplication.AllowedDistributions = allowedDistributions;

            if (!this._editMode)
                this._hostApplications.Add(this._hostApplication);
        }

        #endregion
    }
}
