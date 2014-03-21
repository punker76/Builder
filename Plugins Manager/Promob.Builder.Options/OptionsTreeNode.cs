using System.Windows.Forms;

namespace Promob.Builder.Options
{
    public class OptionsTreeNode : TreeNode
    {
        #region Attributes and Properties

        private AbstractOptions _options;
        public AbstractOptions Options
        {
            get { return _options; }
            set { _options = value; }
        }

        #endregion

        #region Constructor

        public OptionsTreeNode(AbstractOptions options)
        {
            this._options = options;
            this.Tag = options;
            this.ToolTipText = this.ToString();
            this.Text = this.ToString();
            this.Name = options.GetType().Name;
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Options.ToString();
        }

        #endregion
    }
}
