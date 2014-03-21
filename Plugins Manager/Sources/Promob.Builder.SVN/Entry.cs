
using System.Xml;
namespace Promob.Builder.SVN
{
    public class Entry
    {
        #region Attributes and Properties

        private string _kind;
        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private string _revision;
        public string Revision
        {
            get { return _revision; }
            set { _revision = value; }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private Repository _repository;
        public Repository Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }

        private Commit _commit;
        public Commit Commit
        {
            get { return _commit; }
            set { _commit = value; }
        }

        public bool IsDirectory
        {
            get { return this._kind.Equals("dir"); }
        }

        #endregion

        #region Static Methods

        public static Entry Load(XmlElement xmlElement)
        {
            if (!xmlElement.Name.Equals("entry"))
                return null;

            Entry entry = new Entry();

            XmlAttribute kind = xmlElement.Attributes["kind"];
            XmlAttribute path = xmlElement.Attributes["path"];
            XmlAttribute revision = xmlElement.Attributes["revision"];

            foreach (XmlElement child in xmlElement.ChildNodes)
            {
                if (child.Name.Equals("url"))
                    entry.Url = child.InnerText;
                else if (child.Name.Equals("repository"))
                    entry.Repository = Repository.Load(child);
                else if (child.Name.Equals("commit"))
                    entry.Commit = Commit.Load(child);
            }

            entry.Kind = kind.Value;
            entry.Path = path.Value;
            entry.Revision = revision.Value;

            return entry;
        }

        #endregion
    }
}
