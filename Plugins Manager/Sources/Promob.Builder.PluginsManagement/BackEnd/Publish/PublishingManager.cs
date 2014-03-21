using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Promob.Builder.Options;
using Promob.Builder.PluginsManagement.BackEnd.Options;
using Promob.Builder.SVN;
using Promob.Builder.Translation;

namespace Promob.Builder.PluginsManagement.BackEnd.Publish
{
    public class PublishingException : Exception
    {
        #region Constructors

        public PublishingException(string message)
            : base(message)
        {

        }

        #endregion
    }

    public class PublishingManager
    {
        #region Attributes and Properties

        public PublishOptions PublishOptions
        {
            get
            {
                return OptionsManager.GetManager().OptionsCollection["PublishOptions"] as PublishOptions;
            }
        }

        private BackgroundWorker _publisherWorker;
        private BackgroundWorker PublisherWorker
        {
            get { return _publisherWorker; }
            set { _publisherWorker = value; }
        }

        private bool _publishing;
        public bool Publishing
        {
            get { return _publishing; }
        }

        private bool _canceled;
        private bool _saveLocalMediaCopy;
        private string _localMediaCopyPath;
        List<PublishingVersionInfo> _infos;
        private bool _completedWithErrors;
        #endregion

        #region Singleton

        private PublishingManager()
        { }

        private static PublishingManager _instance;
        public static PublishingManager Instance
        {
            get
            {
                if (PublishingManager._instance == null)
                {
                    PublishingManager._instance = new PublishingManager();
                    PublishingManager._instance.InitPublishingWorwer();
                }

                return PublishingManager._instance;
            }
        }

        #endregion

        #region Events

        public event RunWorkerCompletedEventHandler OnPublishCompleted;

        #endregion

        #region Events Methods

        private void PublisherWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this._infos = e.Argument as List<PublishingVersionInfo>;

            this._publishing = true;

            foreach (PublishingVersionInfo info in this._infos)
            {
                if (this._canceled)
                    break;

                this.Publish(info, e);
            }

            this._publishing = false;

            if (this._completedWithErrors)
                throw new PublishingException("Completed With Errors");
        }

        private void _publisherWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                this._canceled = false;

            if (OnPublishCompleted != null)
                OnPublishCompleted(this, e);
        }

        #endregion

        #region Private Methods

        private void InitPublishingWorwer()
        {
            this._publisherWorker = new BackgroundWorker();
            this._publisherWorker.WorkerSupportsCancellation = true;
            this._publisherWorker.WorkerReportsProgress = true;
            this._publisherWorker.DoWork += PublisherWorker_DoWork;
            this._publisherWorker.RunWorkerCompleted += _publisherWorker_RunWorkerCompleted;
        }

        private void Publish(PublishingVersionInfo info, DoWorkEventArgs e)
        {
            PluginVersion version = info.Version;
            string path = string.Empty;

            this.SetPublishingVersionStatus(info, PublishingStatus.Publishing);

            bool releaseExecuted = this.ExecuteRelease(info, version);

            if (!releaseExecuted)
            {
                this._completedWithErrors = true;
                return;
            }

            bool createdBeta = this.ExecuteCreateBeta(info, version, out path);

            if (!createdBeta)
            {
                this._completedWithErrors = true;
                return;
            }

            if (info.Type == PublishingType.Release)
                this.UpdateReleasedAsPatchPluginDataVersion(info, version, path);

            bool publisherExecuted = this.ExecutePublisher(info, e, version, path);

            if (!publisherExecuted)
            {
                this._completedWithErrors = true;
                return;
            }

            if (info.Type == PublishingType.Patch && info.CreatePatch)
            {
                bool patchCreated = this.ExecuteCreatePatch(info, version, info.ReleaseVersion);

                if (!patchCreated)
                {
                    this._completedWithErrors = true;
                    return;
                }

                this.UpdatePluginDataVersion(info, version);
            }
            else if (info.Type == PublishingType.Release && !info.ReleaseAsPatch)
                this.UpdatePluginDataVersion(info, version);

            this.SetPublishingVersionStatus(info, PublishingStatus.Succeeded);
        }

        private void UpdateReleasedAsPatchPluginDataVersion(PublishingVersionInfo info, PluginVersion version, string path)
        {
            string pluginDataPath = string.Format(@"{0}\Program\Plugins\{1}\plugin.data", path, version.PluginData.Id);

            if (System.IO.File.Exists(pluginDataPath))
            {
                PluginData pluginData = PluginData.Load(version, pluginDataPath);
                pluginData.Version = info.ReleaseVersion;
                pluginData.SaveLocal(pluginDataPath);
            }
        }

        private void UpdatePluginDataVersion(PublishingVersionInfo info, PluginVersion version)
        {
            string url = string.Format("{0}/{1}", version.Url.Substring(0, version.Url.IndexOf(version.VersionNumber) - 1), info.ReleaseVersion);

            PluginVersion newVersion = new PluginVersion(version.Product as Plugin, info.ReleaseVersion, string.Empty, url);
            newVersion.Load();
            newVersion.PluginData.Version = info.ReleaseVersion;
            newVersion.PluginData.Save();
        }

        private bool ExecuteCreateBeta(PublishingVersionInfo info, PluginVersion version, out string path)
        {
            string betaPath = Path.Combine(this.PublishOptions.BetasPath, string.Format("{0} - {1}", version.Product.Name, version.VersionNumber));

            if (System.IO.Directory.Exists(betaPath))
                Promob.Builder.IO.IOHelper.DeleteDirectory(betaPath);

            if (!System.IO.Directory.Exists(this.PublishOptions.BetasPath))
                System.IO.Directory.CreateDirectory(this.PublishOptions.BetasPath);

            path = version.CreateBeta(this.PublishOptions.BetasPath).Trim();

            if (string.IsNullOrEmpty(path))
            {
                this.SetPublishingVersionStatus(
                    info,
                    PublishingStatus.Failed,
                    string.Format("Failed to create beta: {0}.", info.ReleaseVersion));

                return false;
            }

            return true;
        }

        private bool ExecuteRelease(PublishingVersionInfo info, PluginVersion version)
        {
            if (info.Type == PublishingType.Release && info.ReleaseAsPatch)
                return true;
            else
                if (info.Type == PublishingType.Release &&
                    !SVNManager.Instance.CreateRelease(version.Product.Name, info.ReleaseVersion))
                {
                    this.SetPublishingVersionStatus(
                        info,
                        PublishingStatus.Failed,
                        string.Format("Failed to release new version: {0}.", info.ReleaseVersion));

                    return false;
                }
                else
                    if (info.Type == PublishingType.Patch &&
                        !SVNManager.Instance.ReleasePatch(version.Product.Name, info.Version.VersionNumber))
                    {
                        this.SetPublishingVersionStatus(
                                info,
                                PublishingStatus.Failed,
                                string.Format("Failed to create patch: {0}.", info.ReleaseVersion));

                        return false;
                    }

            this.UpdatePluginDataVersion(info, version);

            return true;
        }

        private bool ExecutePublisher(PublishingVersionInfo info, DoWorkEventArgs e, PluginVersion version, string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(this.PublishOptions.ExecutablePath);

            startInfo.Arguments = this.GetFormattedSilentPublishArgs(version, path);
            startInfo.WorkingDirectory = Path.GetDirectoryName(this.PublishOptions.ExecutablePath);
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            if (this._canceled)
            {
                e.Cancel = true;
                this.SetPublishingVersionStatus(info, PublishingStatus.Failed, "CANCELED.");
                return false;
            }

            string message = string.Empty;

            using (StreamReader errorStream = process.StandardError)
            {
                if (errorStream.Peek() > -1)
                {
                    message = errorStream.ReadToEnd();

                    if (message.Contains("CANCELED"))
                    {
                        e.Cancel = true;
                        this.Cancel(message);
                        return false;
                    }
                    else
                    {
                        this.SetPublishingVersionStatus(info, PublishingStatus.Failed, message);
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ExecuteCreatePatch(PublishingVersionInfo info, PluginVersion version, string releaseVersion)
        {
            bool createdPatch = SVNManager.Instance.CreatePatch(version.Product.Name, version.VersionNumber, releaseVersion);

            if (!createdPatch)
            {
                this.SetPublishingVersionStatus(
                    info,
                    PublishingStatus.Failed,
                    string.Format("Failed to create patch: {0}.", info.ReleaseVersion));

                return false;
            }

            return true;
        }

        private string GetFormattedSilentPublishArgs(PluginVersion version, string path)
        {
            string productId = version.PluginData.ProductId.ToString();

            string programPath = System.IO.Directory.Exists(Path.Combine(path, "Program")) ? Path.Combine(path, "Program") : string.Empty;
            string systemPath = System.IO.Directory.Exists(Path.Combine(path, "System")) ? Path.Combine(path, "System") : string.Empty;
            string catalogBuilderPath = System.IO.Directory.Exists(Path.Combine(path, "CatalogBuilder")) ? Path.Combine(path, "CatalogBuilder") : string.Empty;

            string installPath = this.PublishOptions.InstallPath;
            string email = this.PublishOptions.Email;

            string localMediaPath = this._saveLocalMediaCopy ? this._localMediaCopyPath : string.Empty;

            return string.Format(
                "-silentPublish {0} \"{1}\" \"{2}\" \"{3}\" \"{4}\" {5} {6} \"{7}\"",
                productId,
                programPath,
                systemPath,
                catalogBuilderPath,
                installPath,
                email,
                this._saveLocalMediaCopy,
                localMediaPath);
        }

        private void Cancel(string message)
        {
            if (this._publishing)
            {
                DialogResult dr = MessageBox.Show(
                    TranslationManager.GetManager().Translate("PublishingManager.Mbox_Caption.CancelAll"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                    this.Cancel(true, message);
            }
        }

        private void SetPublishingVersionStatus(PublishingVersionInfo info, PublishingStatus status, string error)
        {
            info.SetStatus(status, error);
        }

        private void SetPublishingVersionStatus(PublishingVersionInfo info, PublishingStatus status)
        {
            this.SetPublishingVersionStatus(info, status, string.Empty);
        }

        #endregion

        #region Public Methods

        public void Publish(List<PublishingVersionInfo> infos, bool saveLocalMediaCopy, string localMediaCopyPath)
        {
            this._saveLocalMediaCopy = saveLocalMediaCopy;
            this._localMediaCopyPath = localMediaCopyPath;
            this._canceled = false;
            this._completedWithErrors = false;
            this._publishing = true;
            this.PublisherWorker.RunWorkerAsync(infos);
        }

        public void Cancel(bool cancelAll, string message)
        {
            Process[] publisher = Process.GetProcessesByName("Publisher");
            Process[] uploader = Process.GetProcessesByName("Uploader");

            foreach (Process p in publisher)
                p.Kill();

            foreach (Process p in uploader)
                p.Kill();

            if (cancelAll)
            {
                this._canceled = true;
                foreach (PublishingVersionInfo info in this._infos)
                    this.SetPublishingVersionStatus(info, PublishingStatus.Failed, message);
            }
        }

        #endregion
    }
}
