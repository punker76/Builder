using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Promob.Builder.Core;
using Promob.Builder.SVN;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class Plugin : Product
    {
        #region Constructors

        public Plugin(string name)
            : base(name)
        {
        }

        #endregion

        #region Private Methods

        private void LoadAllPluginVersions()
        {
            string url = string.Format("{0}/{1}", SVNManager.PLUGINS_URL, this.Name);
            PluginVersionCollection versions = new PluginVersionCollection();
            versions.Load(this, url);

            IOrderedEnumerable<Version> currentCollection = (from version in versions
                                                             where version.VersionNumber.Equals("Current") && !version.VersionNumber.StartsWith("Beta")
                                                             orderby version.MajorVersion, version.MinorVersion, version.BuildNumber
                                                             select version);

            IEnumerable<Version> versionCollection = (from version in versions
                                                      where !version.VersionNumber.Equals("Current")
                                                      orderby version.MajorVersion, version.MinorVersion, version.BuildNumber
                                                      select version).Reverse();

            this.Versions = new PluginVersionCollection();
            this.Versions.AddRange(currentCollection);
            this.Versions.AddRange(versionCollection);
        }

        private void LoadPluginVersions()
        {
            string url = string.Format("{0}/{1}", SVNManager.PLUGINS_URL, this.Name);
            PluginVersionCollection versions = new PluginVersionCollection();
            versions.Load(this, url);

            IEnumerable<Version> notCurrentCollection = (from version in versions
                                                         where !version.VersionNumber.Equals("Current") && !version.VersionNumber.StartsWith("Beta")
                                                         orderby version.MajorVersion, version.MinorVersion, version.BuildNumber
                                                         select version).Reverse();

            IOrderedEnumerable<Version> currentCollection = (from version in versions
                                                             where version.VersionNumber.Equals("Current") && !version.VersionNumber.StartsWith("Beta")
                                                             orderby version.MajorVersion, version.MinorVersion, version.BuildNumber
                                                             select version);

            this.Versions = new PluginVersionCollection();
            this.Versions.AddRange(currentCollection);

            if (notCurrentCollection.Count() > 0)
                this.Versions.Add(notCurrentCollection.First());
        }

        static T First<T>(IEnumerable<T> items)
        {
            using (IEnumerator<T> iter = items.GetEnumerator())
            {
                iter.MoveNext();
                return iter.Current;
            }
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            this.LoadPluginVersions();
        }

        public void LoadAll()
        {
            this.LoadAllPluginVersions();
        }

        #endregion
    }
}
