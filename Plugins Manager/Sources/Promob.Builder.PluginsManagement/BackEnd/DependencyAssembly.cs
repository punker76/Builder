
using System.ComponentModel;
using System.Xml;
namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class DependencyAssembly : INotifyPropertyChanged
    {
        #region Attributes and Properties

        public string Filename { get; set; }
        public string Local { get; set; }

        #endregion

        #region Constructors

        public DependencyAssembly(string filename, string local)
        {
            this.Filename = filename;
            this.Local = local;
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Filename;
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
            XmlElement xmlElement = xmlDocument.CreateElement("DependancyAssembly");

            XmlAttribute filename = xmlDocument.CreateAttribute("Filename");
            XmlAttribute local = xmlDocument.CreateAttribute("Local");

            filename.Value = this.Filename;
            local.Value = this.Local;

            xmlElement.Attributes.Append(filename);
            xmlElement.Attributes.Append(local);

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static DependencyAssembly Load(XmlElement xmlElement)
        {
            if (!xmlElement.Name.Equals("DependancyAssembly"))
                return null;

            XmlAttribute filename = xmlElement.Attributes["Filename"];
            XmlAttribute local = xmlElement.Attributes["Local"];

            DependencyAssembly dependencyAssembly =
                new DependencyAssembly(filename.Value, local.Value);

            return dependencyAssembly;
        }

        #endregion
    }
}
