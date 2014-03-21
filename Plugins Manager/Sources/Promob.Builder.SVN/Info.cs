using System.IO;
using System.Xml;

namespace Promob.Builder.SVN
{
    public class Info
    {
        #region Attributes and Properties

        private Entry _entry;
        public Entry Entry
        {
            get { return _entry; }
            set { _entry = value; }
        }

        #endregion

        #region Static Methods

        public static Info Load(Stream stream)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);

            return Info.Load(xmlDocument);
        }

        public static Info Load(string path)
        {
            FileInfo fileinfo = new FileInfo(path);

            if (!fileinfo.Exists)
                return null;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            return Info.Load(xmlDocument);
        }

        public static Info Load(XmlDocument xmlDocument)
        {
            if (!xmlDocument.DocumentElement.Name.Equals("info"))
                return null;

            XmlElement documentElement = xmlDocument.DocumentElement;

            Info info = new Info();

            foreach (XmlElement child in documentElement.ChildNodes)
                if (child.Name.Equals("entry"))
                    info.Entry = Entry.Load(child);

            return info;
        }

        #endregion
    }
}
