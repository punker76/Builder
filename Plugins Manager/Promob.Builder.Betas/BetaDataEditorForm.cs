using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Promob.Builder.Core;
using Promob.Builder.Forms;
using Promob.Builder.Options;
using Promob.Builder.SVN;
using Promob.Builder.Translation;

namespace Promob.Builder.Betas
{
    public partial class BetaDataEditorForm : Form, ITranslatable
    {
        #region Constructors

        public BetaDataEditorForm(BetaData beta, bool isEditor)
        {
            this.Cursor = Cursors.WaitCursor;

            this.StartPosition = FormStartPosition.CenterScreen;

            this.BetaData = beta;
            this._isEditor = isEditor;

            this.InitializeComponent();

            if (this._isEditor)
                this.BetaData.UpdateCheckout(this.BetaData.Path, this.BetaData.Url);
            else
                this._newBetaDataFile = true;

            this.Init();

            this.Translate();

            this.Cursor = Cursors.Default;
        }

        #endregion

        #region Attributes and Properties

        private BetaData _betaData;
        public BetaData BetaData
        {
            get { return this._betaData; }
            set { this._betaData = value; }
        }

        private bool _newBetaDataFile = false;
        private bool NewBetaDataFile
        {
            get { return this._newBetaDataFile; }
            set { this._newBetaDataFile = value; }
        }

        private bool _hasChanges = false;
        private bool HasChanges
        {
            get { return this._hasChanges; }
            set { this._hasChanges = value; }
        }

        private bool _betaCreated = false;
        public bool BetaCreated
        {
            get { return this._betaCreated; }
            set { this._betaCreated = value; }
        }

        private bool _isEditor = false;

        #endregion

        #region Private Methods

        private void Init()
        {
            this.Load += BetaDataEditorForm_Load;
            TranslationManager.GetManager().OnCurrentLanguageChanged += TranslationManager_OnCurrentLanguageChanged;
        }

        private void InitControls()
        {
            if (this._isEditor)
                this.InitControlsWithParent();
            else
                this.InitControlsWithoutParent();

            this.UpdateControls();
        }

        private void InitControlsWithParent()
        {
            this.msMenu.Visible = false;
            this.tcBetaData.Location = new System.Drawing.Point(11, 12);
            this.tcBetaData.Size = new System.Drawing.Size(670, 404);
            this.lblValueName.Text = this.BetaData.Name;
            this.btnAction.Click += new System.EventHandler(this.bntAction_Save_Click);
        }

        private void InitControlsWithoutParent()
        {
            this.msMenu.Visible = true;
            this.tcBetaData.Location = new System.Drawing.Point(11, 35);
            this.tcBetaData.Size = new System.Drawing.Size(670, 381);
            this.lblValueName.Text = TranslationManager.GetManager().Translate("None");
            this.btnBrowse.Visible = false;
            this.btnAction.Click += this.bntAction_Create_Click;
            this._betaData.DependencyProducts.ListChanged += DependencyProducts_ListChanged;
        }

        private void UpdateControls()
        {
            this.btnUp.Enabled = this.lbSelected.Items.Count > 0;
            this.btnDown.Enabled = this.lbSelected.Items.Count > 0;
            this.btnRemove.Enabled = this.lbSelected.Items.Count > 0;
            this.tsbNewFile.Enabled = !this._newBetaDataFile;
            this.tsbSaveFile.Enabled = this._hasChanges && !this._newBetaDataFile;

            if (!string.IsNullOrEmpty(this.BetaData.Name))
                this.lblValueName.Text = this.BetaData.Name;
        }

        private void CheckVersions()
        {
            string url = SVNManager.VERSIONS_URL;
            Dictionary<string, string> products = SVNManager.Instance.List(url);

            foreach (string product in products.Keys)
            {
                string path = string.Format(@"{0}\{1}", SVNManager.VERSIONS_PATH, product);
                url = string.Format("{0}/{1}", url, product);

                if (System.IO.Directory.Exists(path))
                    SVNManager.Instance.Update(path);
                else
                    SVNManager.Instance.Checkout(path, url);
            }
        }

        private void LoadProducts()
        {
            this.Cursor = Cursors.WaitCursor;
            this.tvProducts.Nodes.Clear();
            this.lbSelected.DataSource = null;

            string url = SVNManager.VERSIONS_URL;
            Dictionary<string, string> list = SVNManager.Instance.List(url);

            IEnumerable<string> sortedProducts = null;

            if (this._isEditor)
            {
                sortedProducts = from product in list.Keys
                                 where !product.Equals("Promob") &&
                                       !product.Equals("Catalog") &&
                                       !product.Equals(this.BetaData.Version.Product.Name)
                                 select product;
            }
            else
            {
                sortedProducts = from product in list.Keys
                                 where !product.Equals("Promob") &&
                                       !product.Equals("Catalog")
                                 select product;
            }

            List<string> products = new List<string>();

            if (list.ContainsKey("Promob"))
                products.Add("Promob");

            if (list.ContainsKey("Catalog"))
                products.Add("Catalog");

            products.AddRange(sortedProducts);

            foreach (string name in products)
            {
                Product product = new Product(name);

                ProductTreeNode node = new ProductTreeNode(product);

                if (this.BetaData.DependencyProducts.Contains(product.Name))
                    this.LoadProductVersions(node);

                this.tvProducts.Nodes.Add(node);

                this.tvProducts.HideCheckBox(node);
            }

            this.Cursor = Cursors.Default;
        }

        private void LoadProductVersions(ProductTreeNode productNode)
        {
            List<Version> versions = this.LoadVersions(productNode.Product);
            List<Version> sortedVersions = this.SortVersions(versions);

            foreach (Version version in sortedVersions)
            {
                DependencyProduct dependencyProduct = this.LoadDependencyProduct(productNode.Product.Name, version);
                DependencyProductTreeNode node = new DependencyProductTreeNode(dependencyProduct);

                node.Checked = this.BetaData.DependencyProducts.Contains(productNode.Product.Name, version.VersionNumber);

                productNode.Nodes.Add(node);
            }

            productNode.ExpandAll();

            this.UpdateSelectedVersions();
        }

        private DependencyProduct LoadDependencyProduct(string product, Version version)
        {
            DependencyProduct dependencyProduct;

            bool contains = this.BetaData.DependencyProducts.Contains(product, version.VersionNumber);

            if (!contains)
                dependencyProduct = new DependencyProduct(product, version.VersionNumber);
            else
            {
                dependencyProduct =
                    this.BetaData.DependencyProducts.ToList().Find(
                        new System.Predicate<DependencyProduct>(p =>
                            p.Name.Equals(product) &&
                            p.Version.Equals(version.VersionNumber)));
            }

            return dependencyProduct;
        }

        private List<Version> LoadVersions(Product product)
        {
            string path = string.Format(@"{0}\{1}", SVNManager.VERSIONS_PATH, product);
            string url = string.Format("{0}/{1}", SVNManager.VERSIONS_URL, product);

            List<Version> versions = new List<Version>();

            if (!System.IO.Directory.Exists(path))
                SVNManager.Instance.Checkout(path, url);

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            foreach (DirectoryInfo versionDirInfo in dirInfo.GetDirectories())
            {
                foreach (FileInfo fileInfo in versionDirInfo.GetFiles("project.version"))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(fileInfo.FullName);

                    XmlAttribute status = xmlDocument.DocumentElement.Attributes["Status"];

                    if (status.Value.Equals("Desenvolvimento") ||
                        status.Value.Equals("Current") ||
                        status.Value.Equals("Liberado"))
                    {
                        Version newVersion = new Version(product, versionDirInfo.Name, string.Empty, string.Empty);

                        if (this._isEditor)
                        {
                            if (this.BetaData.Version.IsValidVersion(newVersion))
                                versions.Add(newVersion);
                        }
                        else
                            versions.Add(newVersion);
                    }
                }
            }

            return versions;
        }

        private bool CheckForChanges()
        {
            if (this.HasChanges)
            {
                DialogResult dr = MessageBox.Show(
                    TranslationManager.GetManager().Translate("BetaDataEditorForm.Mbox_Caption.HasChanges"),
                    TranslationManager.GetManager().Translate("Attention"),
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    if (this.NewBetaDataFile)
                        this.SaveAs();
                    else
                        this.Save();
                }
                else if (dr == DialogResult.Cancel)
                    return false;
            }

            return true;
        }

        private void NewFile()
        {
            this.BetaData = new BetaData();
            this.BetaData.DependencyProducts.ListChanged += DependencyProducts_ListChanged;
            this._newBetaDataFile = true;
            this._hasChanges = false;
            this.Cursor = Cursors.WaitCursor;
            this.LoadProducts();
            this.UpdateControls();
            this.Cursor = Cursors.Default;
        }

        private void OpenFile()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.CheckPathExists = true;
            openDialog.AddExtension = true;
            openDialog.AutoUpgradeEnabled = true;
            openDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyComputer);
            openDialog.DefaultExt = ".beta";
            openDialog.Filter = "BetaData Files (*.beta)|*.beta";
            openDialog.FilterIndex = 0;
            openDialog.Multiselect = false;

            DialogResult dr = openDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.BetaData = BetaData.Load(openDialog.FileName);
                this.BetaData.DependencyProducts.ListChanged += DependencyProducts_ListChanged;

                this._newBetaDataFile = false;
                this._hasChanges = false;

                this.Cursor = Cursors.WaitCursor;
                this.LoadProducts();
                this.UpdateControls();
                this.Cursor = Cursors.Default;
            }
        }

        private void SaveAs()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.CheckPathExists = true;
            saveDialog.AddExtension = true;
            saveDialog.AutoUpgradeEnabled = true;
            saveDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyComputer);
            saveDialog.DefaultExt = ".beta";
            saveDialog.Filter = "BetaData Files (*.beta)|*.beta";
            saveDialog.FilterIndex = 0;
            DialogResult dr = saveDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.BetaData.Name = System.IO.Path.GetFileNameWithoutExtension(saveDialog.FileName);
                this.BetaData.Path = saveDialog.FileName;
                this.BetaData.Save(saveDialog.FileName);
                this._newBetaDataFile = false;
                this._hasChanges = false;
                this.UpdateControls();
            }
        }

        private void Save()
        {
            this.BetaData.Save(this.BetaData.Path);
            this._newBetaDataFile = false;
            this._hasChanges = false;
            this.UpdateControls();
        }

        private Dictionary<DependencyProduct, string> CreateBeta(string path, out string message)
        {
            List<string> folders = new List<string>();
            Dictionary<DependencyProduct, string> betaNames = new Dictionary<DependencyProduct, string>();

            string name = this.BetaData.Name;
            string betaName = string.Empty;
            string folder = string.Empty;
            string destinationFolder = string.Empty;

            DependencyProducts dependencyProducts = this.BetaData.DependencyProducts;

            foreach (DependencyProduct dependencyproduct in dependencyProducts)
            {
                betaName = SVNManager.Instance.CreateBeta(dependencyproduct.Name, dependencyproduct.Version);

                if (string.IsNullOrEmpty(betaName))
                {
                    message = string.Format("{0} - {1}", TranslationManager.GetManager().Translate("Failed to create beta."), betaName);
                    return betaNames;
                }

                betaNames.Add(dependencyproduct, betaName);

                folder = SVNManager.Instance.DownloadRelease(dependencyproduct.Name, betaName);

                if (string.IsNullOrEmpty(folder))
                {
                    message = TranslationManager.GetManager().Translate("Failed to download the release");
                    return betaNames;
                }

                folders.Add(string.Format(@"{0}\{1}\{2}\Promob5", folder, dependencyproduct.Name, betaName));
            }

            foreach (string source in folders)
            {
                try
                {
                    if (string.IsNullOrEmpty(name))
                        name = TranslationManager.GetManager().Translate("Custom");

                    destinationFolder = string.Format(@"{0}\{1}", path, name);

                    if (!System.IO.Directory.Exists(destinationFolder))
                        System.IO.Directory.CreateDirectory(destinationFolder);

                    Promob.Builder.IO.IOHelper.MoveDirectoryContent(source, destinationFolder);
                }
                catch (IOException ex)
                {
                    message = TranslationManager.GetManager().Translate("Failed to move temporary files", ex.Message, ex.StackTrace);
                    return betaNames;
                }
            }

            message = string.Empty;
            return betaNames;
        }

        private List<Version> SortVersions(List<Version> versions)
        {
            IOrderedEnumerable<Version> currentCollection = (from version in versions
                                                             where version.VersionNumber.Equals("Current") && !version.VersionNumber.StartsWith("Beta")
                                                             orderby version.MajorVersion, version.MinorVersion, version.BuildNumber
                                                             select version);

            IEnumerable<Version> versionCollection = (from version in versions
                                                      where !version.VersionNumber.Equals("Current")
                                                      orderby version.MajorVersion, version.MinorVersion, version.BuildNumber
                                                      select version).Reverse();

            List<Version> sortedVersions = new List<Version>();
            sortedVersions.AddRange(currentCollection);
            sortedVersions.AddRange(versionCollection);

            return sortedVersions;
        }

        private void UpdateSelectedVersions()
        {
            this.BetaData.DependencyProducts.ListChanged -= DependencyProducts_ListChanged;

            var ordered = from product in this.BetaData.DependencyProducts
                          orderby product.Order
                          select product;

            List<DependencyProduct> datasource = ordered.ToList();
            this.lbSelected.DataSource = datasource;
            this.BetaData.DependencyProducts.Clear();

            foreach (DependencyProduct dependencyProduct in datasource)
                this.BetaData.DependencyProducts.Add(dependencyProduct);

            this.BetaData.DependencyProducts.ListChanged += DependencyProducts_ListChanged;
        }

        public void Translate()
        {
            if (this._isEditor)
            {
                this.Text = string.Format("{0} ({1})", TranslationManager.GetManager().Translate("Beta Data Editor"), this.BetaData.Name);
                this.btnAction.Text = TranslationManager.GetManager().Translate("Save");
                this.gbDependencyProducts.Text = TranslationManager.GetManager().Translate("Dependency Products");
            }
            else
            {
                this.Text = TranslationManager.GetManager().Translate("Beta Creator");
                this.btnAction.Text = TranslationManager.GetManager().Translate("Create");
                this.gbDependencyProducts.Text = TranslationManager.GetManager().Translate("Products");
            }

            this.tpGeneral.Text = TranslationManager.GetManager().Translate("General");

            this.btnBrowse.Text = TranslationManager.GetManager().Translate("Browse");
            this.btnUp.Text = TranslationManager.GetManager().Translate("MoveUp");
            this.btnDown.Text = TranslationManager.GetManager().Translate("MoveDown");
            this.btnRemove.Text = TranslationManager.GetManager().Translate("Remove");

            this.lblCaptionName.Text = string.Format("{0}:", TranslationManager.GetManager().Translate("Name"));
            this.lblSelectedProducts.Text = TranslationManager.GetManager().Translate("Selected products");
            this.lblSelectProductsBelow.Text = TranslationManager.GetManager().Translate("Select products below");

            this.tsbNewFile.Text = TranslationManager.GetManager().Translate("New BetaData File");
            this.tsbOpenFile.Text = TranslationManager.GetManager().Translate("Open BetaData File");
            this.tsbSaveFileAs.Text = TranslationManager.GetManager().Translate("Save BetaData File As");
            this.tsbSaveFile.Text = TranslationManager.GetManager().Translate("Save BetaData File");
            this.tsbClose.Text = TranslationManager.GetManager().Translate("Close");
            this.tsbFile.Text = TranslationManager.GetManager().Translate("File");
            this.tsbTools.Text = TranslationManager.GetManager().Translate("Tools");
            this.tsbOptions.Text = TranslationManager.GetManager().Translate("Options");
        }

        #endregion

        #region Signed Event Methods

        private void BetaDataEditorForm_Load(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.LoadProducts();
            this.InitControls();
            this.Cursor = Cursors.Default;
        }

        private void BetaDataEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._isEditor)
                this.BetaData.Unlock();
            else if (!BetaCreated)
                this.CheckForChanges();
        }

        private void DependencyProducts_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            this.HasChanges = true;
            this.UpdateControls();
        }

        private void TranslationManager_OnCurrentLanguageChanged(object sender, System.EventArgs e)
        {
            this.Translate();
        }

        private void tsbNewFile_Click(object sender, System.EventArgs e)
        {
            if (!this.CheckForChanges())
                return;

            this.NewFile();
        }

        private void tsbOpenFile_Click(object sender, System.EventArgs e)
        {
            if (!this.CheckForChanges())
                return;

            this.OpenFile();
        }

        private void tsbSaveFileAs_Click(object sender, System.EventArgs e)
        {
            if (!this.NewBetaDataFile && !this.CheckForChanges())
                return;

            this.SaveAs();
        }

        private void tsbSaveFile_Click(object sender, System.EventArgs e)
        {
            this.Save();
        }

        private void tsbClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void tsbOptions_Click(object sender, System.EventArgs e)
        {
            OptionsManager.GetManager().Form.ShowDialog();
        }

        private void tvProducts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.tvProducts.SelectedNode is ProductTreeNode)
                if (this.tvProducts.SelectedNode.Nodes.Count == 0)
                    this.LoadProductVersions(this.tvProducts.SelectedNode as ProductTreeNode);

            this.Cursor = Cursors.Default;
        }

        private void tvProducts_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (this.tvProducts.SelectedNode is DependencyProductTreeNode)
            {
                DependencyProductTreeNode node = this.tvProducts.SelectedNode as DependencyProductTreeNode;
                DependencyProduct dependencyProduct = node.Product;

                if (!this.tvProducts.SelectedNode.Checked)
                    this.BetaData.DependencyProducts.Remove(dependencyProduct);
                else
                    this.BetaData.DependencyProducts.Add(dependencyProduct);
            }

            this.UpdateSelectedVersions();
            this.UpdateControls();
        }

        private void tvProducts_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node is DependencyProductTreeNode)
                this.tvProducts.SelectedNode = e.Node;
            else
                e.Cancel = true;
        }

        private void btnBrowseFolder_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Process.Start(System.IO.Path.GetDirectoryName(this.BetaData.Path));

            this.Cursor = Cursors.Default;
        }

        private void btnUp_Click(object sender, System.EventArgs e)
        {
            if (this.lbSelected.SelectedIndex == 0)
                return;

            DependencyProduct selected = (this.lbSelected.SelectedItem as DependencyProduct);

            int aux = (this.lbSelected.SelectedItem as DependencyProduct).Order;
            (this.lbSelected.SelectedItem as DependencyProduct).Order = (this.lbSelected.Items[this.lbSelected.SelectedIndex - 1] as DependencyProduct).Order;
            (this.lbSelected.Items[this.lbSelected.SelectedIndex - 1] as DependencyProduct).Order = aux;

            this.UpdateSelectedVersions();
            this.lbSelected.SelectedItem = selected;
        }

        private void btnDown_Click(object sender, System.EventArgs e)
        {
            if (this.lbSelected.SelectedIndex == this.lbSelected.Items.Count - 1)
                return;

            DependencyProduct selected = (this.lbSelected.SelectedItem as DependencyProduct);

            int aux = (this.lbSelected.SelectedItem as DependencyProduct).Order;
            (this.lbSelected.SelectedItem as DependencyProduct).Order = (this.lbSelected.Items[this.lbSelected.SelectedIndex + 1] as DependencyProduct).Order;
            (this.lbSelected.Items[this.lbSelected.SelectedIndex + 1] as DependencyProduct).Order = aux;

            this.UpdateSelectedVersions();

            this.lbSelected.SelectedItem = selected;
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            DependencyProduct product = this.lbSelected.SelectedItem as DependencyProduct;
            ProductTreeNode productNode = this.tvProducts.Nodes[product.Name] as ProductTreeNode;
            DependencyProductTreeNode dependencyProductNode = productNode.Nodes[product.ToString()] as DependencyProductTreeNode;

            this.tvProducts.SelectedNode = dependencyProductNode;
            dependencyProductNode.Checked = false;
        }

        private void bntAction_Save_Click(object sender, System.EventArgs e)
        {
            this.UpdateSelectedVersions();
            this.BetaData.Save();
            this.Close();
        }

        private void bntAction_Create_Click(object sender, System.EventArgs e)
        {
            DialogResult dr;

            if (this.BetaData.DependencyProducts.Count == 0)
            {
                dr = MessageBox.Show(
                    TranslationManager.GetManager().Translate("BetaDataEditorForm.Mbox_Caption.MustHaveAtLeastOneProductSelected"),
                    TranslationManager.GetManager().Translate("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (!this.CheckForChanges())
                return;

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            dr = folderDialog.ShowDialog();

            if (dr != DialogResult.OK)
                return;

            string message = string.Empty;

            this.Cursor = Cursors.WaitCursor;
            Dictionary<DependencyProduct, string> betaNames = this.CreateBeta(folderDialog.SelectedPath, out message);
            this.Cursor = Cursors.Default;

            if (betaNames.Count > 0)
            {
                foreach (DependencyProduct dependencyproduct in betaNames.Keys)
                    SVNManager.Instance.DeleteBeta(dependencyproduct.Name, dependencyproduct.Version, betaNames[dependencyproduct]);

                MessageBox.Show(
                        TranslationManager.GetManager().Translate("Operation Successful"),
                        TranslationManager.GetManager().Translate("Attention"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                        TranslationManager.GetManager().Translate("Operation Failed", message),
                        TranslationManager.GetManager().Translate("Attention"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
