
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class DependencyAssemblies : BindingList<DependencyAssembly>
    {
        #region Indexers

        public DependencyAssembly this[string filename]
        {
            get
            {
                return this.ToList().Find(new System.Predicate<DependencyAssembly>(c => c.Filename.Equals(filename)));
            }
        }

        #endregion

        #region Public Methods

        public bool Contains(string filename)
        {
            return this.ToList().Find(new System.Predicate<DependencyAssembly>(c => c.Filename.Equals(filename))) != null;
        }

        public XmlElement Save(XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.CreateElement("DependancyAssemblies");

            foreach (DependencyAssembly dependencyAssembly in this)
                xmlElement.AppendChild(dependencyAssembly.Save(xmlDocument));

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static DependencyAssemblies Load(XmlElement xmlElement)
        {
            DependencyAssemblies dependencyAssemblies = new DependencyAssemblies();

            foreach (XmlElement child in xmlElement.ChildNodes)
            {
                if (child.Name.Equals("DependancyAssembly"))
                    dependencyAssemblies.Add(DependencyAssembly.Load(child));
            }

            return dependencyAssemblies;
        }

        #endregion
    }
}
