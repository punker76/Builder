
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class HostApplication : INotifyPropertyChanged
    {
        #region Static Members

        public static readonly List<string> ALLOWED_DISTRIBUTIONS =
            new List<string> { 
                "All",
                "Plus", 
                "Studio",
                "Lite",
                "Office",
                "Arch",
                "Catalog",
                "Express",
                "Premium",
                "SW",
                "Trial",
                "Worker",
                "Design Premium"
            };

        #endregion

        #region Attributes and Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                this._name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        private string _minVersion;
        public string MinVersion
        {
            get { return _minVersion; }
            set
            {
                _minVersion = value;
                this.NotifyPropertyChanged("MinVersion");
            }
        }

        private AllowedDistributions _allowedDistributions;
        public AllowedDistributions AllowedDistributions
        {
            get
            {
                if (this._allowedDistributions == null)
                    this._allowedDistributions = new AllowedDistributions();

                return _allowedDistributions;
            }
            set
            {
                _allowedDistributions = value;
                this.AllowedDistributionsDisplayString = _allowedDistributions.ToString();
                this.NotifyPropertyChanged("AllowedDistributions");
            }
        }

        private string _allowedDistributionsDisplayString;
        public string AllowedDistributionsDisplayString
        {
            get
            {
                if (string.IsNullOrEmpty(this._allowedDistributionsDisplayString))
                    this._allowedDistributionsDisplayString = this._allowedDistributions.ToString();

                return _allowedDistributionsDisplayString;
            }

            set
            {
                _allowedDistributionsDisplayString = value;
                this.NotifyPropertyChanged("AllowedDistributionsDisplayString");
            }
        }

        public int MajorVersion
        {
            get
            {
                string[] split = this.MinVersion.Split('.');

                if (split.Length != 3)
                    return 0;

                int conversion = 0;

                bool canConvert = Int32.TryParse(this.MinVersion.Split('.')[0], out conversion);

                if (!canConvert || this.MinVersion.Equals("Current"))
                    return 0;

                return conversion;
            }
        }

        public int MinorVersion
        {
            get
            {
                string[] split = this.MinVersion.Split('.');

                if (split.Length != 3)
                    return 0;

                int conversion = 0;

                bool canConvert = Int32.TryParse(this.MinVersion.Split('.')[1], out conversion);

                if (!canConvert || this.MinVersion.Equals("Current"))
                    return 0;

                return conversion;
            }
        }

        public int BuildNumber
        {
            get
            {
                string[] split = this.MinVersion.Split('.');

                if (split.Length != 3)
                    return 0;

                int conversion = 0;

                bool canConvert = Int32.TryParse(this.MinVersion.Split('.')[2], out conversion);

                if (!canConvert || this.MinVersion.Equals("Current"))
                    return 0;

                return conversion;
            }
        }

        #endregion

        #region Constructors

        public HostApplication()
        {
            this.AllowedDistributions.ListChanged += AllowedDistributions_ListChanged;
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

        #region Event Methods

        private void AllowedDistributions_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.AllowedDistributionsDisplayString = this._allowedDistributions.ToString();
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
            XmlElement xmlElement = xmlDocument.CreateElement("HostApplication");

            XmlAttribute name = xmlDocument.CreateAttribute("Name");
            XmlAttribute minVersion = xmlDocument.CreateAttribute("MinVersion");
            XmlAttribute allowedDistributions = xmlDocument.CreateAttribute("AllowedDistributions");

            name.Value = this.Name;
            minVersion.Value = this.MinVersion;

            allowedDistributions.Value = this.AllowedDistributions.ToString();

            xmlElement.Attributes.Append(name);
            xmlElement.Attributes.Append(minVersion);
            xmlElement.Attributes.Append(allowedDistributions);

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static HostApplication Load(XmlElement xmlElement)
        {
            if (!xmlElement.Name.Equals("HostApplication"))
                return null;

            XmlAttribute name = xmlElement.Attributes["Name"];
            XmlAttribute minVersion = xmlElement.Attributes["MinVersion"];
            XmlAttribute allowedDistribuions = xmlElement.Attributes["AllowedDistributions"];

            HostApplication hostApplication = new HostApplication();

            hostApplication.Name = name.Value;
            hostApplication.MinVersion = minVersion.Value;
            hostApplication.AllowedDistributions = AllowedDistributions.FromString(allowedDistribuions.Value);

            return hostApplication;
        }

        #endregion
    }
}
