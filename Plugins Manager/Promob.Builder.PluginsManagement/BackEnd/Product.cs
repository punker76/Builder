
namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class Product
    {
        #region Attributes and Properties

        public string Name { get; set; }

        #endregion

        #region Constructors

        public Product(string name)
        {
            this.Name = name;
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Name;
        }

        #endregion

    }
}
