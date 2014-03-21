using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd.TreeView
{
    public class DependencyAssemblyTreeNode : TreeNode
    {
        #region Attributes and Properties

        private DependencyAssembly _dependencyAssembly;
        public DependencyAssembly DependencyAssembly
        {
            get { return _dependencyAssembly; }
            set { _dependencyAssembly = value; }
        }

        #endregion

        #region Constructor

        public DependencyAssemblyTreeNode(DependencyAssembly dependencyAssembly)
        {
            this._dependencyAssembly = dependencyAssembly;
            this.Tag = dependencyAssembly;
            this.ToolTipText = this.ToString();
            this.Text = this.ToString();
            this.Name = dependencyAssembly.ToString();
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.DependencyAssembly.ToString();
        }

        #endregion
    }
}
