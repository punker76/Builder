
using System.Xml;
namespace Promob.Builder.SVN
{
    public class Repository
    {
        #region Attributes and Properties

        private string _root;
        public string Root
        {
            get { return _root; }
            set { _root = value; }
        }

        private string _uuid;
        public string Uuid
        {
            get { return _uuid; }
            set { _uuid = value; }
        }

        #endregion

        #region Static Methods

        public static Repository Load(XmlElement xmlElement)
        {
            if (!xmlElement.Name.Equals("repository"))
                return null;

            Repository repository = new Repository();

            foreach (XmlElement child in xmlElement.ChildNodes)
            {
                if (child.Name.Equals("root"))
                    repository.Root = child.InnerText;
                else if (child.Name.Equals("uuid"))
                    repository.Uuid = child.InnerText;
            }

            return repository;
        }

        #endregion
    }
}
