using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace Promob.Builder.Betas
{
    public class DependencyProducts : BindingList<DependencyProduct>
    {
        #region Public Methods

        public new void Add(DependencyProduct item)
        {
            int i = 0;

            foreach (DependencyProduct product in this)
                product.Order = i++;

            item.Order = i;

            base.Add(item);
        }

        public new void Remove(DependencyProduct item)
        {
            base.Remove(item);

            int i = 0;

            foreach (DependencyProduct product in this)
                product.Order = i++;
        }

        public bool Contains(string name)
        {
            return this.ToList().Find(new System.Predicate<DependencyProduct>(p => p.Name.Equals(name))) != null;
        }

        public bool Contains(string name, string version)
        {
            return this.ToList().Find(new System.Predicate<DependencyProduct>(p => p.Name.Equals(name) && p.Version.Equals(version))) != null;
        }

        #endregion

        #region Public Methods

        public XmlElement Save(XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.CreateElement("DependencyProducts");

            foreach (DependencyProduct dependencyProduct in this)
                xmlElement.AppendChild(dependencyProduct.Save(xmlDocument));

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static DependencyProducts Load(XmlElement xmlElement)
        {
            DependencyProducts dependencyProducts = new DependencyProducts();

            foreach (XmlElement child in xmlElement.ChildNodes)
            {
                if (child.Name.Equals("DependencyProduct"))
                    dependencyProducts.Add(DependencyProduct.Load(child));
            }

            return dependencyProducts;
        }

        #endregion
    }
}
