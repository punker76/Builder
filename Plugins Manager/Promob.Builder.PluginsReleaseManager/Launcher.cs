using System;
using Promob.Builder.PluginsManagement.FrontEnd;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Promob.Builder.PluginsReleaseManager
{
    static class Launcher
    {
        #region Constants

        public static readonly string LOCAL_PATH = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
        public static readonly string SERVER_UPDATE_PATH = @"S:\Utilitários\Plugins Release Manager";

        #endregion

        #region Private Methods

        private static void CreateResourceFiles()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string applicationPath =
                System.IO.Path.Combine(
                    System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.ApplicationData), "Plugins Release Manager");

            string[] directories =
                new string[]
                {
                    applicationPath + @"\Cache",
                    applicationPath + @"\Options",
                    applicationPath + @"\Translations" 
                };

            foreach (string directory in directories)
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

            var teste = assembly.GetManifestResourceNames();

            foreach (string resourceName in assembly.GetManifestResourceNames())
            {
                if (resourceName.Contains("Resources.Cache"))
                {
                    int length = "Promob.Builder.PluginsReleaseManager.Resources.Cache.".Length;
                    string resourceFileName = resourceName.Substring(length);
                    string fileDestinyName = applicationPath + @"\Cache\" + resourceFileName;

                    if (!File.Exists(fileDestinyName))
                    {
                        using (StreamWriter file = new StreamWriter(fileDestinyName))
                        using (StreamReader translationFile = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
                            file.Write(translationFile.ReadToEnd());
                    }
                }

                if (resourceName.Contains("Resources.Options"))
                {
                    int length = "Promob.Builder.PluginsReleaseManager.Resources.Options.".Length;
                    string resourceFileName = resourceName.Substring(length);
                    string fileDestinyName = applicationPath + @"\Options\" + resourceFileName;

                    if (!File.Exists(fileDestinyName))
                    {
                        using (StreamWriter file = new StreamWriter(fileDestinyName))
                        using (StreamReader translationFile = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
                            file.Write(translationFile.ReadToEnd());
                    }
                }

                if (resourceName.Contains("Resources.Translations"))
                {
                    int length = "Promob.Builder.PluginsReleaseManager.Resources.Translations.".Length;
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

        [STAThread]
        static void Main()
        {
            Launcher.ValidateApplicationPath();

            string updaterPath = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "Promob.Builder.Updater.exe");

            if (File.Exists(updaterPath))
            {
                string STEPONE = "STEPONE";
                string executableFileName = Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);

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

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            Launcher.CreateResourceFiles();

            Promob.Builder.PluginsManagement.Application.Initialize();

            System.Windows.Forms.Application.Run(new MainForm());

            Promob.Builder.PluginsManagement.Application.Terminate();
            System.Windows.Forms.Application.ExitThread();
        }

        #endregion
    }
}
