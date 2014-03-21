namespace Promob.Builder.Options
{
    public abstract class AbstractOptions
    {
        #region Constructors

        public AbstractOptions() { }

        public AbstractOptions(string path)
        {
            this._path = path;
        }

        #endregion

        #region Attributes and Properties

        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public abstract OptionsContainer OptionsContainer { get; }

        #endregion

        #region Public Methods

        public abstract void Load();
        public abstract void Save();

        #endregion
    }
}
