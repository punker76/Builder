using System.ComponentModel;

namespace Promob.Builder.Core
{
    public class Product
    {
        #region Attributes and Properties

        private string _name;
        [Category("Plugin")]
        public string Name
        {
            get { return this._name; }
        }

        private VersionCollection _versions;
        [Browsable(false)]
        public VersionCollection Versions
        {
            get
            {
                if (this._versions == null)
                    this._versions = new VersionCollection();

                return this._versions;
            }
            set
            { this._versions = value; }
        }

        private bool _isUpToDate;
        [Browsable(false)]
        public bool IsUpToDate
        {
            get { return this._isUpToDate; }
            set { this._isUpToDate = value; }
        }

        #endregion

        #region Constructors

        public Product(string name)
        {
            this._name = name;
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}
