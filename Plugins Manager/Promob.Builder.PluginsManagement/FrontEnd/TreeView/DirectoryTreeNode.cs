using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd.TreeView
{
    public class DirectoryTreeNode : TreeNode
    {
        #region Attributes and Properties

        private Directory _directory;
        public Directory Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }

        #endregion

        #region Constructor

        public DirectoryTreeNode(Directory directory)
        {
            this._directory = directory;
            this.Tag = directory;
            this.ToolTipText = this.ToString();
            this.Text = this.ToString();
            this.Name = this.ToString();
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Directory.ToString();
        }

        #endregion
    }
}
