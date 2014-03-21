using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd.TreeView
{
    public class PluginTreeNode : TreeNode
    {
        #region Attributes and Properties

        private Plugin _plugin;
        public Plugin Plugin
        {
            get { return this._plugin; }
            set { this._plugin = value; }
        }

        private PluginVersionTreeNode _lastVersion;
        public PluginVersionTreeNode LastVersion
        {
            get { return this._lastVersion; }
            set { this._lastVersion = value; }
        }

        #endregion

        #region Constructor

        public PluginTreeNode(Plugin plugin)
        {
            this._plugin = plugin;
            this.Tag = plugin;
            this.ToolTipText = this.ToString();
            this.Text = this.ToString();
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Plugin.ToString();
        }

        #endregion
    }
}
