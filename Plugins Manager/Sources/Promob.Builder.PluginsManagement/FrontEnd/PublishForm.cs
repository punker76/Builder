using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Promob.Builder.Email;
using Promob.Builder.Options;
using Promob.Builder.PluginsManagement.BackEnd;
using Promob.Builder.PluginsManagement.BackEnd.Options;
using Promob.Builder.PluginsManagement.BackEnd.Publish;
using Promob.Builder.Translation;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class PublishForm : Form, ITranslatable
    {
        #region Constructors

        public PublishForm(List<PluginVersion> versions)
        {
            this._versions = versions;
            this.InitializeComponent();
            this.Init();
            this.Translate();
        }

        #endregion

        #region Attributes and Properties

        private List<PluginVersion> _versions;
        public List<PluginVersion> Versions
        {
            get { return this._versions; }
            set { this._versions = value; }
        }

        public PublishOptions PublishOptions
        {
            get
            {
                return OptionsManager.GetManager().OptionsCollection["PublishOptions"] as PublishOptions;
            }
        }

        private BindingList<PublishingVersionInfo> _publishVersionDataSource;
        private BindingList<PublishingVersionInfo> PublishVersionDataSource
        {
            get
            {
                if (this._publishVersionDataSource == null)
                    this._publishVersionDataSource = new BindingList<PublishingVersionInfo>();

                return this._publishVersionDataSource;
            }
        }

        private Cursor _currentCursor = Cursors.Default;

        private bool _publicationCompleted = false;

        #endregion

        #region Private Methods

        private void Init()
        {
            this.Load += PublishForm_Load;
            this.txtLocalMediaPath.Text = this.PublishOptions.LocalMediaPath;
            this.cbSaveLocalMediaCopy.CheckedChanged += cbSaveLocalMediaCopy_CheckedChanged;
            this.btnBrowse.Click += btnBrowse_Click;
            PublishingManager.Instance.OnPublishCompleted += OnPublishCompleted;
        }

        private void EnableControls()
        {
            this.btnAction.Enabled = true;
            this.btnBrowse.Enabled = true;
            this.cbSaveLocalMediaCopy.Enabled = true;
            this.txtLocalMediaPath.Enabled = true;
            this.dgvPublishVersions.Enabled = true;
        }

        private void DisableControls()
        {
            this.btnAction.Enabled = false;
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Enabled = false;
            this.cbSaveLocalMediaCopy.Enabled = false;
            this.txtLocalMediaPath.Enabled = false;
            this.dgvPublishVersions.Enabled = false;
        }

        private void LoadPublishVersionsDataGridView()
        {
            this.LoadPublishVersionDataSource();
            this.dgvPublishVersions.AutoGenerateColumns = false;
            this.colMessage.UseColumnTextForButtonValue = true;
            this.dgvPublishVersions.DataSource = this.PublishVersionDataSource;
        }

        private void LoadPublishVersionDataSource()
        {
            foreach (PluginVersion version in this.Versions)
            {
                PublishingVersionInfo publishingVersionInfo = new PublishingVersionInfo(version);
                this.PublishVersionDataSource.Add(publishingVersionInfo);
            }
        }

        private bool CanExit()
        {
            this.Cursor = Cursors.Default;

            if (PublishingManager.Instance.Publishing)
            {
                DialogResult dr = MessageBox.Show(
                    TranslationManager.GetManager().Translate("PublishForm.Mbox_Caption.CloseMessage"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                    PublishingManager.Instance.Cancel(true, "CANCELED");
                else
                    return false;
            }

            this.Cursor = this._currentCursor;

            return true;
        }

        public void Translate()
        {
            this.Text = TranslationManager.GetManager().Translate("Publish");

            this.cbSaveLocalMediaCopy.Text = TranslationManager.GetManager().Translate("Save Local Media Copy");

            this.gbGeneral.Text = TranslationManager.GetManager().Translate("General");
            this.gbPlugins.Text = TranslationManager.GetManager().Translate("Plugins");

            this.colMessage.HeaderText = TranslationManager.GetManager().Translate("Message");
            this.colVersionName.HeaderText = TranslationManager.GetManager().Translate("Version");
            this.colReleaseVersion.HeaderText = TranslationManager.GetManager().Translate("New Version");
            this.colCreatePatch.HeaderText = TranslationManager.GetManager().Translate("Create Patch");
            this.btnAction.Text = TranslationManager.GetManager().Translate("Publish");
        }

        public void SendEmail()
        {
            if (this.cbxSendEmail.Checked == false ||
                this.PublishOptions.EmailList == string.Empty)
                return;

            if (this.PublishVersionDataSource.Count == 0)
                return;

            string products = string.Empty;

            foreach (PublishingVersionInfo version in this.PublishVersionDataSource)
                if (version.Status == PublishingStatus.Succeeded)
                    products += version.PluginName + " " + version.ReleaseVersion + "\n";

            if (string.IsNullOrEmpty(products))
                return;

            EmailManager.GetManager().SendEmail(PublishOptions.EmailList, products);
        }

        #endregion

        #region Signed Events Methods

        private void PublishForm_Load(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.LoadPublishVersionsDataGridView();
            this.Cursor = Cursors.Default;
        }

        private void PublishForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.CanExit())
                e.Cancel = true;
            else
            {
                this.Cursor = Cursors.Default;
                this.DialogResult = DialogResult.OK;
                PublishingManager.Instance.OnPublishCompleted -= OnPublishCompleted;
            }
        }

        private void cbSaveLocalMediaCopy_CheckedChanged(object sender, System.EventArgs e)
        {
            this.btnBrowse.Enabled = this.cbSaveLocalMediaCopy.Checked;
            this.txtLocalMediaPath.Enabled = this.cbSaveLocalMediaCopy.Checked;
        }

        private void btnBrowse_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = this.txtLocalMediaPath.Text;
            DialogResult dr = folderBrowserDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                DialogResult save = MessageBox.Show(
                    TranslationManager.GetManager().Translate("PublishForm.Mbox_Caption.SaveLocalMediaPathToOptions"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk);

                if (save == DialogResult.Yes)
                {
                    PublishOptions.LocalMediaPath = folderBrowserDialog.SelectedPath;
                    PublishOptions.Save();
                }

                this.txtLocalMediaPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnAction_Click(object sender, System.EventArgs e)
        {
            if (!this._publicationCompleted)
            {
                this.Cursor = this._currentCursor = Cursors.WaitCursor;

                List<PublishingVersionInfo> infos = new List<PublishingVersionInfo>();

                foreach (DataGridViewRow row in this.dgvPublishVersions.Rows)
                {
                    PublishingVersionInfo info = row.DataBoundItem as PublishingVersionInfo;
                    info.SetStatus(PublishingStatus.Pending, string.Empty);
                    infos.Add(info);
                }

                PublishingManager.Instance.Publish(infos, this.cbSaveLocalMediaCopy.Checked, this.txtLocalMediaPath.Text);

                this.DisableControls();
            }
            else
            {
                this.Close();
            }
        }

        private void dgvPublishVersions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPublishVersions.Columns[e.ColumnIndex].Name.Equals("colMessage"))
            {
                PublishingVersionInfo info =
                    this.dgvPublishVersions[e.ColumnIndex, e.RowIndex].OwningRow.DataBoundItem as PublishingVersionInfo;

                MessageForm form = new MessageForm(info.Message);
                form.ShowDialog();
            }
            else if (dgvPublishVersions.Columns[e.ColumnIndex].Name.Equals("colCreatePatch"))
                this.dgvPublishVersions.EndEdit();

        }

        private void dgvPublishVersions_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is System.ArgumentException)
            {
                MessageBox.Show(
                    TranslationManager.GetManager().Translate("ArgumentException", TranslationManager.GetManager().Translate(e.Exception.Message)),
                    TranslationManager.GetManager().Translate("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (e.Exception is System.InvalidOperationException)
            {
                MessageBox.Show(
                    TranslationManager.GetManager().Translate(e.Exception.Message),
                    TranslationManager.GetManager().Translate("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OnPublishCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.SendEmail();

            if (!e.Cancelled && e.Error == null)
            {
                MessageBox.Show(
                    TranslationManager.GetManager().Translate("PublishForm.Mbox_Caption.OperationSuccessful"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (e.Error != null && e.Error is PublishingException)
            {
                MessageBox.Show(
                    TranslationManager.GetManager().Translate("PublishForm.Mbox_Caption.OperationFinishedWithErrors"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            this.btnAction.Text = this.btnAction.Text = TranslationManager.GetManager().Translate("Close");
            this.colReleaseVersion.ReadOnly = true;
            this.colCreatePatch.ReadOnly = true;
            this.btnAction.Enabled = true;
            this.dgvPublishVersions.Enabled = true;
            this.Cursor = Cursors.Default;
            this._publicationCompleted = true;
        }

        #endregion
    }
}
