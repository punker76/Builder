using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using Promob.Builder.PluginsManagement.FrontEnd.Editors;
using Promob.Builder.SVN;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    [Editor(typeof(PluginDataEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PluginData
    {
        #region Attributes and Properties

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _productId;
        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        private string _creator;
        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        private HostApplications _hostApplications;
        public HostApplications HostApplications
        {
            get
            {
                if (this._hostApplications == null)
                    this._hostApplications = new HostApplications();

                return this._hostApplications;
            }
            set
            {
                this._hostApplications = value;
            }
        }

        private DependencyAssemblies _dependencyAssemblies;
        public DependencyAssemblies DependencyAssemblies
        {
            get
            {
                if (this._dependencyAssemblies == null)
                    this._dependencyAssemblies = new DependencyAssemblies();

                return this._dependencyAssemblies;
            }
            set
            {
                this._dependencyAssemblies = value;
            }
        }

        private Content _content;
        public Content Content
        {
            get
            {
                if (this._content == null)
                    this._content = new Content();

                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        private PluginVersion _parent;
        public PluginVersion Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private bool _created;
        public bool Created
        {
            get { return _created; }
        }

        private string _path;
        public string Path
        {
            get
            {
                if (string.IsNullOrEmpty(this._path))
                    this._path = string.Format(
                        @"{0}\{1}\{2}\Promob5\Program\Plugins\{3}\plugin.data",
                        SVNManager.DEV_PATH,
                        this.Parent.Product.Name,
                        this.Parent.VersionNumber,
                        this.Id.ToString());

                return _path;
            }
            set
            {
                this._path = value;
            }
        }

        private string _url;
        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(this._url))
                    this._url = string.Format("{0}/{1}/{2}/{3}/plugin.data", SVNManager.PLUGINS_URL, this.Parent.Product.Name, this.Parent.VersionNumber, this.Id);

                return _url;
            }
            set
            {
                this._url = value;
            }
        }

        #endregion

        #region Private Methods

        private void SaveToServer(XmlDocument xmlDocument)
        {
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "PluginDataTemp");
            string tempFilename = string.Format(@"{0}\{1}", tempPath, "plugin.data");
            string tempUrl = this.Url.Substring(0, this.Url.IndexOf("plugin.data") - 1);

            SVNManager.Instance.Checkout(tempPath, tempUrl);

            //using (FileStream stream = new FileStream(tempFilename, FileMode.Create))
            //using (XmlWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
            //    xmlDocument.Save(writer);

            using (FileStream stream = new FileStream(tempFilename, FileMode.Create))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;
                settings.OmitXmlDeclaration = true;
                settings.Indent = true;
                settings.IndentChars = "\t";

                using (XmlWriter writer = XmlTextWriter.Create(stream, settings))
                    xmlDocument.Save(writer);
            }

            SVNManager.Instance.Commit(tempFilename, "Changing plugin.data version");
            Promob.Builder.IO.IOHelper.DeleteDirectory(tempPath);
        }

        #endregion

        #region Public Methods

        public void Save()
        {
            this.Save(string.Empty);
        }

        public void Save(string path)
        {
            if (string.IsNullOrEmpty(path))
                path = this.Path;

            string url = string.Format("{0}/{1}/{2}/{3}", SVNManager.PLUGINS_URL, this._parent.Product.Name, this._parent.VersionNumber, this.Id);

            SVNManager.Instance.Unlock(this.Url);
            SVNManager.Instance.Update(path);

            this.SaveLocal(path);

            if (!SVNManager.Instance.Exists(this.Url))
                SVNManager.Instance.Add(this.Path);

            SVNManager.Instance.Commit(this.Path, "Creating plugin.data from Plugins Release Manager");
        }

        public void SaveLocal(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement documentElement = xmlDocument.CreateElement("PluginData");

            XmlAttribute id = xmlDocument.CreateAttribute("Id");
            XmlAttribute productId = xmlDocument.CreateAttribute("ProductId");
            XmlAttribute name = xmlDocument.CreateAttribute("Name");
            XmlAttribute description = xmlDocument.CreateAttribute("Description");
            XmlAttribute version = xmlDocument.CreateAttribute("Version");
            XmlAttribute creator = xmlDocument.CreateAttribute("Creator");

            id.Value = this.Id.ToString();
            productId.Value = this.ProductId.ToString();
            name.Value = this.Name;
            description.Value = this.Description;
            version.Value = this.Version;
            creator.Value = this.Creator;

            documentElement.Attributes.Append(id);
            documentElement.Attributes.Append(productId);
            documentElement.Attributes.Append(name);
            documentElement.Attributes.Append(description);
            documentElement.Attributes.Append(version);
            documentElement.Attributes.Append(creator);

            documentElement.AppendChild(this.HostApplications.Save(xmlDocument));
            documentElement.AppendChild(this.DependencyAssemblies.Save(xmlDocument));
            documentElement.AppendChild(this.Content.Save(xmlDocument));

            xmlDocument.AppendChild(documentElement);

            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                this.SaveToServer(xmlDocument);
            else
            {
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Encoding = Encoding.UTF8;
                    settings.OmitXmlDeclaration = true;
                    settings.Indent = true;
                    settings.IndentChars = "\t";

                    using (XmlWriter writer = XmlTextWriter.Create(stream, settings))
                        xmlDocument.Save(writer);
                }
            }
        }

        public void UpdateCheckout()
        {
            this.UpdatePluginsCache();

            if (string.IsNullOrEmpty(this.Path))
                throw new System.ArgumentException("Path is invalid.");

            string directory = System.IO.Path.GetDirectoryName(this.Path);

            if (System.IO.Directory.Exists(directory))
                SVNManager.Instance.Update(directory);
        }

        private void UpdatePluginsCache()
        {
            string dirUrl = this.Url.Remove(this.Url.IndexOf("plugin.data") - 1);

            if (!Application.PluginsCache.ContainsKey(dirUrl))
                Application.PluginsCache.Append(dirUrl, false, false);

            CacheInfo cacheInfo = Application.PluginsCache[dirUrl];
            Info info = SVNManager.Instance.Info(dirUrl);

            if (info.Entry.Revision.Equals(cacheInfo.LastRevision))
                return;
            else
            {
                cacheInfo.LastRevision = info.Entry.Revision;

                Application.PluginsCache.Save();
                this.Update();
            }
        }

        private PluginData Load()
        {
            PluginData ret = null;
            string url = string.Format("{0}/{1}/{2}", SVNManager.PLUGINS_URL, this.Parent.Product.Name, this.Parent.VersionNumber);
            Dictionary<string, string> pluginId = SVNManager.Instance.List(url);

            string[] urls = new string[pluginId.Values.Count];

            pluginId.Values.CopyTo(urls, 0);

            if (pluginId.Count > 0)
            {
                string path = SVNManager.Instance.Download(string.Format("{0}/plugin.data", urls[0]));

                if (System.IO.File.Exists(path))
                {
                    ret = PluginData.Load(this.Parent, path);
                    System.IO.File.Delete(path);
                    ret.Path = string.Empty;
                    ret.Url = string.Empty;
                    ret.Parent = this.Parent;
                    return ret;
                }
            }

            return null;
        }

        private void Update()
        {
            PluginData newPluginData = this.Load();

            if (newPluginData == null)
                return;

            this._id = newPluginData._id;
            this._productId = newPluginData._productId;
            this._name = newPluginData._name;
            this._description = newPluginData._description;
            this._version = newPluginData._version;
            this._creator = newPluginData._creator;
            this._content = newPluginData._content;
            this._created = newPluginData._created;
            this._dependencyAssemblies = newPluginData._dependencyAssemblies;
            this._hostApplications = newPluginData._hostApplications;
            this._parent = newPluginData._parent;
            this._path = newPluginData._path;
            this._url = newPluginData._url;
        }

        public void Lock()
        {
            SVNManager.Instance.Lock(this.Url);
        }

        public void Unlock()
        {
            SVNManager.Instance.Unlock(this.Url);
        }

        #endregion

        #region Override Methods

        public override string ToString()
        {
            if (!this.Created)
                return "(Empty)";
            else
                return string.Format("(Plugin.data)");
        }

        #endregion

        #region Static Methods

        public static PluginData Load(PluginVersion parent, string path)
        {
            FileInfo fileinfo = new FileInfo(path);

            if (!fileinfo.Exists)
                return null;

            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load(path);
            }
            catch
            {
                string text = System.IO.File.ReadAllText(path, Encoding.Default);
                System.IO.File.WriteAllText(path, text, Encoding.UTF8);

                using (StreamReader streamreader = new StreamReader(path, Encoding.UTF8))
                    xmlDocument.Load(streamreader.BaseStream);
            }

            if (!xmlDocument.DocumentElement.Name.Equals("PluginData"))
                return null;

            XmlElement documentElement = xmlDocument.DocumentElement;

            XmlAttribute id = documentElement.Attributes["Id"];
            XmlAttribute productId = documentElement.Attributes["ProductId"];
            XmlAttribute name = documentElement.Attributes["Name"];
            XmlAttribute description = documentElement.Attributes["Description"];
            XmlAttribute version = documentElement.Attributes["Version"];
            XmlAttribute creator = documentElement.Attributes["Creator"];

            PluginData pluginData = new PluginData();

            pluginData.Id = System.Convert.ToInt32(id.Value);
            pluginData.ProductId = System.Convert.ToInt32(productId.Value);
            pluginData.Name = name.Value;
            pluginData.Description = description.Value;
            pluginData.Version = version.Value;
            pluginData.Creator = creator.Value;
            pluginData.Parent = parent;
            pluginData.Path = path;

            foreach (XmlElement child in documentElement.ChildNodes)
            {
                if (child.Name.Equals("Content"))
                    pluginData.Content = Content.Load(child);
                else if (child.Name.Equals("HostApplications"))
                    pluginData.HostApplications = HostApplications.Load(child);
                else if (child.Name.Equals("DependancyAssemblies"))
                    pluginData.DependencyAssemblies = DependencyAssemblies.Load(child);
            }

            pluginData._created = true;

            return pluginData;
        }

        #endregion
    }
}
