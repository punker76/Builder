using System.ComponentModel;
using System.Xml;
using Promob.Builder.Options;
using Promob.Builder.PluginsManagement.FrontEnd.Options;

namespace Promob.Builder.PluginsManagement.BackEnd.Options
{
    public class PublishOptions : AbstractOptions, INotifyPropertyChanged
    {
        #region Constructors

        public PublishOptions(string path)
            : base(path)
        {

        }

        #endregion

        #region Attributes and Properties

        private string _betasPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Betas");
        public string BetasPath
        {
            get { return this._betasPath; }
            set
            {
                this._betasPath = value;
                this.NotifyPropertyChanged("BetasPath");
            }
        }

        private string _executablePath;
        public string ExecutablePath
        {
            get { return this._executablePath; }
            set
            {
                this._executablePath = value;
                this.NotifyPropertyChanged("ExecutablePath");
            }
        }

        private string _installPath;
        public string InstallPath
        {
            get { return this._installPath; }
            set
            {
                this._installPath = value;
                this.NotifyPropertyChanged("InstallPath");
            }
        }

        private string _localMediaPath;
        public string LocalMediaPath
        {
            get { return this._localMediaPath; }
            set
            {
                this._localMediaPath = value;
                this.NotifyPropertyChanged("LocalMediaPath");
            }
        }

        private string _email;
        public string Email
        {
            get { return this._email; }
            set
            {
                this._email = value;
                this.NotifyPropertyChanged("Email");
            }
        }

        private string _emailList;
        public string EmailList
        {
            get { return this._emailList; }
            set
            {
                this._emailList = value;
                this.NotifyPropertyChanged("EmailList");
            }
        }

        public override OptionsContainer OptionsContainer
        {
            get
            {
                return new PublishOptionsContainer(this);

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

            if (!xmlDocument.DocumentElement.Name.Equals("PublishOptions"))
                return;

            XmlElement documentElement = xmlDocument.DocumentElement;

            foreach (XmlElement child in documentElement.ChildNodes)
            {
                if (child.Name.Equals("ExecutablePath"))
                    this.ExecutablePath = child.InnerText;
                else if (child.Name.Equals("InstallPath"))
                    this.InstallPath = child.InnerText;
                else if (child.Name.Equals("LocalMediaPath"))
                    this.LocalMediaPath = child.InnerText;
                else if (child.Name.Equals("BetasPath"))
                    this.BetasPath = child.InnerText;
                else if (child.Name.Equals("Email"))
                    this.Email = child.InnerText;
                else if (child.Name.Equals("EmailList"))
                    this.EmailList = child.InnerText;
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
            XmlElement documentElement = xmlDocument.CreateElement("PublishOptions");

            XmlElement executablePath = xmlDocument.CreateElement("ExecutablePath");
            XmlElement installPath = xmlDocument.CreateElement("InstallPath");
            XmlElement localMediaPath = xmlDocument.CreateElement("LocalMediaPath");
            XmlElement betasPath = xmlDocument.CreateElement("BetasPath");
            XmlElement email = xmlDocument.CreateElement("Email");
            XmlElement sendEmail = xmlDocument.CreateElement("SendEmail");
            XmlElement emailList = xmlDocument.CreateElement("EmailList");

            executablePath.InnerText = this.ExecutablePath;
            installPath.InnerText = this.InstallPath;
            localMediaPath.InnerText = this.LocalMediaPath;
            betasPath.InnerText = this.BetasPath;
            email.InnerText = this.Email;
            emailList.InnerText = this.EmailList;

            documentElement.AppendChild(executablePath);
            documentElement.AppendChild(installPath);
            documentElement.AppendChild(localMediaPath);
            documentElement.AppendChild(betasPath);
            documentElement.AppendChild(email);
            documentElement.AppendChild(sendEmail);
            documentElement.AppendChild(emailList);

            xmlDocument.AppendChild(documentElement);

            xmlDocument.Save(this.Path);
        }

        public override string ToString()
        {
            return "Publish";
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
