using System.Windows.Forms;
using Promob.Builder.Core;

namespace Promob.Builder.Forms
{
    public class VersionTreeNode : TreeNode
    {
        #region Attributes and Properties

        private Version _version;
        public Version Version
        {
            get { return _version; }
            set { _version = value; }
        }

        #endregion

        #region Constructor

        public VersionTreeNode(Version version)
        {
            this._version = version;
            this.Tag = version;
            this.Text = this.ToString();
            this.Name = version.VersionNumber;
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Version.ToString();
        }

        #endregion
    }
}
