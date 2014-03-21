
using System;
using System.ComponentModel;
using System.Xml;
namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class File : IContentObject, INotifyPropertyChanged
    {
        #region Attributes and Properties

        public string Name { get; set; }
        public string Local { get; set; }
        public string Destiny { get; set; }
        public bool Shared { get; set; }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        #endregion

        #region Constructors

        public File(string name)
        {
            this.Name = name;
        }

        public File(string name, string directory, string url)
        {
            this.Name = name;
            this._url = url;

            if (string.IsNullOrEmpty(directory))
            {
                this.Local = "Program";
                this.Destiny = "%ProgramFolder%";
            }
            else
            {
                this.Local = string.Format(@"Program\{0}", directory);
                this.Destiny = string.Format(@"%ProgramFolder%\{0}", directory);
            }
        }


        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Name;
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
            XmlElement xmlElement = xmlDocument.CreateElement("File");

            XmlAttribute name = xmlDocument.CreateAttribute("Name");
            XmlAttribute local = xmlDocument.CreateAttribute("Local");
            XmlAttribute destiny = xmlDocument.CreateAttribute("Destiny");
            XmlAttribute shared = xmlDocument.CreateAttribute("Shared");

            name.Value = this.Name;
            local.Value = this.Local;
            destiny.Value = this.Destiny;
            shared.Value = this.Shared.ToString();

            xmlElement.Attributes.Append(name);
            xmlElement.Attributes.Append(local);
            xmlElement.Attributes.Append(destiny);
            xmlElement.Attributes.Append(shared);

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static File Load(XmlElement xmlElement)
        {
            if (!xmlElement.Name.Equals("File"))
                return null;

            XmlAttribute name = xmlElement.Attributes["Name"];
            XmlAttribute local = xmlElement.Attributes["Local"];
            XmlAttribute destiny = xmlElement.Attributes["Destiny"];
            XmlAttribute shared = xmlElement.Attributes["Shared"];

            File file = new File(name.Value);

            file.Local = local.Value;
            file.Destiny = destiny.Value;

            bool convertedShared = false;

            if (Boolean.TryParse(shared.Value, out convertedShared))
                file.Shared = convertedShared;

            return file;
        }

        #endregion
    }
}
