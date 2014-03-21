using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Promob.Builder.SVN;

namespace Promob.Builder.PluginsManagement.BackEnd.Publish
{
    public class PublishingVersionInfoEventArgs
    {
        #region Attributes and Properties

        private PublishingStatus _status;
        public PublishingStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion

        #region Constructors

        public PublishingVersionInfoEventArgs(PublishingStatus status)
        {
            this._status = status;
        }

        #endregion
    }

    public delegate void PublishingVersionInfoEventHandler(object sender, PublishingVersionInfoEventArgs args);

    public class PublishingVersionInfo : INotifyPropertyChanged
    {
        #region Attributes and Properties

        private ImageList _imageList;
        public ImageList ImageList
        {
            get
            {
                if (this._imageList == null)
                {
                    this._imageList = new ImageList();
                    this._imageList.ImageSize = new Size(16, 16);
                    this._imageList.ColorDepth = ColorDepth.Depth32Bit;
                    this._imageList.Images.Add(Properties.Resources._16x16_exclamation);
                    this._imageList.Images.Add(Properties.Resources._16x16_tick);
                    this._imageList.Images.Add(Properties.Resources._16x16_cross);
                }

                return _imageList;
            }
        }

        private int _imageIndex = 0;
        public int ImageIndex
        {
            get { return _imageIndex; }
            set
            {
                _imageIndex = value;
                this.Image = this.ImageList.Images[value];
                this.NotifyPropertyChanged("ImageIndex");
            }
        }

        private Image _image;
        public Image Image
        {
            get
            {
                this._image = this.ImageList.Images[this._imageIndex];
                return _image;
            }
            set
            {
                _image = value;
                this.NotifyPropertyChanged("Image");
            }
        }

        public string PluginName
        {
            get
            {
                return this.Version.Product.Name;
            }
        }

        private PluginVersion _version;
        public PluginVersion Version
        {
            get { return _version; }
            set
            {
                _version = value;
                this.NotifyPropertyChanged("Version");
            }
        }

        private string _releaseVersion;
        public string ReleaseVersion
        {
            get
            {
                if (string.IsNullOrEmpty(this._releaseVersion))
                    this._releaseVersion = this.CalculateReleaseVersion();

                return _releaseVersion;
            }
            set
            {
                if (!this.ValidateReleaseVersion(value))
                    throw new System.ArgumentException("InvalidReleaseVersion");

                _releaseVersion = value;

                if (this.Type == PublishingType.Release)
                    this.SetReleaseAsPatch();

                this.NotifyPropertyChanged("ReleaseVersion");
            }
        }

        private void SetReleaseAsPatch()
        {
            string url = string.Format("{0}{1}", this.Version.Url.Substring(0, this.Version.Url.IndexOf(this.Version.VersionNumber)), this._releaseVersion);

            Info info = SVNManager.Instance.Info(url);

            if (info != null)
                this._releaseAsPatch = true;
        }

        private bool _releaseAsPatch;
        public bool ReleaseAsPatch
        {
            get { return _releaseAsPatch; }
        }

        private PublishingStatus _status;
        public PublishingStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                this.NotifyPropertyChanged("Status");
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                this.NotifyPropertyChanged("Error");
            }
        }

        private PublishingType _type;
        public PublishingType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private bool _createPatch = true;
        public bool CreatePatch
        {
            get { return _createPatch; }
            set
            {
                if (this.Version.VersionNumber.Equals("Current"))
                    throw new System.InvalidOperationException("CurrentAlwaysCreateRelease");

                _createPatch = value;
                this.NotifyPropertyChanged("CreatePatch");
            }
        }

        #endregion

        #region Constructors

        public PublishingVersionInfo(PluginVersion version)
        {
            this._version = version;

            this.SetStatus(PublishingStatus.Waiting, string.Empty);

            if (version.VersionNumber.Equals("Current"))
                this._type = PublishingType.Release;
            else
                this._type = PublishingType.Patch;
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        public event PublishingVersionInfoEventHandler StatusChanged;

        #endregion

        #region Private Methods

        private string CalculateReleaseVersion()
        {
            if (this.Version.MajorVersion == 0 && this.Version.MinorVersion == 0 && this.Version.BuildNumber == 0)
            {
                string url = string.Format("{0}/{1}", SVNManager.VERSIONS_URL, this.Version.Product.Name);
                List<PluginVersion> versions = new List<PluginVersion>();

                Dictionary<string, string> list = SVNManager.Instance.List(url);

                foreach (string name in list.Keys)
                    versions.Add(new PluginVersion(this.Version.Product as Plugin, name, this.Version.Path, this.Version.Url));

                PluginVersion lastestVersion = (from version in versions
                                                where !version.VersionNumber.Equals("Current") && !version.VersionNumber.StartsWith("Beta")
                                                orderby version.MajorVersion descending, version.MinorVersion descending, version.BuildNumber descending
                                                select version).FirstOrDefault();

                if (lastestVersion != null)
                    return string.Format("{0}.{1}.{2}", lastestVersion.MajorVersion, lastestVersion.MinorVersion, lastestVersion.BuildNumber + 1);
                else
                    return "1.0.0";
            }
            else
                return string.Format("{0}.{1}.{2}", this.Version.MajorVersion, this.Version.MinorVersion, this.Version.BuildNumber + 1);
        }

        private bool ValidateReleaseVersion(string name)
        {
            PluginVersion version = new PluginVersion(this.Version.Product as Plugin, name, this.Version.Path, this.Version.Url);

            if (version.MajorVersion == 0 && version.MinorVersion == 0 && version.BuildNumber == 0)
                return false;
            else if ((version.MajorVersion >= this.Version.MajorVersion && version.MinorVersion > this.Version.MinorVersion) ||
                    (version.MinorVersion == this.Version.MinorVersion && version.BuildNumber >= this.Version.BuildNumber))
                return true;
            else
                return false;
        }

        public void SetStatus(PublishingStatus status, string message)
        {
            this.Status = status;
            this.Message = message;

            switch (status)
            {
                case PublishingStatus.Waiting:
                    this.ImageIndex = 0;
                    break;
                case PublishingStatus.Publishing:
                    break;
                case PublishingStatus.Succeeded:
                    this.ImageIndex = 1;
                    break;
                case PublishingStatus.Failed:
                    this.ImageIndex = 2;
                    break;
                default:
                    break;
            }

            this.OnStatusChanged(status);
        }

        private void NotifyPropertyChanged(string name)
        {
            //TODO: Tratar por invoke
            try
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("{0} - {1}", ex.Message, name));
            }
        }

        private void OnStatusChanged(PublishingStatus status)
        {
            if (StatusChanged != null)
                StatusChanged(this, new PublishingVersionInfoEventArgs(status));
        }

        #endregion
    }
}
