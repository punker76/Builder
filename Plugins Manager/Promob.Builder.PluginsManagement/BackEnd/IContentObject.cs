using System.Xml;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public interface IContentObject
    {
        #region Attributes and Properties

        string Destiny { get; set; }
        string Local { get; set; }
        string Name { get; set; }
        bool Shared { get; set; }

        #endregion

        #region Methods

        XmlElement Save(XmlDocument xmlDocument);

        #endregion
    }
}