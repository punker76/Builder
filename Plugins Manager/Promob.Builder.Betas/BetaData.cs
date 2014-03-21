using System.ComponentModel;
using System.IO;
using System.Xml;
using Promob.Builder.Core;
using Promob.Builder.SVN;

namespace Promob.Builder.Betas
{
    [Editor(typeof(BetaDataEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class BetaData
    {
        #region Constructor

        public BetaData()
        {
        }

        public BetaData(Version version)
        {
            this._version = version;
        }

        #endregion

        #region Attributes and Properties

        private string _name;
        public string Name
        {
            get { return this._name; }

            set { this._name = value; }
        }

        private Version _version;
        public Version Version
        {
            get { return this._version; }

            set { this._version = value; }
        }

        private DependencyProducts _dependencyProducts;
        public DependencyProducts DependencyProducts
        {
            get
            {
                if (this._dependencyProducts == null)
                    this._dependencyProducts = new DependencyProducts();

                return this._dependencyProducts;
            }

            set { this._dependencyProducts = value; }
        }

        private bool _created;
        public bool Created
        {
            get { return this._created; }
        }

        private string _path;
        public string Path
        {
            get
            {
                if (string.IsNullOrEmpty(this._path))
                    this._path = string.Format(@"{0}\{1}\{2}\{3}",
                        SVNManager.VERSIONS_PATH,
                        this._version.Product.Name,
                        this._version.VersionNumber,
                        "beta.data");

                return this._path;
            }

            set { this._path = value; }
        }

        private string _url;
        public string Url
        {
            get { return this._url; }

            set { this._url = value; }
        }

        #endregion

        #region Public Methods

        public void Create(Version version)
        {
            this._dependencyProducts = new DependencyProducts();
            this._version = version;
            this._created = true;

            this.Save();
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(this.Path))
                throw new System.ArgumentException("Path is invalid.");

            string directory = System.IO.Path.GetDirectoryName(this.Path);

            if (Directory.Exists(directory))
                SVNManager.Instance.Update(directory);
            else
                SVNManager.Instance.Checkout(directory, this._url);

            bool locked = SVNManager.Instance.Lock(this.Path);

            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XmlElement documentElement = xmlDocument.CreateElement("BetaData");

            XmlAttribute name = xmlDocument.CreateAttribute("Name");
            name.Value = this._name;

            documentElement.Attributes.Append(name);
            documentElement.AppendChild(this.DependencyProducts.Save(xmlDocument));

            xmlDocument.AppendChild(documentElement);

            xmlDocument.Save(this.Path);

            if (!SVNManager.Instance.Exists(this._url))
                SVNManager.Instance.Add(this.Path);

            bool unlocked = SVNManager.Instance.Unlock(this._url);

            SVNManager.Instance.Commit(this.Path, "Creating beta.data from Plugins Release Manager");
        }

        public void Save(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new System.ArgumentException("Path is invalid.");

            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XmlElement documentElement = xmlDocument.CreateElement("BetaData");

            XmlAttribute name = xmlDocument.CreateAttribute("Name");
            name.Value = this._name;

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
            SVNManager.Instance.Lock(this._url);
        }

        public void Unlock()
        {
            SVNManager.Instance.Unlock(this._url);
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

        public static BetaData Load(string path)
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
            beta.Path = path;

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
