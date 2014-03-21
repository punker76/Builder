using System.Collections.Generic;
using Promob.Builder.SVN;

namespace Promob.Builder.Core
{
    public class VersionCollection : List<Version>
    {
        #region Virtual Methods

        public virtual VersionCollection Load(Product plugin, string url)
        {
            Dictionary<string, string> versions = SVNManager.Instance.List(url);
            VersionCollection versionCollection = new VersionCollection();

            foreach (string name in versions.Keys)
            {
                Version version = new Version(plugin, name, string.Empty, string.Empty);
                versionCollection.Add(version);
            }

            return versionCollection;
        }

        #endregion
    }
}
