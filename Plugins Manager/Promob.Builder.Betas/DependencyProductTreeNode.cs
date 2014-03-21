using System.Windows.Forms;

namespace Promob.Builder.Betas
{
    public class DependencyProductTreeNode : TreeNode
    {
        #region Attributes and Properties

        private DependencyProduct _dependencyProduct;
        public DependencyProduct Product
        {
            get { return _dependencyProduct; }
            set { _dependencyProduct = value; }
        }

        #endregion

        #region Constructor

        public DependencyProductTreeNode(DependencyProduct dependencyProduct)
        {
            this._dependencyProduct = dependencyProduct;
            this.Tag = dependencyProduct;
            this.ToolTipText = this.ToString();
            this.Text = this.ToString();
            this.Name = dependencyProduct.ToString();
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Product.ToString();
        }

        #endregion
    }
}
