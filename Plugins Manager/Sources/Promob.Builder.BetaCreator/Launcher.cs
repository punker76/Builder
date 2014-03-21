using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Promob.Builder.Betas;
using Promob.Builder.Options;
using Promob.Builder.Translation;
using System.Windows.Forms;

namespace Promob.Builder.BetaCreator
{
    public static class Launcher
    {
        #region Consts

        public static readonly string APPLICATIONDATA_PATH = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Beta Creator");
        public static readonly string LOCAL_PATH = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
        public static readonly string OPTIONS_PATH = System.IO.Path.Combine(APPLICATIONDATA_PATH, "Options");
        public static readonly string SERVER_UPDATE_PATH = @"S:\Utilitários\Beta Creator";
        public static readonly string TRANSLATIONS_PATH = System.IO.Path.Combine(APPLICATIONDATA_PATH, "Translations");

        #endregion

        #region Private Methods

        private static void CreateResourceFiles()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string applicationPath =
                Path.Combine(
                    Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData), "Beta Creator");

            string[] directories =
                new string[]
                        { 
                            applicationPath + @"\Options",
                            applicationPath + @"\Translations" 
                        };

            foreach (string directory in directories)
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

            Stream resourceStream = assembly.GetManifestResourceStream("Promob.Builder.BetaCreator.Resources.Options.GeneralOptions.options");

            using (StreamReader generalOptionsStreamReader = new StreamReader(resourceStream))
            {
                if (!File.Exists(applicationPath + @"\Options\GeneralOptions.options"))
                    using (StreamWriter file = new StreamWriter(applicationPath + @"\Options\GeneralOptions.options"))
                        file.Write(generalOptionsStreamReader.ReadToEnd());
            }

            foreach (string resourceName in assembly.GetManifestResourceNames())
            {
                if (resourceName.Contains("Resources.Translations"))
                {
                    int length = "Promob.Builder.BetaCreator.Resources.Translations.".Length;
                    string resourceFileName = resourceName.Substring(length);
                    string fileDestinyName = applicationPath + @"\Translations\" + resourceFileName;

                    if (!File.Exists(fileDestinyName))
                    {
                        using (StreamWriter file = new StreamWriter(fileDestinyName))
                            using (StreamReader translationFile = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
                                file.Write(translationFile.ReadToEnd());
                    }
                }
            }
        }

        private static void ValidateApplicationPath()
        {
            if (Application.ExecutablePath.Contains(Launcher.SERVER_UPDATE_PATH))
            {
                MessageBox.Show("Não é permitido o acesso a partir do servidor, favor copiar para uma pasta local.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Process.GetCurrentProcess().Kill();
            }
        }

        #endregion

        #region Public Methods

        public static void Initialize()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Launcher));
            OptionsManager.GetManager().Initialize(Launcher.OPTIONS_PATH, assembly);
            int currentLanguage = (OptionsManager.GetManager().OptionsCollection["GeneralOptions"] as GeneralOptions).Language;
            TranslationManager.GetManager().Initialize(Launcher.TRANSLATIONS_PATH, currentLanguage);
        }

        [STAThread]
        public static void Main()
        {
            Launcher.ValidateApplicationPath();

            string updaterPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Promob.Builder.Updater.exe");

            if (File.Exists(updaterPath))
            {
                string executableFileName = Path.GetFileName(Application.ExecutablePath);
                string STEPONE = "STEPONE";

                ProcessStartInfo psi = new ProcessStartInfo(updaterPath);
                psi.Arguments =
                    string.Format(@"{0} ""{1}"" ""{2}"" ""{3}"" ""{4}""",
                            STEPONE,
                            Launcher.SERVER_UPDATE_PATH,
                            Launcher.LOCAL_PATH,
                            executableFileName,
                            Process.GetCurrentProcess().Id);

                Process updateProcess = new Process();
                updateProcess.StartInfo = psi;
                updateProcess.Start();
                updateProcess.WaitForExit();
            }

            Launcher.CreateResourceFiles();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Launcher.Initialize();
            Application.Run(new BetaDataEditorForm(new BetaData(null), false));
        }

        #endregion
    }
}
