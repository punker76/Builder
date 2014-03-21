using System;
using System.ComponentModel;
using Promob.Builder.SVN;

namespace Promob.Builder.Core
{
    public class Version : ICloneable
    {
        #region Attributes and Properties

        private Product _product;
        [Category("Info")]
        public Product Product
        {
            get { return this._product; }
        }

        private string _versionNumber;
        [Category("Info")]
        public string VersionNumber
        {
            get { return this._versionNumber; }
        }

        [Browsable(false)]
        public int MajorVersion
        {
            get
            {
                string[] split = this.VersionNumber.Split('.');

                if (split.Length != 3)
                    return 0;

                int conversion = 0;

                bool canConvert = Int32.TryParse(this.VersionNumber.Split('.')[0], out conversion);

                if (!canConvert || this._versionNumber.Equals("Current"))
                    return 0;

                return conversion;
            }
        }

        [Browsable(false)]
        public int MinorVersion
        {
            get
            {
                string[] split = this.VersionNumber.Split('.');

                if (split.Length != 3)
                    return 0;

                int conversion = 0;

                bool canConvert = Int32.TryParse(this.VersionNumber.Split('.')[1], out conversion);

                if (!canConvert || this._versionNumber.Equals("Current"))
                    return 0;

                return conversion;
            }
        }

        [Browsable(false)]
        public int BuildNumber
        {
            get
            {
                string[] split = this.VersionNumber.Split('.');

                if (split.Length != 3)
                    return 0;

                int conversion = 0;

                bool canConvert = Int32.TryParse(this.VersionNumber.Split('.')[2], out conversion);

                if (!canConvert || this._versionNumber.Equals("Current"))
                    return 0;

                return conversion;
            }
        }

        private string _status;
        [Category("Info")]
        public virtual string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        private string _url;
        [Category("Info")]
        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(this._url))
                    this._url = string.Format(
                        @"{0}/{1}/{2}/project.version",
                        SVNManager.VERSIONS_URL,
                        this.Product.Name,
                        this.VersionNumber);

                return this._url;
            }
            set
            {
                this._url = value;
            }
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
                        this.Product.Name,
                        this.VersionNumber);

                return this._path;
            }
            set
            {
                this._path = value;
            }
        }

        #endregion

        #region Constructors

        public Version(Product product, string name, string path, string url)
        {
            this._product = product;
            this._versionNumber = name;
            this._url = url;
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this._versionNumber;
        }

        #endregion

        #region Virtual Methods

        public virtual bool IsValidVersion(Version version)
        {
            if (version.VersionNumber.Equals("Current"))
                return true;

            if ((version.MajorVersion >= this.MajorVersion && version.MinorVersion > this.MinorVersion) ||
                (version.MinorVersion == this.MinorVersion && version.BuildNumber >= this.BuildNumber))
                return true;

            return false;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new Version(this._product, this._versionNumber, this._path, this._url);
        }

        #endregion
    }
}