using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Promob.Builder.Options;
using Promob.Builder.Performance;
using Promob.Builder.PluginsManagement.BackEnd.Options;
using Promob.Builder.SVN;
using Promob.Builder.Translation;

namespace Promob.Builder.PluginsManagement
{
    public static class Application
    {
        #region Consts

        public static readonly string APPLICATIONDATA_PATH = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Plugins Release Manager");
        public static readonly string TRANSLATIONS_PATH = System.IO.Path.Combine(APPLICATIONDATA_PATH, "Translations");
        public static readonly string OPTIONS_PATH = System.IO.Path.Combine(APPLICATIONDATA_PATH, "Options");
        public static readonly string CACHE_PATH = System.IO.Path.Combine(APPLICATIONDATA_PATH, "Cache");
        public static readonly string VERSIONS_CACHE_PATH = System.IO.Path.Combine(CACHE_PATH, "versions.cache");
        public static readonly string PLUGINS_CACHE_PATH = System.IO.Path.Combine(CACHE_PATH, "plugins.cache");
        public static readonly string REFERENCES_CACHE_PATH = System.IO.Path.Combine(CACHE_PATH, "references.cache");

        public static SVNCache VersionsCache;
        public static SVNCache PluginsCache;
        public static SVNCache ReferencesCache;

        #endregion

        #region Private Methods

        private static void InitializeManagers()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Application));
            OptionsManager.GetManager().Initialize(Application.OPTIONS_PATH, assembly);

            int currentLanguage = (OptionsManager.GetManager().OptionsCollection["GeneralOptions"] as GeneralOptions).Language;
            TranslationManager.GetManager().Initialize(Application.TRANSLATIONS_PATH, currentLanguage);

            PerformanceManager.Instance.Initialize();
        }

        private static void TerminateManagers()
        {
            PerformanceManager.Instance.Terminate();
        }

        private static void CreateCaches()
        {
            Application.VersionsCache = new SVNCache(VERSIONS_CACHE_PATH);
            Application.PluginsCache = new SVNCache(PLUGINS_CACHE_PATH);
            Application.ReferencesCache = new SVNCache(REFERENCES_CACHE_PATH);

            if (!Application.VersionsCache.Created)
                Application.VersionsCache.Create(SVNManager.VERSIONS_URL, true);
            else
                Application.VersionsCache.Load();

            if (!Application.PluginsCache.Created)
                Application.PluginsCache.Create(SVNManager.PLUGINS_URL, true);
            else
                Application.PluginsCache.Load();

            Application.CreateReferencesCache();
        }

        private static void CreateReferencesCache()
        {
            if (!Application.ReferencesCache.Created)
            {
                Dictionary<string, string> list = SVNManager.Instance.List(SVNManager.REFERENCES_URL);

                var references = from reference in list.Keys
                                 where
                                    !reference.Equals("Benchmark") &&
                                    !reference.Equals("BudgetOnline") &&
                                    !reference.Equals("Component.Data") &&
                                    !reference.Equals("Component.Flow") &&
                                    !reference.Equals("Component.Messaging") &&
                                    !reference.Equals("Component.Scripting") &&
                                    !reference.Equals("Component.Sheet") &&
                                    !reference.Equals("Component.UI") &&
                                    !reference.Equals("Composer") &&
                                    !reference.Equals("CoreBusiness") &&
                                    !reference.Equals("DownloaderService") &&
                                    !reference.Equals("ForrestGump") &&
                                    !reference.Equals("FurnitureBuilderLite") &&
                                    !reference.Equals("InternalProducts") &&
                                    !reference.Equals("Manager") &&
                                    !reference.Equals("Portal") &&
                                    !reference.Equals("Procad.Downloader") &&
                                    !reference.Equals("ProductsManager") &&
                                    !reference.Equals("Promob.FurnitureBuiderLite") &&
                                    !reference.Equals("Promob.ManagerOne") &&
                                    !reference.Equals("PromobDataServices") &&
                                    !reference.Equals("PromobManager") &&
                                    !reference.Equals("PromobSW") &&
                                    !reference.Equals("Publisher4") &&
                                    !reference.Equals("SpecialRawMaterials") &&
                                    !reference.Equals("WebCatalog")
                                 select reference;

                List<string> urls = new List<string>();

                foreach (string url in references)
                    urls.Add(string.Format("{0}/{1}/Current", SVNManager.REFERENCES_URL, url));

                Application.ReferencesCache.Create(urls, true, true);
            }
            else
                Application.ReferencesCache.Load();
        }

        #endregion

        #region Public Methods

        public static void Initialize()
        {
            Application.InitializeManagers();
            Application.CreateCaches();
        }

        public static void Terminate()
        {
            Application.TerminateManagers();
        }

        #endregion
    }
}
