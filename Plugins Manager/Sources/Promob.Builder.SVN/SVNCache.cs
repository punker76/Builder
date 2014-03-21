using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Promob.Builder.SVN
{
    public class SVNCache : Dictionary<string, CacheInfo>
    {
        #region Static Consts

        public static readonly string DEFAULT_PATH =
            string.Format(@"{0}\{1}", SVNManager.DEV_PATH, "SVNCache");

        #endregion

        #region Attributes and Properties

        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private bool _created;
        public bool Created
        {
            get { return _created; }
            set { _created = value; }
        }

        #endregion

        #region Constructors

        public SVNCache(string path)
        {
            this._path = path;
            this._created = File.Exists(path);
        }

        #endregion

        #region Private Methods

        private void Insert(string curUrl, bool recursive, bool cacheFiles)
        {
            Info info = SVNManager.Instance.Info(curUrl);

            if (info == null)
                return;

            CacheInfo cacheInfo = new CacheInfo();
            cacheInfo.Url = info.Entry.Url;
            cacheInfo.LastCommit = info.Entry.Commit;
            cacheInfo.IsDirectory = info.Entry.IsDirectory;
            cacheInfo.LastRevision = info.Entry.Revision;

            if (!this.ContainsKey(cacheInfo.Url))
                this.Add(cacheInfo.Url, cacheInfo);

            if (!info.Entry.IsDirectory)
                return;

            Dictionary<string, string> urls = SVNManager.Instance.List(curUrl, cacheFiles);

            if (urls == null)
                return;

            if (recursive)
                foreach (string url in urls.Values)
                    this.Insert(url, recursive, cacheFiles);
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            if (string.IsNullOrEmpty(this.Path))
                throw new System.ArgumentException("Path is invalid.");

            if (!this.Created)
                throw new System.ArgumentException("Cache is not created.");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(this.Path);

            if (!xmlDocument.DocumentElement.Name.Equals("SVNCache"))
                return;

            this.Clear();

            XmlElement xmlElement = xmlDocument.DocumentElement;

            foreach (XmlElement child in xmlElement.ChildNodes)
            {
                CacheInfo cacheInfo = CacheInfo.Load(child);
                this.Add(cacheInfo.Url, cacheInfo);
            }
        }

        public void Create(string url, bool recursive)
        {
            this.Create(url, recursive, false);
        }

        public void Append(string url, bool recursive, bool cacheFiles)
        {
            this.Insert(url, recursive, cacheFiles);
            this.Save();
        }

        public void Create(string url, bool recursive, bool cacheFiles)
        {
            this.Insert(url, recursive, cacheFiles);
            this.Save();
            this.Created = true;
        }

        public void Create(List<string> urls)
        {
            this.Create(urls, false, false);
        }

        public void Create(List<string> urls, bool recursive, bool cacheFiles)
        {
            foreach (string url in urls)
                this.Insert(url, recursive, cacheFiles);

            this.Save();
            this.Created = true;
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(this.Path))
                throw new System.ArgumentException("Path is invalid.");

            if (File.Exists(this.Path))
                File.Delete(this.Path);

            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XmlElement documentElement = xmlDocument.CreateElement("SVNCache");

            xmlDocument.AppendChild(documentElement);

            foreach (CacheInfo cacheInfo in this.Values)
                documentElement.AppendChild(cacheInfo.Save(xmlDocument));

            xmlDocument.Save(this.Path);
        }

        #endregion
    }
}
