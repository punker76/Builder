using System.Collections.Generic;
using Promob.Builder.Core;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class PluginCollection : List<Plugin>
    {
        #region Public Methods

        public PluginCollection Load()
        {
            return new PluginCollection();
        }

        public void Save()
        {

        }

        #endregion
    }
}
