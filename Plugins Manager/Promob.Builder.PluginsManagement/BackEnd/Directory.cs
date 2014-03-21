
using System;
using System.ComponentModel;
using System.Xml;
namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class Directory : IContentObject, INotifyPropertyChanged
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

        public Directory(string name)
        {
            this.Name = name;
        }

        public Directory(string name, string directory, string url)
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

        #region Private Methods

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Name;
        }

        #endregion

        #region Public Methods

        public XmlElement Save(XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.CreateElement("Directory");

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

        public static Directory Load(XmlElement xmlElement)
        {
            if (!xmlElement.Name.Equals("Directory"))
                return null;

            XmlAttribute name = xmlElement.Attributes["Name"];
            XmlAttribute local = xmlElement.Attributes["Local"];
            XmlAttribute destiny = xmlElement.Attributes["Destiny"];
            XmlAttribute shared = xmlElement.Attributes["Shared"];

            Directory directory = new Directory(name.Value);

            directory.Local = local.Value;
            directory.Destiny = destiny.Value;

            bool convertedShared = false;

            if (Boolean.TryParse(shared.Value, out convertedShared))
                directory.Shared = convertedShared;

            return directory;
        }

        #endregion
    }
}
