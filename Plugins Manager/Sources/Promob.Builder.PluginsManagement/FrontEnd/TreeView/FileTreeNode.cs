using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd.TreeView
{
    public class FileTreeNode : TreeNode
    {
        #region Attributes and Properties

        private File _file;
        public File File
        {
            get { return _file; }
            set { _file = value; }
        }

        #endregion

        #region Constructor

        public FileTreeNode(File file)
        {
            this._file = file;
            this.Tag = file;
            this.ToolTipText = this.ToString();
            this.Text = this.ToString();
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.File.ToString();
        }

        #endregion
    }
}
