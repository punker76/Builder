using System.Collections.Generic;
using Promob.Builder.Core;
using Promob.Builder.SVN;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class PluginVersionCollection : VersionCollection
    {
        #region Public Methods

        public virtual void Load(Plugin plugin, string url)
        {
            Dictionary<string, string> versions = SVNManager.Instance.List(url);

            if (versions == null)
                return;

            foreach (string name in versions.Keys)
            {
                PluginVersion version = new PluginVersion(plugin, name, string.Empty, string.Empty);
                this.Add(version);
            }
        }

        #endregion
    }
}
