using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using Promob.Builder.SVN;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class Version : ICloneable
    {
        #region Attributes and Properties

        private Plugin _plugin;
        [Category("Info")]
        public Plugin Plugin
        {
            get { return _plugin; }
        }

        private string _name;
        [Category("Info")]
        public string Name
        {
            get { return _name; }
        }

        private string _url;
        [Category("Info")]
        public string Url
        {
            get { return _url; }
        }

        private string _path;
        [Browsable(false)]
        public string Path
        {
            get
            {
                if (string.IsNullOrEmpty(this._path))
                    this._path = string.Format(
                        @"{0}\{1}\{2}\project.version",
                        SVNManager.VERSIONS_PATH,
                        this.Plugin.Name,
                        this.Name);

                return _path;
            }
        }

        private PluginData _pluginData;
        [Category("General")]
        public PluginData PluginData
        {
            get
            {
                if (this._pluginData == null)
                    this._pluginData = new PluginData();

                return _pluginData;
            }
        }

        private BetaData _betaData;
        [Category("General")]
        public BetaData BetaData
        {
            get
            {
                if (this._betaData == null)
                    this._betaData = new BetaData();

                return _betaData;
            }
        }

        [Browsable(false)]
        public int MajorVersion
        {
            get
            {
                string[] split = this.Name.Split('.');

                if (split.Length != 3)
                    return 0;

                int conversion = 0;

                bool canConvert = Int32.TryParse(this.Name.Split('.')[0], out conversion);

                if (!canConvert || this._name.Equals("Current"))
                    return 0;

                return conversion;
            }
        }

        [Browsable(false)]
        public int MinorVersion
        {
            get
            {
                string[] split = this.Name.Split('.');

                if (split.Length != 3)
                    return 0;

                int conversion = 0;

                bool canConvert = Int32.TryParse(this.Name.Split('.')[1], out conversion);

                if (!canConvert || this._name.Equals("Current"))
                    return 0;

                return conversion;
            }
        }

        [Browsable(false)]
        public int BuildNumber
        {
            get
            {
                string[] split = this.Name.Split('.');

                if (split.Length != 3)
                    return 0;

                int conversion = 0;

                bool canConvert = Int32.TryParse(this.Name.Split('.')[2], out conversion);

                if (!canConvert || this._name.Equals("Current"))
                    return 0;

                return conversion;
            }
        }

        private string _status;
        [Category("Info")]
        public string Status
        {
            get
            {
                if (string.IsNullOrEmpty(this._status))
                {
                    string path = this.Path;
                    string dirPath = System.IO.Path.GetDirectoryName(path);
                    string dirUrl = string.Format("{0}/{1}/{2}", SVNManager.VERSIONS_URL, this.Plugin.Name, this.Name);

                    if (!Application.VersionsCache.ContainsKey(dirUrl))
                        Application.VersionsCache.Append(dirUrl, false);

                    CacheInfo cacheInfo = Application.VersionsCache[dirUrl];
                    string lastRevision = SVNManager.Instance.LastRevision(dirUrl);

                    if (!lastRevision.Equals(cacheInfo.LastRevision) || string.IsNullOrEmpty(cacheInfo.Status))
                    {
                        if (System.IO.Directory.Exists(dirPath))
                            SVNManager.Instance.Update(dirPath);
                        else
                            SVNManager.Instance.Checkout(dirPath, dirUrl);

                        if (!System.IO.File.Exists(path))
                            throw new FileNotFoundException(path);

                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.Load(path);

                        XmlAttribute status = xmlDocument.DocumentElement.Attributes["Status"];

                        cacheInfo.LastRevision = lastRevision;
                        cacheInfo.Status = status.Value;
                        this._status = status.Value;

                        Application.VersionsCache.Save();
                    }
                    else
                        this._status = cacheInfo.Status;
                }

                return this._status;
            }
        }

        #endregion

        #region Constructors

        public Version(Plugin plugin, string name, string url)
        {
            this._plugin = plugin;
            this._name = name;
            this._url = url;
        }

        #endregion

        #region Private Methods

        private bool LoadPluginData()
        {
            Dictionary<string, string> pluginId = SVNManager.Instance.List(this.Url);
            string[] url = new string[pluginId.Values.Count];

            pluginId.Values.CopyTo(url, 0);

            if (pluginId.Count > 0)
            {
                string path = SVNManager.Instance.Download(string.Format("{0}/plugin.data", url[0]));

                if (System.IO.File.Exists(path))
                {
                    this._pluginData = PluginData.Load(this, path);
                    System.IO.File.Delete(path);
                    return true;
                }
            }

            return false;
        }

        private bool LoadBeta()
        {
            string url = string.Format("{0}/{1}/{2}/beta.data", SVNManager.VERSIONS_URL, this.Plugin.Name, this.Name);
            string path = string.Empty;

            if (SVNManager.Instance.Exists(url))
            {
                path = SVNManager.Instance.Download(url);

                if (System.IO.File.Exists(path))
                {
                    this._betaData = BetaData.Load(this, path);
                    System.IO.File.Delete(path);
                    return true;
                }
            }
            else
            {
                this.BetaData.Create(this);
                return true;
            }

            return false;
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            this.LoadPluginData();
            this.LoadBeta();
        }

        public string CreateBeta(string path)
        {
            return this.CreateBeta(path, false);
        }

        public string CreateBeta(string path, bool test)
        {
            List<string> folders = new List<string>();

            string plugin = this.Plugin.Name;
            string version = this.Name;
            string betaName = string.Empty;
            string folder = string.Empty;
            string destinationFolder = string.Empty;

            if (test)
            {
                DependencyProducts dependencyProducts = this.BetaData.DependencyProducts;

                foreach (DependencyProduct dependencyproduct in dependencyProducts)
                {
                    betaName = SVNManager.Instance.CreateBeta(dependencyproduct.Name, dependencyproduct.Version);
                    folder = SVNManager.Instance.DownloadRelease(dependencyproduct.Name, betaName);
                    folders.Add(string.Format(@"{0}\{1}\{2}\Promob5", folder, dependencyproduct.Name, betaName));
                }
            }

            betaName = SVNManager.Instance.CreateBeta(plugin, version);
            folder = SVNManager.Instance.DownloadRelease(plugin, betaName);
            folders.Add(string.Format(@"{0}\{1}\{2}\Promob5", folder, plugin, betaName));

            foreach (string source in folders)
            {
                try
                {
                    destinationFolder = string.Format(@"{0}\{1} - {2}", path, plugin, version);

                    if (!System.IO.Directory.Exists(destinationFolder))
                        System.IO.Directory.CreateDirectory(destinationFolder);

                    Promob.Builder.IO.IOHelper.MoveDirectoryContent(source, destinationFolder);
                }
                catch (IOException ex)
                {
                    throw ex;
                }
            }

            return destinationFolder;
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this._name;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            Version clone = new Version(this._plugin, this._name, this._url);
            clone._pluginData = this.PluginData;
            clone._betaData = this.BetaData;
            return clone;
        }

        #endregion
    }
}