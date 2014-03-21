
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class Content : BindingList<IContentObject>
    {
        #region Indexers

        public IContentObject this[string name]
        {
            get
            {
                return this.ToList().Find(new System.Predicate<IContentObject>(c => c.Name.Equals(name)));
            }
        }

        #endregion

        #region Public Methods

        public bool Contains(string name)
        {
            return this.ToList().Find(new System.Predicate<IContentObject>(c => c.Name.Equals(name))) != null;
        }

        public XmlElement Save(XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.CreateElement("Content");

            foreach (IContentObject contentObject in this)
                xmlElement.AppendChild(contentObject.Save(xmlDocument));

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static Content Load(XmlElement xmlElement)
        {
            Content content = new Content();

            foreach (XmlElement child in xmlElement.ChildNodes)
            {
                if (child.Name.Equals("Directory"))
                    content.Add(Directory.Load(child));
                else if (child.Name.Equals("File"))
                    content.Add(File.Load(child));
            }

            return content;
        }

        #endregion
    }
}
