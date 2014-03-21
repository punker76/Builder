
using System.ComponentModel;
using System.Linq;
using System.Xml;
namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class HostApplications : BindingList<HostApplication>
    {
        #region Indexers

        public HostApplication this[string hostApplicationName]
        {
            get
            {
                HostApplication host = this.ToList().Find(new System.Predicate<HostApplication>(h => h.Name.Equals(hostApplicationName)));
                return host;
            }
        }

        #endregion

        #region Public Methods

        public bool Contains(string hostApplicationName)
        {
            if (this.Count > 0)
            {
                HostApplication host = this.ToList().Find(new System.Predicate<HostApplication>(h => h.Name.Equals(hostApplicationName)));
                return host != null;
            }

            return false;
        }

        public XmlElement Save(XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.CreateElement("HostApplications");

            foreach (HostApplication hostApplication in this)
                xmlElement.AppendChild(hostApplication.Save(xmlDocument));

            return xmlElement;
        }

        #endregion

        #region Static Methods

        public static HostApplications Load(XmlElement xmlElement)
        {
            HostApplications hostApplications = new HostApplications();

            foreach (XmlElement child in xmlElement.ChildNodes)
            {
                if (child.Name.Equals("HostApplication"))
                    hostApplications.Add(HostApplication.Load(child));
            }

            return hostApplications;
        }

        #endregion
    }
}