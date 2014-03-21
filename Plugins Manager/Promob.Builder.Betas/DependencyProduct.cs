using System.ComponentModel;
using System.Xml;

namespace Promob.Builder.Betas
{
    public class DependencyProduct : INotifyPropertyChanged
    {
        #region Attributes and Properties

        private string _name;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        private string _version;
        public string Version
        {
            get { return this._version; }
            set { this._version = value; }
        }

        private int _order;
        public int Order
        {
            get { return this._order; }
            set { this._order = value; }
        }

        #endregion

        #region Constructors

        public DependencyProduct(string name, string version)
        {
            this._name = name;
            this._version = version;
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return string.Format("{0} - {1}", this._name, this._version);
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

            name.Value = this._name;
            version.Value = this._version;

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
