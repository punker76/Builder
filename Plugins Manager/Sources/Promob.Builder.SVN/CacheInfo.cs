
using System;
using System.Xml;
namespace Promob.Builder.SVN
{
    public class CacheInfo
    {
        #region Attributes and Properties

        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private Commit _lastCommit;
        public Commit LastCommit
        {
            get { return _lastCommit; }
            set { _lastCommit = value; }
        }

        private string _lastRevision;
        public string LastRevision
        {
            get { return _lastRevision; }
            set { _lastRevision = value; }
        }

        private bool _isUpToDate = true;
        public bool IsUpToDate
        {
            get { return this._isUpToDate; }
            set { this._isUpToDate = value; }
        }

        private bool _isDirectory;
        public bool IsDirectory
        {
            get { return _isDirectory; }
            set { _isDirectory = value; }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion

        #region Constructors

        public CacheInfo()
        {

        }

        #endregion

        #region Public Methods

        public XmlElement Save(XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.CreateElement("CacheInfo");

            XmlAttribute url = xmlDocument.CreateAttribute("Url");
            url.Value = this.Url;

            XmlAttribute isDirectory = xmlDocument.CreateAttribute("IsDirectory");
            isDirectory.Value = this.IsDirectory.ToString();

            XmlAttribute lastRevision = xmlDocument.CreateAttribute("LastRevision");
            lastRevision.Value = this.LastRevision;

            XmlAttribute status = xmlDocument.CreateAttribute("Status");
            status.Value = this._status;

            xmlElement.Attributes.Append(url);
            xmlElement.Attributes.Append(isDirectory);
            xmlElement.Attributes.Append(lastRevision);
            xmlElement.Attributes.Append(status);
            xmlElement.AppendChild(this._lastCommit.Save(xmlDocument));

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static CacheInfo Load(XmlElement xmlElement)
        {
            if (!xmlElement.Name.Equals("CacheInfo"))
                return null;

            XmlAttribute url = xmlElement.Attributes["Url"];
            XmlAttribute isDirectory = xmlElement.Attributes["IsDirectory"];
            XmlAttribute lastRevision = xmlElement.Attributes["LastRevision"];
            XmlAttribute status = xmlElement.Attributes["Status"];

            CacheInfo cacheInfo = new CacheInfo();

            cacheInfo.Url = url.Value;

            if (isDirectory != null)
                cacheInfo.IsDirectory = Convert.ToBoolean(isDirectory.Value);

            cacheInfo.Url = url.Value;

            foreach (XmlElement child in xmlElement.ChildNodes)
                if (child.Name.Equals("commit"))
                    cacheInfo._lastCommit = Commit.Load(child);

            if (lastRevision == null)
                cacheInfo._lastRevision = cacheInfo._lastCommit.Revision;
            else
                cacheInfo._lastRevision = lastRevision.Value;
            
            if (status != null)
                cacheInfo.Status = status.Value;

            return cacheInfo;
        }

        #endregion
    }
}
