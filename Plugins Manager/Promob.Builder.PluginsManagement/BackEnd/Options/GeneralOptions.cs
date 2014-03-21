using System;
using System.ComponentModel;
using System.Xml;
using Promob.Builder.Options;
using Promob.Builder.PluginsManagement.FrontEnd.Options;

namespace Promob.Builder.PluginsManagement.BackEnd.Options
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

        private string _productsManagerExecutablePath;
        public string ProductsManagerExecutablePath
        {
            get { return this._productsManagerExecutablePath; }
            set
            {
                this._productsManagerExecutablePath = value;
                this.NotifyPropertyChanged("ProductsManagerExecutablePath");
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
            {
                if (child.Name.Equals("Language"))
                    this.Language = Convert.ToInt32(child.InnerText);
                else if (child.Name.Equals("ProductsManagerExecutablePath"))
                    this.ProductsManagerExecutablePath = child.InnerText;
            }
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
            XmlElement executablePath = xmlDocument.CreateElement("ProductsManagerExecutablePath");

            language.InnerText = this.Language.ToString();
            executablePath.InnerText = this.ProductsManagerExecutablePath;

            documentElement.AppendChild(language);
            documentElement.AppendChild(executablePath);

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
