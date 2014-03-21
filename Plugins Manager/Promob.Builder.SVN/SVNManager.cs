using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using ForrestGump.ConfigurationManagement;
using StepRunner;
using System.Reflection;
using System.Drawing;

namespace Promob.Builder.SVN
{
    public class SVNManager
    {
        #region Static Consts

        public static readonly string DEV_URL = @"http://servidor/svn/Desenvolvimento";
        public static readonly string PLUGINS_URL = @"http://servidor/svn/Desenvolvimento/Plugins";
        public static readonly string VERSIONS_URL = @"http://servidor/svn/Desenvolvimento/Version";
        public static readonly string REFERENCES_URL = @"http://servidor/svn/Desenvolvimento/References";

        public static readonly string DEV_PATH =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Dev");

        public static readonly string VERSIONS_PATH =
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), @"Dev\Version");

        public static string SVN_PATH = string.Format(@"{0}\{1}\{2}", System.Environment.CurrentDirectory, "Libs", "svn.exe");


        #endregion

        #region Singleton

        private SVNManager()
        { }

        private static SVNManager _instance;
        public static SVNManager Instance
        {
            get
            {
                if (SVNManager._instance == null)
                {
                    SVNManager._instance = new SVNManager();

                    SVNManager.GetFilesFromResource();

                    Workspace.GetWorkspace().Add("DEV_REPOSITORY", SVNManager.DEV_URL);
                    Workspace.GetWorkspace().Add("EXPORTED_VERSION_REPOSITORY", SVNManager.VERSIONS_PATH);
                    var path = System.IO.Path.GetDirectoryName(SVNManager.SVN_PATH);
                    ForrestGump.ConfigurationManagement.SVNSettings.SVNBINFOLDER = path;
                }

                return SVNManager._instance;
            }
        }

        #endregion

        #region Attributes and Properties

        public bool IsSvnRepositoryOnline
        {
            get { return true; }
        }

        public bool IsSvnInstalled
        {
            get { return true; }
        }

        private Console _console;
        public Console Console
        {
            get
            {
                if (this._console == null)
                    this._console = new Console();

                return _console;
            }
        }

        private Dictionary<string, string> _directories = null;
        private bool _listFiles = true;
        private string _url = string.Empty;

        #endregion

        #region Private Methods

        private bool ExecuteStep(CommonStep step)
        {
            try
            {
                step.Execute(this.Console);
            }
            catch (System.Exception ex)
            {
                this.Console.Write(string.Format("{0}: {1}", step.Description, ex.Message));
                return false;
            }

            return true;
        }

        private void List_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;

            string line = e.Data;

            bool isDirectory = line[line.Length - 1].Equals('/');

            if (!this._listFiles && !isDirectory)
                return;

            if (isDirectory)
                line = line.Remove(line.Length - 1);

            this._directories.Add(line, string.Format("{0}/{1}", this._url, line));

            //Debug.WriteLine("LIST -> " + line);
            //Debug.Flush();
        }

        #endregion

        #region Public Methods

        public string CreateBeta(string product, string version)
        {
            System.DateTime releaseDate = System.DateTime.Now;
            bool branchCreated = false;
            bool projectVersionEdited = false;
            bool dllHashCreated = false;

            CreateBranch createBranch = new CreateBranch();

            createBranch.Description = "Isolando arquivos";
            createBranch.Dll = "ForrestGump.ConfigurationManagement.dll";
            createBranch.Class = "ForrestGump.ConfigurationManagement.CreateBranch";
            createBranch.NewVersion = string.Format("Beta_{0}_{1}_{2}", product, version, releaseDate.ToString("dd.MM.yy_HH.mm"));
            createBranch.DevRepository = SVNManager.DEV_URL;
            createBranch.Product = product;
            createBranch.Version = version;
            createBranch.MappingFile = "beta.mapping";

            branchCreated = this.ExecuteStep(createBranch);

            string path = Path.Combine(SVNManager.VERSIONS_PATH, product);

            this.Update(path);


            EditProjectVersion editProjectVersion = new EditProjectVersion();

            editProjectVersion.Description = "Alterando atributos de versão (project.version)";
            editProjectVersion.Dll = "ForrestGump.ConfigurationManagement.dll";
            editProjectVersion.Class = "ForrestGump.ConfigurationManagement.EditProjectVersion";
            editProjectVersion.DevRepository = SVNManager.DEV_URL;
            editProjectVersion.Product = product;
            editProjectVersion.Version = createBranch.NewVersion;
            editProjectVersion.Status = "Isolado";
            editProjectVersion.Released = releaseDate.ToString("dd.MM.yy HH:mm");
            editProjectVersion.Beta = true;

            projectVersionEdited = this.ExecuteStep(editProjectVersion);

            CreateDLLHash createDLLHash = new CreateDLLHash();

            createDLLHash.Description = "Criando Hash das DLLs";
            createDLLHash.Dll = "ForrestGump.ConfigurationManagement.dll";
            createDLLHash.Class = "ForrestGump.ConfigurationManagement.CreateDLLHash";
            createDLLHash.DevRepository = SVNManager.DEV_URL;
            createDLLHash.Product = product;
            createDLLHash.Version = createBranch.NewVersion;

            dllHashCreated = this.ExecuteStep(createDLLHash);

            if (branchCreated && projectVersionEdited && dllHashCreated)
                return createBranch.NewVersion;
            else
                return string.Empty;
        }

        public bool DeleteBeta(string product, string version, string betaName)
        {
            string path = string.Format(
                @"{0}\{1}\{2}\{3}",
                SVNManager.VERSIONS_PATH,
                product,
                version,
                "beta.mapping");

            SVNManager.Instance.Update(path);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XmlElement docElement = xmlDocument.DocumentElement;

            foreach (XmlNode child in docElement.ChildNodes)
            {
                if (!(child is XmlElement))
                    continue;

                if (child.Name.Equals("ExportRemoteMapping"))
                {
                    XmlAttribute attributeSource = child.Attributes["Source"];

                    string url = string.Format(
                                    @"{0}/{1}/{2}/{3}",
                                    SVNManager.DEV_URL,
                                    attributeSource.Value,
                                    product,
                                    betaName);

                    SVNManager.Instance.Delete(url);
                }
            }

            return false;
        }

        public string DownloadRelease(string product, string version)
        {
            string path = @"C:\Temp";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            Map map = new Map();

            map.Description = "Sincronizar versão local";
            map.Dll = "ForrestGump.ConfigurationManagement.dll";
            map.Class = "ForrestGump.ConfigurationManagement.Map";
            map.MappingFile = "beta.mapping";
            map.DevRepository = SVNManager.DEV_URL;
            map.DevFolder = path;
            map.Product = product;
            map.Version = version;

            if (this.ExecuteStep(map))
                return path;

            return string.Empty;
        }

        public bool CreateRelease(string product, string releaseVersion)
        {
            bool branchCreated = false;
            bool projectVersionEdited = false;

            System.DateTime releaseDate = System.DateTime.Now;

            CreateBranch createBranch = new CreateBranch();

            createBranch.Description = "Isolando arquivos";
            createBranch.Dll = "ForrestGump.ConfigurationManagement.dll";
            createBranch.Class = "ForrestGump.ConfigurationManagement.CreateBranch";
            createBranch.NewVersion = releaseVersion;
            createBranch.DevRepository = SVNManager.DEV_URL;
            createBranch.Product = product;
            createBranch.Version = "Current";
            createBranch.MappingFile = "default.mapping";

            branchCreated = this.ExecuteStep(createBranch);

            EditProjectVersion editProjectVersion = new EditProjectVersion();

            editProjectVersion.Description = "Alterando atributos de versão (project.version)";
            editProjectVersion.Dll = "ForrestGump.ConfigurationManagement.dll";
            editProjectVersion.Class = "ForrestGump.ConfigurationManagement.EditProjectVersion";
            editProjectVersion.DevRepository = SVNManager.DEV_URL;
            editProjectVersion.Product = product;
            editProjectVersion.Version = createBranch.NewVersion;
            editProjectVersion.Status = "Liberado";
            editProjectVersion.Released = releaseDate.ToString("dd.MM.yy HH:mm");
            editProjectVersion.Beta = false;

            bool success = projectVersionEdited = this.ExecuteStep(editProjectVersion);

            if (success)
                SVNManager.Instance.Update(string.Format(@"{0}\{1}", SVNManager.VERSIONS_PATH, product));

            return success;
        }

        public bool CreatePatch(string product, string version, string releaseVersion)
        {
            bool branchCreated = false;
            bool projectVersionEdited = false;
            bool dllHashCreated = false;

            System.DateTime releaseDate = System.DateTime.Now;

            CreateBranch createBranch = new CreateBranch();

            createBranch.Description = "Isolando arquivos";
            createBranch.Dll = "ForrestGump.ConfigurationManagement.dll";
            createBranch.Class = "ForrestGump.ConfigurationManagement.CreateBranch";
            createBranch.NewVersion = releaseVersion;
            createBranch.DevRepository = SVNManager.DEV_URL;
            createBranch.Product = product;
            createBranch.Version = version;
            createBranch.MappingFile = "default.mapping";

            branchCreated = this.ExecuteStep(createBranch);

            EditProjectVersion editProjectVersion = new EditProjectVersion();

            editProjectVersion.Description = "Alterando atributos de versão (project.version)";
            editProjectVersion.Dll = "ForrestGump.ConfigurationManagement.dll";
            editProjectVersion.Class = "ForrestGump.ConfigurationManagement.EditProjectVersion";
            editProjectVersion.DevRepository = SVNManager.DEV_URL;
            editProjectVersion.Product = product;
            editProjectVersion.Version = createBranch.NewVersion;
            editProjectVersion.Status = "Desenvolvimento";
            editProjectVersion.Released = releaseDate.ToString("dd.MM.yy HH:mm");
            editProjectVersion.Beta = false;

            projectVersionEdited = this.ExecuteStep(editProjectVersion);

            CreateDLLHash createDLLHash = new CreateDLLHash();

            createDLLHash.Description = "Criando Hash das DLLs";
            createDLLHash.Dll = "ForrestGump.ConfigurationManagement.dll";
            createDLLHash.Class = "ForrestGump.ConfigurationManagement.CreateDLLHash";
            createDLLHash.DevRepository = SVNManager.DEV_URL;
            createDLLHash.Product = product;
            createDLLHash.Version = createBranch.NewVersion;

            dllHashCreated = this.ExecuteStep(createDLLHash);

            bool success = branchCreated && projectVersionEdited && dllHashCreated;

            if (success)
                SVNManager.Instance.Update(string.Format(@"{0}\{1}", SVNManager.VERSIONS_PATH, product));

            return success;
        }

        public bool ReleasePatch(string product, string version)
        {
            System.DateTime releaseDate = System.DateTime.Now;

            EditProjectVersion editProjectVersion = new EditProjectVersion();

            editProjectVersion.Description = "Alterando atributos de versão (project.version)";
            editProjectVersion.Dll = "ForrestGump.ConfigurationManagement.dll";
            editProjectVersion.Class = "ForrestGump.ConfigurationManagement.EditProjectVersion";
            editProjectVersion.DevRepository = SVNManager.DEV_URL;
            editProjectVersion.Product = product;
            editProjectVersion.Version = version;
            editProjectVersion.Status = "Liberado";
            editProjectVersion.Released = releaseDate.ToString("dd.MM.yy HH:mm");
            editProjectVersion.Beta = false;

            return this.ExecuteStep(editProjectVersion);
        }

        public bool Lock(string url)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("lock \"{0}\" --force", url);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            if (process.StandardError.Peek() != -1)
                return false;

            return true;
        }

        public bool Unlock(string url)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("unlock \"{0}\"", url);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            if (process.StandardError.Peek() != -1)
            {
                while (process.StandardError.Peek() > -1)
                {
                    this.Console.Write(process.StandardError.ReadLine());
                }
                return false;
            }

            return true;
        }

        public bool Add(string path)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("add \"{0}\"", path);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            if (process.StandardError.Peek() != -1)
                return false;

            return true;
        }

        public bool Delete(string url)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("delete \"{0}\" -m \"{1}\" --force", url, "Deleted");

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            if (process.StandardError.Peek() != -1)
            {
                while (process.StandardError.Peek() > -1)
                    this.Console.Write(process.StandardError.ReadLine());

                return false;
            }

            return true;
        }

        public bool Commit(string path, string message)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("commit \"{0}\" -m \"{1}\"", path, message);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            if (process.StandardError.Peek() != -1)
            {
                while (process.StandardError.Peek() > -1)
                    this.Console.Write(process.StandardError.ReadLine());

                return false;
            }

            return true;
        }

        public Info Info(string url)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("info --xml \"{0}\"", url);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            if (process.StandardError.Peek() != -1)
                return null;

            Info ret = null;

            if (process.StandardOutput.Peek() != -1)
            {
                XmlDocument document = null;
                string xml = process.StandardOutput.ReadToEnd();
                document = new XmlDocument();
                document.LoadXml(xml);

                ret = SVN.Info.Load(document);
            }

            return ret;
        }

        public string LastRevision(string url)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("info \"{0}\"", url);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            string ret = string.Empty;

            if (process.StandardError.Peek() != -1)
                ret = string.Empty;
            else
            {
                while (process.StandardOutput.Peek() > -1)
                {
                    string line = process.StandardOutput.ReadLine();

                    if (line.Contains("Last Changed Rev: "))
                        ret = line.Substring(18);
                    else
                        continue;
                }
            }

            return ret;
        }

        public bool Exists(string url)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("info {0}", url);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            bool success = false;

            using (StreamReader errorStream = process.StandardError)
                if (errorStream.Peek() == -1)
                    success = true;

            return success;
        }

        public Dictionary<string, string> List(string url)
        {
            return this.List(url, true);
        }

        public Dictionary<string, string> List(string url, bool listFiles)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            this._directories = new Dictionary<string, string>();
            this._listFiles = listFiles;
            this._url = url;
            string argument = string.Format("list \"{0}\"", url);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process process = new Process();

            process.OutputDataReceived += List_OutputDataReceived;

            process.StartInfo = startInfo;
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            process.OutputDataReceived -= List_OutputDataReceived;

            if (process.StandardError.Peek() != -1)
                return null;

            return this._directories;
        }

        public bool Update(string path)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("update \"{0}\"", path);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            bool success = false;

            using (StreamReader errorStream = process.StandardError)
                if (errorStream.Peek() == -1)
                    success = true;
                else
                {
                    while (errorStream.Peek() > 0)
                    {
                        string line = errorStream.ReadLine();

                        this.Console.Write(line);
                    }
                }

            using (StreamReader outputStream = process.StandardOutput)
            {
                while (outputStream.Peek() > 0)
                {
                    string line = outputStream.ReadLine();

                    this.Console.Write(line);
                }
            }


            return success;
        }

        public bool Checkout(string path, string url)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("checkout \"{0}\" \"{1}\"", url, path);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            bool success = false;

            using (StreamReader errorStream = process.StandardError)
                if (errorStream.Peek() == -1)
                    return true;
                else
                    while (errorStream.Peek() > -1)
                        this.Console.Write(errorStream.ReadLine());

            return success;
        }

        public bool Import(string path, string url)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string message = "Importing";

            string argument = string.Format("import -q \"{0}\" \"{1}\" -m \"{2}\"", path, url, message);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            bool success = false;

            using (StreamReader errorStream = process.StandardError)
                if (errorStream.Peek() == -1)
                    return true;
                else
                    while (errorStream.Peek() > -1)
                    {
                        this.Console.Write(errorStream.ReadLine());
                    }

            return success;
        }

        public bool Export(string url, string path)
        {
            if (!this.IsSvnInstalled)
                throw new System.InvalidOperationException("Snv must be running.");

            if (!this.IsSvnRepositoryOnline)
                throw new System.InvalidOperationException("Svn must be online.");

            string argument = string.Format("export \"{0}\" \"{1}\" --force", url, path);

            ProcessStartInfo startInfo =
                new ProcessStartInfo(SVNManager.SVN_PATH, argument);

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            bool success = false;

            using (StreamReader errorStream = process.StandardError)
                if (errorStream.Peek() == -1)
                    success = true;
                else
                    while (errorStream.Peek() > -1)
                    {
                        this.Console.Write(errorStream.ReadLine());
                    }

            return success;
        }

        public string Download(string url)
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            if (SVNManager.Instance.Export(url, path))
                return path;

            return "";
        }

        #endregion

        private static void GetFilesFromResource()
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (string resource in stream)
            {
                string fileName = resource.Remove(0, 24);

                if (resource.Contains("Libs"))
                {
                    Stream fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
                    SaveStreamToFile(fileStream, fileName);
                }
            }

            RootSaveStreamToFile(Assembly.GetExecutingAssembly().GetManifestResourceStream("Promob.Builder.SVN.SVN.dll"), "SVN.dll");
            RootSaveStreamToFile(Assembly.GetExecutingAssembly().GetManifestResourceStream("Promob.Builder.SVN.ForrestGump.SVN.dll"), "ForrestGump.SVN.dll");
        }

        public static void SaveStreamToFile(Stream stream, string filename)
        {
            if (!Directory.Exists("Libs"))
                Directory.CreateDirectory("Libs");

            using (Stream destination = File.Create(@"Libs\" + filename))
                Write(stream, destination);
        }

        public static void Write(Stream from, Stream to)
        {
            for (int a = from.ReadByte(); a != -1; a = from.ReadByte())
                to.WriteByte((byte)a);
        }

        public static void RootSaveStreamToFile(Stream stream, string filename)
        {
            using (Stream destination = File.Create(filename))
                Write(stream, destination);
        }
    }
}
