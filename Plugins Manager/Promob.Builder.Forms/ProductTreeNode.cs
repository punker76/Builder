using System.Windows.Forms;
using Promob.Builder.Core;

namespace Promob.Builder.Forms
{
    public class ProductTreeNode : TreeNode
    {
        #region Attributes and Properties

        private Product _product;
        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        #endregion

        #region Constructor

        public ProductTreeNode(Product product)
        {
            this._product = product;
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
