using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Promob.Builder.Options;
using Promob.Builder.PluginsManagement.BackEnd;
using Promob.Builder.PluginsManagement.BackEnd.Options;
using Promob.Builder.Translation;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class PluginDataEditorForm : Form, ITranslatable
    {
        #region Attributes and Properties

        private PluginData _pluginData;
        public PluginData PluginData
        {
            get { return this._pluginData; }
            set { this._pluginData = value; }
        }

        private bool _checkAllStatus = false;

        #endregion

        #region Constructors

        public PluginDataEditorForm(PluginData pluginData)
        {
            this._pluginData = pluginData;
            this.InitializeComponent();
            this.Translate();
        }

        #endregion

        #region Events Methods

        private void PluginDataEditorForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.PluginData.UpdateCheckout();
            this.Init();
            this.Cursor = Cursors.Default;
        }

        private void PluginDataEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.PluginData.Unlock();
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (System.IO.File.Exists(this.PluginData.Path))
                Process.Start(System.IO.Path.GetDirectoryName(this.PluginData.Path));
            else
            {
                DialogResult drProductsManager = MessageBox.Show(
                    TranslationManager.GetManager().Translate("PluginDataEditorForm.Mbox_Caption.LocalFolderNotFound"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk);

                if (drProductsManager == DialogResult.Yes)
                    this.TryOpenProductsManager();
            }

            this.Cursor = Cursors.Default;
        }

        private void btnAddHostApplication_Click(object sender, EventArgs e)
        {
            AddHostApplicationForm form = new AddHostApplicationForm(this.PluginData.HostApplications);
            form.ShowDialog();
            this.dgvHostApplications.Update();
            this.UpdateControls();
        }

        private void btnEditHostApplication_Click(object sender, EventArgs e)
        {
            HostApplication hostApplication = this.dgvHostApplications.SelectedRows[0].DataBoundItem as HostApplication;

            AddHostApplicationForm form = new AddHostApplicationForm(hostApplication);
            form.ShowDialog();

            this.dgvHostApplications.EndEdit();
        }

        private void btnRemoveHostApplication_Click(object sender, EventArgs e)
        {
            DialogResult dr =
                    MessageBox.Show(
                        TranslationManager.GetManager().Translate("PluginDataEditorForm.Mbox_Caption.RemoveSelectedHostApplications"),
                        TranslationManager.GetManager().Translate("Attention"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation);

            if (dr != DialogResult.Yes)
                return;

            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in this.dgvHostApplications.Rows)
            {
                if (row.Index == -1)
                    continue;

                if (row.Cells[0] != null && row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value))
                    rowsToRemove.Add(row);
            }

            foreach (DataGridViewRow row in rowsToRemove)
                this.dgvHostApplications.Rows.Remove(row);

            this.dgvHostApplications.Update();
            this.UpdateControls();

            this.btnRemoveHostApplication.Enabled = false;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            this.PluginData.Save();
            this.Close();
        }

        private void tsbAddDependency_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            AddDependencyForm form = new AddDependencyForm(this.PluginData);
            form.ShowDialog();
            this.dgvDependencies.Update();
            this.Cursor = Cursors.Default;
        }

        private void tsbRemoveSelectedDependency_Click(object sender, EventArgs e)
        {
            DialogResult dr =
                    MessageBox.Show(
                        TranslationManager.GetManager().Translate("PluginDataEditorForm.Mbox_Caption.RemoveSelectedDependencies"),
                        TranslationManager.GetManager().Translate("Attention"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation);

            if (dr != DialogResult.Yes)
                return;

            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in this.dgvDependencies.Rows)
            {
                if (row.Index == -1)
                    continue;

                if (row.Cells[0] != null && row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value))
                    rowsToRemove.Add(row);
            }

            foreach (DataGridViewRow row in rowsToRemove)
                this.dgvDependencies.Rows.Remove(row);

            this.dgvDependencies.Update();

            tsbRemoveSelectedContent.Enabled = false;
        }

        private void tsbAddContent_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            AddContentForm form = new AddContentForm(this.PluginData);
            form.ShowDialog();
            this.dgvContent.Update();
            this.Cursor = Cursors.Default;
        }

        private void tsbRemoveSelectedContent_Click(object sender, System.EventArgs e)
        {
            DialogResult dr =
                    MessageBox.Show(
                        TranslationManager.GetManager().Translate("PluginDataEditorForm.Mbox_Caption.RemoveSelectedContents"),
                        TranslationManager.GetManager().Translate("Attention"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation);

            if (dr != DialogResult.Yes)
                return;

            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in this.dgvContent.Rows)
            {
                if (row.Index == -1)
                    continue;

                if (row.Cells[0] != null && row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value))
                    rowsToRemove.Add(row);
            }

            foreach (DataGridViewRow row in rowsToRemove)
                this.dgvContent.Rows.Remove(row);

            this.dgvContent.Update();

            this.tsbRemoveSelectedContent.Enabled = false;
        }

        private void dgvHostApplications_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                DataGridViewRow[] copy = new DataGridViewRow[this.dgvHostApplications.SelectedRows.Count];
                DataGridViewSelectedRowCollection collection = this.dgvHostApplications.SelectedRows;
                collection.CopyTo(copy, 0);

                this.dgvHostApplications.ClearSelection();

                foreach (DataGridViewRow row in this.dgvHostApplications.Rows)
                {
                    if (row.Index == -1)
                        continue;

                    row.Cells[0].Value = (!_checkAllStatus).ToString();
                }

                this._checkAllStatus = !this._checkAllStatus;

                if (this._checkAllStatus == true)
                    this.btnRemoveHostApplication.Enabled = true;
                else
                    this.btnRemoveHostApplication.Enabled = false;

                this.dgvHostApplications.EndEdit();

                foreach (DataGridViewRow row in copy)
                    row.Selected = true;

                this.dgvHostApplications.Update();
            }
        }

        private void dgvHostApplications_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                this.dgvHostApplications.EndEdit();

            this.UpdateControls();
        }

        private void dgvDependencies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                DataGridViewRow[] copy = new DataGridViewRow[this.dgvDependencies.SelectedRows.Count];
                DataGridViewSelectedRowCollection collection = this.dgvDependencies.SelectedRows;
                collection.CopyTo(copy, 0);

                this.dgvDependencies.ClearSelection();

                foreach (DataGridViewRow row in this.dgvDependencies.Rows)
                {
                    if (row.Index == -1)
                        continue;

                    row.Cells[0].Value = (!this._checkAllStatus).ToString();
                }

                this._checkAllStatus = !this._checkAllStatus;

                if (this._checkAllStatus == true)
                    this.tsbRemoveSelectedDependency.Enabled = true;
                else
                    this.tsbRemoveSelectedDependency.Enabled = false;

                this.dgvDependencies.EndEdit();

                foreach (DataGridViewRow row in copy)
                    row.Selected = true;

                this.dgvDependencies.Update();
            }
        }

        private void dgvDependencies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                this.dgvDependencies.EndEdit();

            this.UpdateControls();
        }

        private void dgvContent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                DataGridViewRow[] copy = new DataGridViewRow[this.dgvContent.SelectedRows.Count];
                DataGridViewSelectedRowCollection collection = this.dgvContent.SelectedRows;
                collection.CopyTo(copy, 0);

                this.dgvContent.ClearSelection();

                foreach (DataGridViewRow row in this.dgvContent.Rows)
                {
                    if (row.Index == -1)
                        continue;

                    row.Cells[0].Value = (!this._checkAllStatus).ToString();
                }

                this._checkAllStatus = !this._checkAllStatus;

                if(this._checkAllStatus == true)
                    this.tsbRemoveSelectedContent.Enabled = true;
                else
                    this.tsbRemoveSelectedContent.Enabled = false;

                this.dgvContent.EndEdit();

                foreach (DataGridViewRow row in copy)
                    row.Selected = true;

                this.dgvContent.Update();

            }
        }

        private void dgvContent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                this.dgvContent.EndEdit();

            this.UpdateControls();
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            this.InitControls();
        }

        private void InitControls()
        {
            this.InitDataGridViewHostApplications();
            this.InitDataGridViewDependencies();
            this.InitDataGridViewContent();
            this.UpdateControls();
        }

        private void InitDataGridViewHostApplications()
        {
            this.dgvHostApplications.AutoGenerateColumns = false;
            this.dgvHostApplications.DataSource = this.PluginData.HostApplications;
            this.dgvHostApplications.CellContentClick += dgvHostApplications_CellContentClick;
            this.dgvHostApplications.CellClick += dgvHostApplications_CellClick;
        }

        private void InitDataGridViewDependencies()
        {
            this.dgvDependencies.AutoGenerateColumns = false;
            this.dgvDependencies.DataSource = this.PluginData.DependencyAssemblies;
            this.dgvDependencies.CellContentClick += dgvDependencies_CellContentClick;
            this.dgvDependencies.CellClick += dgvDependencies_CellClick;
        }

        private void InitDataGridViewContent()
        {
            this.dgvContent.AutoGenerateColumns = false;
            this.dgvContent.DataSource = this.PluginData.Content;
            this.dgvContent.CellContentClick += dgvContent_CellContentClick;
            this.dgvContent.CellClick += dgvContent_CellClick;
        }

        private void UpdateControls()
        {
            this.lblValueName.Text = this.PluginData.Name;
            this.lblValueDescription.Text = this.PluginData.Description;
            this.lblValueVersion.Text = this.PluginData.Version;
            this.lblValueId.Text = this.PluginData.Id.ToString();
            this.lblValueProductId.Text = this.PluginData.ProductId.ToString();
            this.btnEditHostApplication.Enabled = this.dgvHostApplications.Rows.Count > 0;
            this.btnRemoveHostApplication.Enabled = this.HasAnyRowSelected(this.dgvHostApplications);
            this.tsbRemoveSelectedContent.Enabled = this.HasAnyRowSelected(this.dgvContent);
            this.tsbRemoveSelectedDependency.Enabled = this.HasAnyRowSelected(this.dgvDependencies);
        }

        private bool HasAnyRowSelected(DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index == -1)
                    continue;

                if (row.Cells[0] != null && row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value))
                    return true;
                string conteudo = Convert.ToString(row.Cells[1].Value);
                bool test = Convert.ToBoolean(row.Cells[0].Value);
            }

            return false;
        }

        public void Translate()
        {
            this.Text = string.Format("{0} ({1} - {2})", TranslationManager.GetManager().Translate("Plugin Data Editor"), this.PluginData.Parent.Product.Name, this.PluginData.Parent.VersionNumber);

            this.tpContent.Text = TranslationManager.GetManager().Translate("Content");
            this.tpDependencies.Text = TranslationManager.GetManager().Translate("Dependencies");
            this.tpGeneral.Text = TranslationManager.GetManager().Translate("General");

            this.colContentDestiny.HeaderText = TranslationManager.GetManager().Translate("Destiny");
            this.colContentName.HeaderText = TranslationManager.GetManager().Translate("Name");
            this.colContentShared.HeaderText = TranslationManager.GetManager().Translate("Shared");
            this.colDependencyFilename.HeaderText = TranslationManager.GetManager().Translate("Filename");
            this.colHostDistributionsAllowedDistributions.HeaderText = TranslationManager.GetManager().Translate("Allowed Distributions");
            this.colHostDistributionsMinVersion.HeaderText = TranslationManager.GetManager().Translate("Min. Version");
            this.colHostDistributionsName.HeaderText = TranslationManager.GetManager().Translate("Name");

            this.gbHostApplications.Text = TranslationManager.GetManager().Translate("Host Applications");

            this.btnBrowse.Text = TranslationManager.GetManager().Translate("Browse");
            this.btnAddHostApplication.Text = TranslationManager.GetManager().Translate("Add");
            this.btnEditHostApplication.Text = TranslationManager.GetManager().Translate("Edit");
            this.btnRemoveHostApplication.Text = TranslationManager.GetManager().Translate("Remove");
            this.btnSave.Text = TranslationManager.GetManager().Translate("Save");

            this.lblCaptionDescription.Text = string.Format("{0}:", TranslationManager.GetManager().Translate("Description"));
            this.lblCaptionName.Text = string.Format("{0}:", TranslationManager.GetManager().Translate("Name"));
            this.lblCaptionProductId.Text = string.Format("{0}:", TranslationManager.GetManager().Translate("Product Id"));
            this.lblCaptionVersion.Text = string.Format("{0}:", TranslationManager.GetManager().Translate("Version"));

            this.tsbAddDependency.Text = TranslationManager.GetManager().Translate("Add dependency");
            this.tsbRemoveSelectedDependency.Text = TranslationManager.GetManager().Translate("Remove selected dependency");
            this.tsbAddContent.Text = TranslationManager.GetManager().Translate("Add content");
            this.tsbRemoveSelectedContent.Text = TranslationManager.GetManager().Translate("Remove selected content");
        }

        private void TryOpenProductsManager()
        {
            string executablePath = (OptionsManager.GetManager().OptionsCollection["GeneralOptions"] as GeneralOptions).ProductsManagerExecutablePath;

            if (!System.IO.File.Exists(executablePath))
            {
                DialogResult dr = MessageBox.Show(
                    TranslationManager.GetManager().Translate("PluginDataEditorForm.Mbox_Caption.ProductsManagerExecutableInvalid"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

                if (OptionsManager.GetManager().Form.ShowDialog() == DialogResult.OK)
                    TryOpenProductsManager();
            }
            else
                Process.Start((OptionsManager.GetManager().OptionsCollection["GeneralOptions"] as GeneralOptions).ProductsManagerExecutablePath);
        }

        #endregion
    }
}