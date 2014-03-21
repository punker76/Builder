using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd.TreeView
{
    public class ProductTreeNode : TreeNode
    {
        #region Attributes and Properties

        private Product _dependencyProduct;
        public Product Product
        {
            get { return _dependencyProduct; }
            set { _dependencyProduct = value; }
        }

        #endregion

        #region Constructor

        public ProductTreeNode(Product product)
        {
            this._dependencyProduct = product;
            this.Tag = product;
            this.ToolTipText = this.ToString();
            this.Text = this.ToString();
            this.Name = product.Name;
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
