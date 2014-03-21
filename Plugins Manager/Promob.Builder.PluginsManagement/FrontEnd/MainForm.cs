using Promob.Builder.Betas;
using Promob.Builder.Options;
using Promob.Builder.PluginsManagement.BackEnd;
using Promob.Builder.PluginsManagement.FrontEnd.TreeView;
using Promob.Builder.SVN;
using Promob.Builder.Translation;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class MainForm : Form, ITranslatable
    {
        #region Constructors

        public MainForm()
        {
            this.InitializeComponent();
            this.InitMenu();

            this.Load += MainForm_Load;

            this.Translate();
        }

        private void InitMenu()
        {
            this.Text += " " + Assembly.GetEntryAssembly().GetName().Version.ToString();
            this.tsbCreateBetas.Image = Promob.Builder.PluginsManagement.Properties.Resources._16x16_lego;
            this.tsbCreateBetas.ImageScaling = ToolStripItemImageScaling.None;
            this.tsbCreateTestBetas.Image = Promob.Builder.PluginsManagement.Properties.Resources._16x16_lego_unmounted;
            this.tsbCreateTestBetas.ImageScaling = ToolStripItemImageScaling.None;
            this.tsbCreateCustomBeta.Image = Promob.Builder.PluginsManagement.Properties.Resources._16x16_lego_mounted;
            this.tsbCreateCustomBeta.ImageScaling = ToolStripItemImageScaling.None;

        }

        #endregion

        #region Attributes and Properties

        private PluginCollection _plugins;
        public PluginCollection Plugins
        {
            get
            {
                if (this._plugins == null)
                    this._plugins = new PluginCollection();

                return this._plugins;
            }
        }

        private List<PluginVersionTreeNode> _selectedNodes;
        public List<PluginVersionTreeNode> SelectedNodes
        {
            get
            {
                if (this._selectedNodes == null)
                    this._selectedNodes = new List<PluginVersionTreeNode>();

                return this._selectedNodes;
            }
            set { this._selectedNodes = value; }
        }

        #endregion

        #region Private Methods

        private void CreateBetas()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult dr = folderDialog.ShowDialog();

            if (dr != DialogResult.OK)
                return;
            Dictionary<PluginVersion, string> betasPaths = new Dictionary<PluginVersion, string>();

            foreach (PluginVersionTreeNode node in this.SelectedNodes)
                betasPaths.Add(node.PluginVersion, (node.PluginVersion as PluginVersion).CreateBeta(folderDialog.SelectedPath, false));

            MessageBox.Show(
                    TranslationManager.GetManager().Translate("MainForm.Mbox_Caption.BetaCreationSuccessful"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void CreateTestBetas()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult dr = folderDialog.ShowDialog();

            if (dr != DialogResult.OK)
                return;

            Dictionary<PluginVersion, string> betasPaths = new Dictionary<PluginVersion, string>();

            foreach (PluginVersionTreeNode node in this.SelectedNodes)
                betasPaths.Add(node.PluginVersion, (node.PluginVersion as PluginVersion).CreateBeta(folderDialog.SelectedPath, true));

            MessageBox.Show(
                    TranslationManager.GetManager().Translate("MainForm.Mbox_Caption.BetaCreationSuccessful"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void LoadPlugins()
        {
            Dictionary<string, string> urls = SVNManager.Instance.List(SVNManager.PLUGINS_URL);

            if (urls == null)
                return;

            foreach (string name in urls.Keys)
                this.Plugins.Add(new Plugin(name));
        }

        private void LoadPluginTreeView()
        {
            if(true)
            foreach (Plugin plugin in this._plugins)
            {
                PluginTreeNode node = new PluginTreeNode(plugin);
                this.tvwPlugins.Nodes.Add(node);
            }
        }

        private void SelectPluginTreeNode()
        {
            PluginTreeNode node = this.tvwPlugins.SelectedNode as PluginTreeNode;
            Plugin plugin = node.Plugin;

            if (plugin.Versions.Count == 0)
                plugin.Load();

            if (node.Nodes.Count == 0)
            {

                foreach (PluginVersion version in node.Plugin.Versions)
                {
                    if (node.LastVersion == null)
                    {
                        node.LastVersion = new PluginVersionTreeNode(version);
                        if (node.Plugin.Versions.Count == 1)
                            node.Text = node.Plugin.Name + " (Current: 1.0.0)";
                    }
                    else
                        node.Text = node.Plugin.Name + " (Last Version: " + version.VersionNumber + ")";
                }
            }
            else
                return;

            PluginVersion pluginVersion = node.LastVersion.PluginVersion as PluginVersion;

            if (!pluginVersion.BetaData.Created || !pluginVersion.PluginData.Created)
                pluginVersion.Load();

            this.UpdatePropertyGrid();
        }

        private void Publish()
        {
            List<PluginVersion> versions = new List<PluginVersion>();

            foreach (PluginVersionTreeNode node in this.SelectedNodes)
                versions.Add(node.PluginVersion as PluginVersion);

            PublishForm form = new PublishForm(versions);
            DialogResult dr = form.ShowDialog();

            if (dr == DialogResult.OK)
                this.Reload();
        }

        private void Reload()
        {
            this.SelectedNodes.Clear();
            this.tvwPlugins.Nodes.Clear();
            this.LoadPluginTreeView();
        }

        private void UpdatePropertyGrid()
        {
            if ((this.tvwPlugins.SelectedNode as PluginTreeNode).LastVersion != null)
                this.pgProperties.SelectedObject = (this.tvwPlugins.SelectedNode as PluginTreeNode).LastVersion.PluginVersion;
            else
                this.pgProperties.SelectedObject = null;
        }

        #endregion

        #region Public Methods

        public void Translate()
        {
            this.tsbCreateBetas.Text = TranslationManager.GetManager().Translate("Create Selected Versions Betas");
            this.tsbCreateTestBetas.Text = TranslationManager.GetManager().Translate("Create Selected Test Betas");
            this.tsbCreateCustomBeta.Text = TranslationManager.GetManager().Translate("Create Custom Beta");

            this.tsbBetas.Text = TranslationManager.GetManager().Translate("Betas");

            this.tsbPublish.Text = TranslationManager.GetManager().Translate("Publish");

            this.tsbInstall.Text = TranslationManager.GetManager().Translate("Install");

            this.tsbOptions.Text = TranslationManager.GetManager().Translate("Options");

            this.Refresh();
        }

        #endregion

        #region Signed Event Methods

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            this.LoadPlugins();
            this.LoadPluginTreeView();
        }

        private void tsbCreateBetas_Click(object sender, System.EventArgs e)
        {
            if (this.SelectedNodes.Count == 0)
                MessageBox.Show(
                    TranslationManager.GetManager().Translate("MainForm.Mbox_Caption.AtLeastOneVersion"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            else
                this.CreateBetas();
        }

        private void tsbCreateTestBetas_Click(object sender, System.EventArgs e)
        {
            if (this.SelectedNodes.Count == 0)
                MessageBox.Show(
                    TranslationManager.GetManager().Translate("MainForm.Mbox_Caption.AtLeastOneVersion"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            else
                this.CreateTestBetas();
        }

        private void tsbCreateCustomBeta_Click(object sender, System.EventArgs e)
        {
            BetaData customBeta = new BetaData();

            using (BetaDataEditorForm form = new BetaDataEditorForm(customBeta, false))
                form.ShowDialog(this);
        }

        private void tsmPublish_Click(object sender, System.EventArgs e)
        {
            if (this.SelectedNodes.Count == 0)
                MessageBox.Show(
                    TranslationManager.GetManager().Translate("MainForm.Mbox_Caption.AtLeastOneVersion"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            else
                this.Publish();
        }

        private void tsbOptions_Click(object sender, System.EventArgs e)
        {
            OptionsManager.GetManager().Form.ShowDialog();
        }

        private void tvwPlugins_AfterCheck(object sender, TreeViewEventArgs e)
        {
            PluginTreeNode node = e.Node as PluginTreeNode;

            PluginVersionTreeNode selectedNode = node.LastVersion as PluginVersionTreeNode;

            if (node.Checked && !this.SelectedNodes.Contains(selectedNode))
                this.SelectedNodes.Add(selectedNode);
            else if (!node.Checked)
                this.SelectedNodes.Remove(selectedNode);
        }

        private void tvwPlugins_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.UpdatePropertyGrid();
        }

        private void tvwPlugins_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            this.tvwPlugins.SelectedNode = e.Node;

            this.Cursor = Cursors.WaitCursor;

            PluginTreeNode pluginNode = this.tvwPlugins.SelectedNode as PluginTreeNode;

            if (pluginNode != null)
                this.SelectPluginTreeNode();

            this.Cursor = Cursors.Default;
        }

        #endregion
    }
}