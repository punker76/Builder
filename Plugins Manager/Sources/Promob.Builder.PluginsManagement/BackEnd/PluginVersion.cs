using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using Promob.Builder.Betas;
using Promob.Builder.Core;
using Promob.Builder.SVN;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class PluginVersion : Version
    {
        #region Attributes and Properties

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

        private string _status;
        public override string Status
        {
            get
            {
                if (string.IsNullOrEmpty(this._status))
                {
                    string path = this.Path;
                    string dirPath = System.IO.Path.GetDirectoryName(path);
                    string dirUrl = string.Format("{0}/{1}/{2}", SVNManager.VERSIONS_URL, this.Product.Name, this.VersionNumber);

                    if (!Application.VersionsCache.ContainsKey(dirUrl))
                        Application.VersionsCache.Append(dirUrl, false, false);

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
            set
            {
                base.Status = value;
            }
        }

        #endregion

        #region Constructors

        public PluginVersion(Plugin plugin, string name, string path, string url)
            : base(plugin, name, path, url)
        {

        }

        #endregion

        #region Overriden Methods

        public override bool IsValidVersion(Version version)
        {
            if (version.VersionNumber.Equals("Current"))
                return true;

            if (version.Product.Name.Equals("Promob") || version.Product.Name.Equals("Catalog"))
            {
                HostApplication hostApplication = this.PluginData.HostApplications["Promob"];

                if ((version.MajorVersion >= hostApplication.MajorVersion && version.MinorVersion > hostApplication.MinorVersion) ||
                    (version.MinorVersion == hostApplication.MinorVersion && version.BuildNumber >= hostApplication.BuildNumber))
                    return true;
            }
            else
            {
                if ((version.MajorVersion >= this.BetaData.Version.MajorVersion && version.MinorVersion > this.BetaData.Version.MinorVersion) ||
                    (version.MinorVersion == this.BetaData.Version.MinorVersion && version.BuildNumber >= this.BetaData.Version.BuildNumber))
                    return true;
            }

            return false;
        }

        #endregion

        #region Private Methods

        private bool LoadPluginData()
        {
            string url = string.Format("{0}/{1}/{2}", SVNManager.PLUGINS_URL, this.Product.Name, this.VersionNumber);
            Dictionary<string, string> pluginId = SVNManager.Instance.List(url);
            string[] urls = new string[pluginId.Values.Count];

            pluginId.Values.CopyTo(urls, 0);

            if (pluginId.Count > 0)
            {
                string path = SVNManager.Instance.Download(string.Format("{0}/plugin.data", urls[0]));

                if (System.IO.File.Exists(path))
                {
                    this._pluginData = PluginData.Load(this, path);
                    System.IO.File.Delete(path);
                    this._pluginData.Path = string.Empty;
                    this._pluginData.Url = string.Empty;
                    this._pluginData.Parent = this;
                    return true;
                }
            }

            return false;
        }

        private bool LoadBeta()
        {
            string url = string.Format("{0}/{1}/{2}/beta.data", SVNManager.VERSIONS_URL, this.Product.Name, this.VersionNumber);
            string path = string.Empty;

            if (SVNManager.Instance.Exists(url))
            {
                path = SVNManager.Instance.Download(url);

                if (System.IO.File.Exists(path))
                {
                    this._betaData = BetaData.Load(path);
                    System.IO.File.Delete(path);
                    this._betaData.Path = string.Empty;
                    this._betaData.Url = string.Empty;
                    this._betaData.Version = this;
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
            Dictionary<DependencyProduct, string> betaNames = new Dictionary<DependencyProduct, string>();

            string plugin = this.Product.Name;
            string version = this.VersionNumber;
            string betaName = string.Empty;
            string folder = string.Empty;
            string destinationFolder = string.Empty;

            if (test)
            {
                DependencyProducts dependencyProducts = this.BetaData.DependencyProducts;

                foreach (DependencyProduct dependencyProduct in dependencyProducts)
                {
                    betaName = SVNManager.Instance.CreateBeta(dependencyProduct.Name, dependencyProduct.Version);
                    folder = SVNManager.Instance.DownloadRelease(dependencyProduct.Name, betaName);
                    folders.Add(string.Format(@"{0}\{1}\{2}\Promob5", folder, dependencyProduct.Name, betaName));
                    betaNames.Add(dependencyProduct, betaName);
                }
            }

            DependencyProduct product = new DependencyProduct(plugin, version);

            betaName = SVNManager.Instance.CreateBeta(plugin, version);
            folder = SVNManager.Instance.DownloadRelease(plugin, betaName);
            folders.Add(string.Format(@"{0}\{1}\{2}\Promob5", folder, plugin, betaName));

            betaNames.Add(product, betaName);

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

            foreach (DependencyProduct dependencyproduct in betaNames.Keys)
                SVNManager.Instance.DeleteBeta(dependencyproduct.Name, dependencyproduct.Version, betaNames[dependencyproduct]);

            return destinationFolder;
        }

        #endregion
    }
}
