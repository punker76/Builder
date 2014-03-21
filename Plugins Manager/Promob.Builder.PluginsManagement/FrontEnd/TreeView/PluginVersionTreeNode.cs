using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd.TreeView
{
    public class PluginVersionTreeNode : TreeNode
    {
        #region Attributes and Properties

        private PluginVersion _pluginVersion;
        public PluginVersion PluginVersion
        {
            get { return _pluginVersion; }
            set { _pluginVersion = value; }
        }

        #endregion

        #region Constructor

        public PluginVersionTreeNode(PluginVersion version)
        {
            this._pluginVersion = version;
            this.Tag = version;
            this.Text = this.ToString();
            this.Name = version.VersionNumber;
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.PluginVersion.ToString();
        }

        #endregion
    }
}
