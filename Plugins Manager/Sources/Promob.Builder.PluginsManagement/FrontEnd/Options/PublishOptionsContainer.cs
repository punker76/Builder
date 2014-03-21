using System.Linq;
using Promob.Builder.Email;
using Promob.Builder.Options;
using Promob.Builder.PluginsManagement.BackEnd.Options;
using Promob.Builder.Translation;

namespace Promob.Builder.PluginsManagement.FrontEnd.Options
{
    public partial class PublishOptionsContainer : OptionsContainer
    {
        #region Constructors

        public PublishOptionsContainer()
            : base()
        {
            this.InitializeComponent();
            this.InitControls();
        }

        public PublishOptionsContainer(PublishOptions publishOptions)
            : base()
        {
            this._publishOptions = publishOptions;
            this.InitializeComponent();
            this.InitControls();
        }

        #endregion

        #region Attributes and Properties

        private PublishOptions _publishOptions;
        public PublishOptions PublishOptions
        {
            get
            {
                if (this._publishOptions == null)
                    this._publishOptions = new PublishOptions(string.Empty);

                return _publishOptions;
            }
            set
            {
                this._publishOptions = value;
                this.LoadPublishOptions();
            }
        }

        #endregion

        #region Overriden Methods

        public override void Translate()
        {
            base.Translate();

            this.txtExecutablePath.Text = TranslationManager.GetManager().Translate("Executable Path");
            this.txtInstallPath.Text = TranslationManager.GetManager().Translate("Install Path");
            this.txtLocalMediaPath.Text = TranslationManager.GetManager().Translate("Local Media Path");
            this.txtBetasPath.Text = TranslationManager.GetManager().Translate("Betas Path");
            this.lblEmail.Text = TranslationManager.GetManager().Translate("Email");
        }

        #endregion

        #region Private Methods

        private void AddEmailsToList()
        {
            foreach (string email in EmailManager.GetManager().SelectedContacts.Distinct())
                this.txtEmailList.Text += email + "; ";

            this.PublishOptions.EmailList = this.txtEmailList.Text;
        }

        private void InitControls()
        {
            this.txtEmail.TextChanged += EmailChanged;
            this.txtExecutablePath.PathChanged += ExecutablePathChanged;
            this.txtInstallPath.PathChanged += InstallPathChanged;
            this.txtLocalMediaPath.PathChanged += LocalMediaPathChanged;
            this.txtBetasPath.PathChanged += BetasPathChanged;
            this.LoadPublishOptions();
        }

        private void LoadPublishOptions()
        {
            this.txtExecutablePath.Path = this.PublishOptions.ExecutablePath;
            this.txtInstallPath.Path = this.PublishOptions.InstallPath;
            this.txtLocalMediaPath.Path = this.PublishOptions.LocalMediaPath;
            this.txtBetasPath.Path = this.PublishOptions.BetasPath;
            this.txtEmail.Text = this.PublishOptions.Email;
            this.txtEmailList.Text = this.PublishOptions.EmailList;
        }

        #endregion

        #region Signed Event Methods

        private void BetasPathChanged(object sender, System.EventArgs e)
        {
            this.PublishOptions.BetasPath = this.txtBetasPath.Path;
        }

        private void btnContacts_Click(object sender, System.EventArgs e)
        {
            ContactsListForm contactsListForm = new ContactsListForm();

            contactsListForm.EmailAdded += EmailAdded;

            contactsListForm.ShowDialog();
        }

        private void EmailAdded()
        {
            this.AddEmailsToList();
        }

        private void EmailChanged(object sender, System.EventArgs e)
        {
            this.PublishOptions.Email = this.txtEmail.Text;
        }

        private void ExecutablePathChanged(object sender, System.EventArgs e)
        {
            this.PublishOptions.ExecutablePath = this.txtExecutablePath.Path;
        }

        private void InstallPathChanged(object sender, System.EventArgs e)
        {
            this.PublishOptions.InstallPath = this.txtInstallPath.Path;
        }

        private void LocalMediaPathChanged(object sender, System.EventArgs e)
        {
            this.PublishOptions.LocalMediaPath = this.txtLocalMediaPath.Path;
        }

        private void txtEmailList_Leave(object sender, System.EventArgs e)
        {
            this.PublishOptions.EmailList = this.txtEmailList.Text;
        }

        #endregion
    }
}
