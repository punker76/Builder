
using System.ComponentModel;
using System.Xml;
namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class DependencyProduct : INotifyPropertyChanged
    {
        #region Attributes and Properties

        public string Name { get; set; }
        public string Version { get; set; }
        public int Order { get; set; }

        #endregion

        #region Constructors

        public DependencyProduct(string name, string version)
        {
            this.Name = name;
            this.Version = version;
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Name, this.Version);
        }

        #endregion

        #region Private Methods

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Public Methods

        public XmlElement Save(XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.CreateElement("DependencyProduct");

            XmlAttribute name = xmlDocument.CreateAttribute("Name");
            XmlAttribute version = xmlDocument.CreateAttribute("Version");

            name.Value = this.Name;
            version.Value = this.Version;

            xmlElement.Attributes.Append(name);
            xmlElement.Attributes.Append(version);

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static DependencyProduct Load(XmlElement xmlElement)
        {
            if (!xmlElement.Name.Equals("DependencyProduct"))
                return null;

            XmlAttribute name = xmlElement.Attributes["Name"];
            XmlAttribute version = xmlElement.Attributes["Version"];

            DependencyProduct dependencyProduct = new DependencyProduct(name.Value, version.Value);

            return dependencyProduct;
        }

        #endregion
    }
}
