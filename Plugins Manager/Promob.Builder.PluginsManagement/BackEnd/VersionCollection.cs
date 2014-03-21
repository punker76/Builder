using System.Collections.Generic;
using Promob.Builder.SVN;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class VersionCollection : List<Version>
    {
        #region Static Methods

        public static VersionCollection Load(Plugin plugin, string url)
        {
            Dictionary<string, string> versions = SVNManager.Instance.List(url);
            VersionCollection versionCollection = new VersionCollection();

            foreach (string name in versions.Keys)
            {
                Version version = new Version(plugin, name, versions[name]);
                versionCollection.Add(version);
            }

            return versionCollection;
        }

        #endregion
    }
}
