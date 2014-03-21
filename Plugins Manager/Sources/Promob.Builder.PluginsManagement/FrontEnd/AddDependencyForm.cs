using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;
using Promob.Builder.PluginsManagement.FrontEnd.TreeView;
using Promob.Builder.SVN;
using Promob.Builder.Translation;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class AddDependencyForm : Form
    {
        #region Attributes and Properties

        private PluginData _pluginData;
        public PluginData PluginData
        {
            get { return _pluginData; }
            set { _pluginData = value; }
        }

        private ImageList _dependencyImageList;
        public ImageList DependencyImageList
        {
            get
            {
                if (this._dependencyImageList == null)
                {
                    this._dependencyImageList = new ImageList();
                    this.DependencyImageList.ColorDepth = ColorDepth.Depth32Bit;
                    this.DependencyImageList.ImageSize = new System.Drawing.Size(16, 16);

                    this._dependencyImageList.Images.Add(Properties.Resources._16x16_folder);
                    this._dependencyImageList.Images.Add(Properties.Resources._16x16_empty_file);
                }

                return this._dependencyImageList;
            }
        }

        private bool _checkingNode;

        #endregion

        #region Constructors

        public AddDependencyForm(PluginData pluginData)
        {
            this._pluginData = pluginData;
            this.InitializeComponent();
            this.Init();
        }

        #endregion

        #region Events Methods

        private void AddDependencyForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.LoadDependenciesTreeView();
            this.Cursor = Cursors.Default;
        }

        private void tvDependencies_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.tvDependencies.AfterCheck -= tvDependencies_AfterCheck;
            this.tvDependencies.BeforeCheck -= tvDependencies_BeforeCheck;

            if (e.Node is DirectoryTreeNode)
                if (e.Node.Nodes.Count == 0)
                    this.LoadDependencies(e.Node as DirectoryTreeNode);

            this.tvDependencies.AfterCheck += tvDependencies_AfterCheck;
            this.tvDependencies.BeforeCheck += tvDependencies_BeforeCheck;
            this.Cursor = Cursors.Default;
        }

        private void tvDependencies_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            this.tvDependencies.SelectedNode = e.Node;
        }

        private void tvDependencies_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node is DirectoryTreeNode && !this._checkingNode)
            {
                DialogResult dr =
                    MessageBox.Show(
                        TranslationManager.GetManager().Translate("AddDependencyForm.Mbox_Caption.CheckUncheck"),
                        TranslationManager.GetManager().Translate("Atention"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation);

                if (dr == DialogResult.Yes)
                {
                    this._checkingNode = true;

                    this.tvDependencies.BeforeCheck -= tvDependencies_BeforeCheck;

                    foreach (TreeNode node in e.Node.Nodes)
                        node.Checked = e.Node.Checked;

                    this._checkingNode = false;

                    this.tvDependencies.BeforeCheck += tvDependencies_BeforeCheck;
                }
            }

            if (e.Node is DependencyAssemblyTreeNode)
            {
                DependencyAssembly dependencyAssembly = (e.Node as DependencyAssemblyTreeNode).DependencyAssembly;

                if (e.Node.Checked && !this.PluginData.DependencyAssemblies.Contains(dependencyAssembly))
                    this.PluginData.DependencyAssemblies.Add(dependencyAssembly);
                else if (!e.Node.Checked && this.PluginData.DependencyAssemblies.Contains(dependencyAssembly))
                    this.PluginData.DependencyAssemblies.Remove(dependencyAssembly);
            }
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            this.Text = string.Format("Add Dependencies ({0} - {1})", this.PluginData.Parent.Product.Name, this.PluginData.Parent.VersionNumber);
            this.Load += AddDependencyForm_Load;
        }

        private void LoadDependenciesTreeView()
        {
            this.tvDependencies.ImageList = this.DependencyImageList;

            this.LoadDirectories();

            this.tvDependencies.AfterSelect += this.tvDependencies_AfterSelect;
            this.tvDependencies.BeforeCheck += this.tvDependencies_BeforeCheck;
            this.tvDependencies.AfterCheck += this.tvDependencies_AfterCheck;
        }

        private void LoadDirectories()
        {
            this.Cursor = Cursors.WaitCursor;

            string url = string.Format("{0}/References", SVNManager.DEV_URL);

            Dictionary<string, string> directories = SVNManager.Instance.List(url, false);

            foreach (string name in directories.Keys)
            {
                Directory directory = new Directory(name);
                directory.Url = string.Format("{0}/{1}/Current", url, name);

                if (!Application.ReferencesCache.ContainsKey(directory.Url))
                {
                    Info info = SVNManager.Instance.Info(directory.Url);

                    if (info != null)
                    {
                        Application.ReferencesCache.Append(directory.Url, false, false);
                    }
                    else
                        continue;
                }

                DirectoryTreeNode node = new DirectoryTreeNode(directory);
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                this.tvDependencies.Nodes.Add(node);
            }

            this.Cursor = Cursors.Default;
        }

        private void LoadDependencies(DirectoryTreeNode directoryNode)
        {
            string url = directoryNode.Directory.Url;

            Info info = SVNManager.Instance.Info(url);

            List<string> dependencies = new List<string>();
            Dictionary<string, string> urls = SVNManager.Instance.List(url);

            if (urls == null)
                return;

            if (Application.ReferencesCache.ContainsKey(url) &&
                Application.ReferencesCache[url].LastRevision.Equals(info.Entry.Revision))
                this.LoadCachedDependencies(directoryNode, url, urls);
            else
            {
                if (!Application.ReferencesCache.ContainsKey(url))
                    Application.ReferencesCache.Append(url, false, true);

                if (Application.ReferencesCache.ContainsKey(url))
                {
                    Application.ReferencesCache[url].LastRevision = info.Entry.Revision;
                    Application.ReferencesCache.Save();
                }

                this.LoadNonCachedDependencies(directoryNode, url, urls);
            }

            directoryNode.Expand();
        }

        private void LoadCachedDependencies(DirectoryTreeNode directoryNode, string url, Dictionary<string, string> urls)
        {
            foreach (string name in urls.Keys)
            {
                string curUrl = string.Format("{0}/{1}", url, name);

                if (!Application.ReferencesCache.ContainsKey(urls[name]))
                {
                    this.LoadNonCachedDependencies(directoryNode, url, urls);
                    return;
                }

                CacheInfo cacheInfo = Application.ReferencesCache[urls[name]];

                if (cacheInfo == null)
                {
                    Info info = SVNManager.Instance.Info(curUrl);

                    this.LoadNonCachedDependency(directoryNode, info, name, curUrl);
                }
                else if (cacheInfo.IsDirectory)
                {
                    Directory directory = new Directory(name);

                    directory.Url = curUrl;

                    DirectoryTreeNode node = new DirectoryTreeNode(directory);
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    directoryNode.Nodes.Add(node);
                }
                else
                {
                    DependencyAssembly dependencyAssembly = this.PluginData.DependencyAssemblies[name] as DependencyAssembly;

                    bool contains = dependencyAssembly != null;

                    if (!contains)
                        dependencyAssembly = new DependencyAssembly(name, @"Program\Bin");

                    DependencyAssemblyTreeNode node = new DependencyAssemblyTreeNode(dependencyAssembly);
                    node.Checked = contains;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;

                    directoryNode.Nodes.Add(node);
                }
            }
        }

        private void LoadNonCachedDependencies(DirectoryTreeNode directoryNode, string url, Dictionary<string, string> urls)
        {
            foreach (string name in urls.Keys)
            {
                string curUrl = string.Format("{0}/{1}", url, name);

                if (!Application.ReferencesCache.ContainsKey(urls[name]))
                    Application.ReferencesCache.Append(urls[name], false, false);

                Info info = SVNManager.Instance.Info(curUrl);
                CacheInfo cacheInfo = Application.ReferencesCache[curUrl];

                string lastRevision = SVNManager.Instance.LastRevision(curUrl);

                if (!lastRevision.Equals(cacheInfo.LastRevision))
                {
                    Application.ReferencesCache[curUrl].LastRevision = info.Entry.Revision;
                    Application.ReferencesCache.Save();
                }

                this.LoadNonCachedDependency(directoryNode, info, name, curUrl);
            }
        }

        private void LoadNonCachedDependency(DirectoryTreeNode directoryNode, Info info, string name, string url)
        {
            if (info == null)
                return;

            if (info.Entry.IsDirectory)
            {
                Directory directory = this.PluginData.Content[name] as Directory;

                bool contains = directory != null;

                if (!contains)
                    directory = new Directory(name, directoryNode.FullPath, url);

                directory.Url = url;

                DirectoryTreeNode node = new DirectoryTreeNode(directory);
                node.Checked = contains;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                directoryNode.Nodes.Add(node);
            }
            else
            {
                DependencyAssembly dependencyAssembly = this.PluginData.DependencyAssemblies[name] as DependencyAssembly;

                bool contains = dependencyAssembly != null;

                if (!contains)
                    dependencyAssembly = new DependencyAssembly(name, @"Program\Bin");

                DependencyAssemblyTreeNode node = new DependencyAssemblyTreeNode(dependencyAssembly);
                node.Checked = contains;
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;

                directoryNode.Nodes.Add(node);
            }
        }

        #endregion
    }
}
