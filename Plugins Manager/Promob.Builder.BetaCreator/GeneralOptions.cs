using System;
using System.ComponentModel;
using System.Xml;
using Promob.Builder.Options;

namespace Promob.Builder.BetaCreator
{
    public class GeneralOptions : AbstractOptions, INotifyPropertyChanged
    {
        #region Constructors

        public GeneralOptions(string path)
            : base(path)
        {
        }

        #endregion

        #region Attributes and Properties

        private int _language;
        public int Language
        {
            get { return this._language; }
            set
            {
                this._language = value;
                this.NotifyPropertyChanged("Language");
            }
        }

        public override OptionsContainer OptionsContainer
        {
            get
            {
                return new GeneralOptionsContainer(this);
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Overriden Methods

        public override void Load()
        {
            if (!System.IO.File.Exists(this.Path))
                return;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(this.Path);

            if (!xmlDocument.DocumentElement.Name.Equals("GeneralOptions"))
                return;

            XmlElement documentElement = xmlDocument.DocumentElement;

            foreach (XmlElement child in documentElement.ChildNodes)
                if (child.Name.Equals("Language"))
                    this.Language = Convert.ToInt32(child.InnerText);
        }

        public override void Save()
        {
            string dirPath = System.IO.Path.GetDirectoryName(this.Path);

            if (!System.IO.Directory.Exists(dirPath))
                System.IO.Directory.CreateDirectory(dirPath);
            else if (System.IO.File.Exists(this.Path))
                System.IO.File.Delete(this.Path);

            XmlDocument xmlDocument = new XmlDocument();
            XmlElement documentElement = xmlDocument.CreateElement("GeneralOptions");

            XmlElement language = xmlDocument.CreateElement("Language");
            language.InnerText = this.Language.ToString();
            documentElement.AppendChild(language);

            xmlDocument.AppendChild(documentElement);

            xmlDocument.Save(this.Path);
        }

        public override string ToString()
        {
            return "General";
        }

        #endregion

        #region Private Methods

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
