using System.Xml;

namespace Promob.Builder.SVN
{
    public class Commit
    {
        #region Attributes and Properties

        private string _revision;
        public string Revision
        {
            get { return _revision; }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
        }

        #endregion

        #region Overriden Methods

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Commit))
                return false;

            Commit commit = obj as Commit;

            bool equals =
                (this._author.Equals(commit._author) &&
                this._date.Equals(commit._date) &&
                this._revision.Equals(commit._revision));

            return equals;
        }

        #endregion

        #region Public Methods

        public XmlElement Save(XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.CreateElement("commit");

            XmlAttribute revision = xmlDocument.CreateAttribute("revision");
            revision.Value = this._revision;

            xmlElement.Attributes.Append(revision);

            XmlElement authorElement = xmlDocument.CreateElement("author");
            authorElement.InnerText = this._author;
            XmlElement dateElement = xmlDocument.CreateElement("date");
            dateElement.InnerText = this._date;

            xmlElement.AppendChild(authorElement);
            xmlElement.AppendChild(dateElement);

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static Commit Load(XmlElement xmlElement)
        {
            if (!xmlElement.Name.Equals("commit"))
                return null;

            Commit commit = new Commit();

            XmlAttribute revision = xmlElement.Attributes["revision"];

            foreach (XmlElement child in xmlElement.ChildNodes)
            {
                if (child.Name.Equals("author"))
                    commit._author = child.InnerText;
                else if (child.Name.Equals("date"))
                    commit._date = child.InnerText;
            }

            commit._revision = revision.Value;

            return commit;
        }

        #endregion
    }
}
