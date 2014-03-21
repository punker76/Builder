using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;
using Promob.Builder.PluginsManagement.FrontEnd.TreeView;
using Promob.Builder.SVN;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class VersionManagement : Form
    {
        #region Constructors

        public VersionManagement(ProductData product, bool isEditor)
        {
            this.ProductData = product;
            this._isEditor = isEditor;

            this.InitializeComponent();

            if (this._isEditor)
                this.ProductData.UpdateCheckout(this.ProductData.Path, this.ProductData.Url);
        }

        #endregion

        #region Attributes and Properties

        private ProductData _productData;
        public ProductData ProductData
        {
            get { return this._productData; }
            set { this._productData = value; }
        }

        private bool _isEditor = false;

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

        #endregion

        #region Private Methods

        private void DeleteVersion()
        {
            this.Cursor = Cursors.WaitCursor;

            string pluginName = (this.tvwPlugins.SelectedNode as PluginVersionTreeNode).Parent.Text;
            string pluginVersion = (this.tvwPlugins.SelectedNode as PluginVersionTreeNode).PluginVersion.ToString();

            Dictionary<string, string> fordersUrls = SVNManager.Instance.List(SVNManager.DEV_URL);
            List<string> versionUrlList = new List<string>();
            string foldersToRemove = string.Empty;

            foreach (KeyValuePair<string, string> forder in fordersUrls)
            {
                Dictionary<string, string> urls = SVNManager.Instance.List(string.Format("{0}/{1}/{2}", SVNManager.DEV_URL, forder.Key, pluginName));

                if (urls != null)
                    foreach (KeyValuePair<string, string> url in urls)
                        if (url.Key.Equals(pluginVersion))
                        {
                            versionUrlList.Add(url.Value);
                            foldersToRemove += url.Value + "\n";
                        }
            }

            DialogResult dialogResult =
                MessageBox.Show("Are you sure that you want to delete the following folders?\n\n" + foldersToRemove,
                                "Attention",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Exclamation);

            if (dialogResult == DialogResult.Cancel)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            foreach (string versionUrl in versionUrlList)
                SVNManager.Instance.Delete(versionUrl);

            this.LoadPlugins();
            this.LoadPluginTreeView();

            this.ManagerControls();

            this.Cursor = Cursors.Default;
        }

        private void LoadPlugins()
        {
            Dictionary<string, string> urls = SVNManager.Instance.List(SVNManager.VERSIONS_URL);

            if (urls == null)
                return;

            this.Plugins.Clear();

            foreach (string name in urls.Keys)
                this.Plugins.Add(new Plugin(name));
        }

        private void LoadPluginTreeView()
        {
            this.tvwPlugins.Nodes.Clear();

            foreach (Plugin plugin in this._plugins)
            {
                PluginTreeNode node = new PluginTreeNode(plugin);
                this.tvwPlugins.Nodes.Add(node);
            }
        }

        private void LoadSelectPluginTreeNode()
        {
            PluginTreeNode node = this.tvwPlugins.SelectedNode as PluginTreeNode;
            Plugin plugin = node.Plugin;

            if (plugin.Versions.Count == 0)
                plugin.LoadAll();

            if (node.Nodes.Count == 0)
                foreach (PluginVersion version in node.Plugin.Versions)
                {
                    PluginVersionTreeNode versionNode = new PluginVersionTreeNode(version);
                    node.Nodes.Add(versionNode);
                }
        }

        private void ManagerControls()
        {
            if (this.tvwPlugins.SelectedNode == null || this.tvwPlugins.SelectedNode.Parent == null)
                this.tsbDeleteVersion.Enabled = false;
            else
                this.tsbDeleteVersion.Enabled = true;
        }

        #endregion

        #region Signed Event Methods

        private void cmiDeleteVersion_Click(object sender, EventArgs e)
        {
            this.DeleteVersion();
        }

        private void tsbDeleteVersion_Click(object sender, EventArgs e)
        {
            this.DeleteVersion();
        }

        private void tvwPlugins_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ManagerControls();

            this.Cursor = Cursors.WaitCursor;

            this.tvwPlugins.SelectedNode = e.Node;

            PluginVersionTreeNode pluginVersionNode = this.tvwPlugins.SelectedNode as PluginVersionTreeNode;
            PluginTreeNode pluginNode = this.tvwPlugins.SelectedNode as PluginTreeNode;

            if (pluginNode != null)
                this.LoadSelectPluginTreeNode();

            this.Cursor = Cursors.Default;
        }

        private void tvwPlugins_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.tvwPlugins.SelectedNode == null)
                return;

            if (this.tvwPlugins.SelectedNode.Parent == null)
                return;

            if (e.Button == MouseButtons.Right && this.tvwPlugins.SelectedNode.FirstNode == null)
            {
                this.tvwPlugins.SelectedNode = this.tvwPlugins.GetNodeAt(e.X, e.Y);

                if (this.tvwPlugins.SelectedNode != null)
                    this.cmsVersion.Show(this.tvwPlugins, e.Location);
            }
        }

        private void VersionManagement_Load(object sender, EventArgs e)
        {
            this.LoadPlugins();
            this.LoadPluginTreeView();
            this.tvwPlugins.CollapseAll();
        }

        #endregion
    }
}
