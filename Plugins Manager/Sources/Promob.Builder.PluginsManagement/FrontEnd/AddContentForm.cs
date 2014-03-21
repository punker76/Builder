using System.Collections.Generic;
using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;
using Promob.Builder.PluginsManagement.FrontEnd.TreeView;
using Promob.Builder.SVN;
using Promob.Builder.Translation;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class AddContentForm : Form
    {
        #region Attributes and Properties

        private PluginData _pluginData;
        public PluginData PluginData
        {
            get { return _pluginData; }
            set { _pluginData = value; }
        }

        private ImageList _contentImageList;
        public ImageList ContentImageList
        {
            get
            {
                if (this._contentImageList == null)
                {
                    this._contentImageList = new ImageList();
                    this.ContentImageList.ColorDepth = ColorDepth.Depth32Bit;
                    this.ContentImageList.ImageSize = new System.Drawing.Size(16, 16);

                    this._contentImageList.Images.Add(Properties.Resources._16x16_folder);
                    this._contentImageList.Images.Add(Properties.Resources._16x16_empty_file);
                }

                return this._contentImageList;
            }
        }

        private bool _checkingNode;
        private bool _changed;
        #endregion

        #region Constructors

        public AddContentForm(PluginData pluginData)
        {
            this._pluginData = pluginData;
            this.InitializeComponent();
            this.Init();
        }

        #endregion

        #region Events Methods

        private void AddContentForm_Load(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.LoadContentTreeView();
            this.Cursor = Cursors.Default;
        }

        private void AddContentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this._changed)
                return;

            this.PluginData.Content.Clear();

            foreach (TreeNode node in this.tvContent.Nodes)
                this.FillContent(node, this.PluginData.Content);
        }

        private void tvContent_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.tvContent.AfterCheck -= tvContent_AfterCheck;
            this.tvContent.BeforeCheck -= tvContent_BeforeCheck;

            if (e.Node is DirectoryTreeNode)
                if (e.Node.Nodes.Count == 0)
                    this.LoadContent(e.Node as DirectoryTreeNode);

            this.tvContent.AfterCheck += tvContent_AfterCheck;
            this.tvContent.BeforeCheck += tvContent_BeforeCheck;
            this.Cursor = Cursors.Default;
        }

        private void tvContent_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            this.tvContent.SelectedNode = e.Node;
        }

        private void tvContent_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node is DirectoryTreeNode && !this._checkingNode)
            {
                DialogResult dr =
                    MessageBox.Show(
                        TranslationManager.GetManager().Translate("AddContentForm.Mbox_Caption.CheckUncheck"),
                        TranslationManager.GetManager().Translate("Atention"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation);

                if (dr == DialogResult.Yes)
                {
                    this._checkingNode = true;
                    this.tvContent.BeforeCheck -= tvContent_BeforeCheck;

                    foreach (TreeNode node in e.Node.Nodes)
                        node.Checked = e.Node.Checked;

                    this.tvContent.BeforeCheck += tvContent_BeforeCheck;
                    this._checkingNode = false;
                }

                this._changed = true;
            }
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            this.Text = string.Format("Add Content ({0} - {1})", this.PluginData.Parent.Product.Name, this.PluginData.Parent.VersionNumber);
            this.Load += this.AddContentForm_Load;
            this.FormClosing += this.AddContentForm_FormClosing;
        }

        private void LoadContentTreeView()
        {
            this.tvContent.ImageList = this.ContentImageList;

            this.LoadDirectories();

            this.tvContent.AfterSelect += this.tvContent_AfterSelect;
            this.tvContent.BeforeCheck += this.tvContent_BeforeCheck;
            this.tvContent.AfterCheck += this.tvContent_AfterCheck;
        }

        private void LoadDirectories()
        {
            this.Cursor = Cursors.WaitCursor;

            Dictionary<string, string> directories = SVNManager.Instance.List(SVNManager.DEV_URL, false);

            foreach (string name in directories.Keys)
            {
                if (name.Equals("Sources") ||
                    name.Equals("Version") ||
                    name.Equals("Plugins"))
                    continue;

                string url = string.Format("{0}/{1}/{2}/{3}", SVNManager.DEV_URL, name, this.PluginData.Parent.Product.Name, this.PluginData.Parent.VersionNumber);

                Info info = SVNManager.Instance.Info(url);

                if (info == null)
                    continue;

                Directory directory = this.PluginData.Content[name] as Directory;

                if (directory == null)
                    directory = new Directory(name, string.Empty, url);

                directory.Url = url;

                DirectoryTreeNode node = new DirectoryTreeNode(directory);
                node.SelectedImageIndex = 0;
                node.ImageIndex = 0;
                if (name.Equals("References"))
                {
                    node.Text = "Bin";
                    node.Name = "Bin";
                }

                this.tvContent.Nodes.Add(node);
            }

            foreach (DirectoryTreeNode node in this.tvContent.Nodes)
                LoadContent(node);

            this.Cursor = Cursors.Default;
        }

        private void LoadContent(DirectoryTreeNode directoryNode)
        {
            Dictionary<string, string> list = SVNManager.Instance.List(directoryNode.Directory.Url);

            foreach (string name in list.Keys)
            {
                string url = string.Format("{0}/{1}", directoryNode.Directory.Url, name);

                Info info = SVNManager.Instance.Info(url);

                if (info == null)
                    continue;

                if (info.Entry.IsDirectory)
                {
                    Directory directory = this.PluginData.Content[name] as Directory;

                    bool contains = directory != null;

                    if (!contains)
                        directory = new Directory(name, directoryNode.FullPath, url);

                    directory.Url = url;

                    DirectoryTreeNode node = new DirectoryTreeNode(directory);
                    node.SelectedImageIndex = 0;
                    node.ImageIndex = 0;
                    node.Checked = contains;

                    directoryNode.Nodes.Add(node);
                }
                else
                {
                    File file = this.PluginData.Content[name] as File;

                    bool contains = file != null;

                    if (!contains)
                        file = new File(name, directoryNode.FullPath, url);

                    FileTreeNode node = new FileTreeNode(file);
                    node.SelectedImageIndex = 1;
                    node.ImageIndex = 1;
                    node.Checked = contains;

                    directoryNode.Nodes.Add(node);
                }
            }

            foreach (TreeNode node in directoryNode.Nodes)
                if (node is DirectoryTreeNode)
                    LoadContent(node as DirectoryTreeNode);

            directoryNode.Expand();
        }

        private void FillContent(TreeNode node, Content content)
        {
            if (node is DirectoryTreeNode)
            {
                Directory directory = (node as DirectoryTreeNode).Directory;

                if (node.Checked && !content.Contains(directory))
                    content.Add(directory);
            }
            else if (node is FileTreeNode)
            {
                File file = (node as FileTreeNode).File;

                if (node.Checked && !content.Contains(file))
                    content.Add(file);
            }

            foreach (TreeNode child in node.Nodes)
                this.FillContent(child, content);
        }

        #endregion
    }
}
