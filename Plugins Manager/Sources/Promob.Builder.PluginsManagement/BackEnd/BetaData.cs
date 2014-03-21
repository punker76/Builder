using System.ComponentModel;
using System.IO;
using System.Xml;
using Promob.Builder.PluginsManagement.FrontEnd.Editors;
using Promob.Builder.SVN;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    [Editor(typeof(BetaDataEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class BetaData
    {
        #region Attributes and Properties

        private string _name;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this._name) && this.Parent != null)
                    this._name = string.Format("{0} - {1}", this.Parent.Plugin.Name, this.Parent.Name);

                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        private Version _parent;
        public Version Parent
        {
            get { return _parent; }
        }

        private DependencyProducts _dependencyProducts;
        public DependencyProducts DependencyProducts
        {
            get
            {
                if (this._dependencyProducts == null)
                    this._dependencyProducts = new DependencyProducts();
                return _dependencyProducts;
            }
            set { _dependencyProducts = value; }
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
                        @"{0}\{1}\{2}\beta.data",
                        SVNManager.VERSIONS_PATH,
                        this.Parent.Plugin.Name,
                        this.Parent.Name);

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
                    this._url = string.Format(
                        "{0}/{1}/{2}/beta.data",
                        SVNManager.VERSIONS_URL,
                        this.Parent.Plugin.Name,
                        this.Parent.Name);

                return _url;
            }
        }

        #endregion

        #region Constructor

        public BetaData()
        {

        }

        #endregion

        #region Public Methods

        public void Create(Version parent)
        {
            this._dependencyProducts = new DependencyProducts();
            this._parent = parent;
            this._created = true;
            this._url = string.Format("{0}/{1}/{2}/beta.data", SVNManager.VERSIONS_URL, this._parent.Plugin.Name, this._parent.Name);

            this.Save();
        }

        public void Save()
        {
            string path = string.Format(@"{0}\{1}\{2}\beta.data", SVNManager.VERSIONS_PATH, this._parent.Plugin.Name, this._parent.Name);

            if (string.IsNullOrEmpty(path))
                throw new System.ArgumentException("Path is invalid.");

            string directory = System.IO.Path.GetDirectoryName(path);

            if (System.IO.Directory.Exists(directory))
                SVNManager.Instance.Update(directory);
            else
            {
                string url = string.Format("{0}/{1}/{2}", SVNManager.VERSIONS_URL, this._parent.Plugin.Name, this._parent.Name);
                SVNManager.Instance.Checkout(directory, url);
            }

            bool locked = SVNManager.Instance.Lock(path);

            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XmlElement documentElement = xmlDocument.CreateElement("BetaData");

            XmlAttribute name = xmlDocument.CreateAttribute("Name");
            name.Value = this.Name;

            documentElement.Attributes.Append(name);
            documentElement.AppendChild(this.DependencyProducts.Save(xmlDocument));

            xmlDocument.AppendChild(documentElement);

            xmlDocument.Save(path);

            if (!SVNManager.Instance.Exists(this._url))
                SVNManager.Instance.Add(path);

            bool unlocked = SVNManager.Instance.Unlock(this.Url);

            SVNManager.Instance.Commit(path, "Creating beta.data from Plugins Release Manager");
        }

        public void Save(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new System.ArgumentException("Path is invalid.");

            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XmlElement documentElement = xmlDocument.CreateElement("BetaData");

            XmlAttribute name = xmlDocument.CreateAttribute("Name");
            name.Value = this.Name;

            documentElement.Attributes.Append(name);
            documentElement.AppendChild(this.DependencyProducts.Save(xmlDocument));

            xmlDocument.AppendChild(documentElement);

            xmlDocument.Save(path);
        }

        public void UpdateCheckout(string path, string url)
        {
            if (string.IsNullOrEmpty(path))
                throw new System.ArgumentException("Path is invalid.");

            string directory = System.IO.Path.GetDirectoryName(path);

            if (System.IO.Directory.Exists(directory))
                SVNManager.Instance.Update(directory);
            else
                SVNManager.Instance.Checkout(directory, url);
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

        #region Overriden Methods

        public override string ToString()
        {
            if (this.DependencyProducts.Count == 0)
                return "(Empty)";
            else
                return string.Format("(Beta.data)");
        }

        #endregion

        #region Static Methods

        public static BetaData Load(Version parent, string path)
        {
            FileInfo fileinfo = new FileInfo(path);

            if (!fileinfo.Exists)
                return null;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            if (!xmlDocument.DocumentElement.Name.Equals("BetaData"))
                return null;

            XmlElement documentElement = xmlDocument.DocumentElement;
            XmlAttribute name = documentElement.Attributes["Name"];

            BetaData beta = new BetaData();
            beta._created = true;
            beta._name = name.Value;
            beta._path = path;

            if (parent != null)
            {
                beta._parent = parent;
                beta._url = string.Format("{0}/{1}/{2}/beta.data", SVNManager.VERSIONS_URL, parent.Plugin.Name, parent.Name);
            }

            foreach (XmlElement child in documentElement.ChildNodes)
            {
                if (child.Name.Equals("DependencyProducts"))
                    beta.DependencyProducts = DependencyProducts.Load(child);
            }

            return beta;
        }

        #endregion
    }
}
