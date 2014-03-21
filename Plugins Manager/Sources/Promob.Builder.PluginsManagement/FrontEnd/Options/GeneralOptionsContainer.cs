using System.Collections.Generic;
using System.Windows.Forms;
using Promob.Builder.Options;
using Promob.Builder.PluginsManagement.BackEnd.Options;
using Promob.Builder.Translation;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd.Options
{
    public partial class GeneralOptionsContainer : OptionsContainer
    {
        #region Constructors

        public GeneralOptionsContainer()
            : this(null)
        {
        }

        public GeneralOptionsContainer(GeneralOptions GeneralOptions)
            : base()
        {
            this._generalOptions = GeneralOptions;
            this.InitializeComponent();
            this.InitControls();
            this.Translate();
        }

        #endregion

        #region Attributes and Properties

        private GeneralOptions _generalOptions;
        public GeneralOptions GeneralOptions
        {
            get
            {
                if (this._generalOptions == null)
                    this._generalOptions = new GeneralOptions(string.Empty);

                return this._generalOptions;
            }
            set
            {
                this._generalOptions = value;
                this.LoadGeneralOptions();
            }
        }

        #endregion

        #region Private Methods

        private void InitControls()
        {
            this.txtProductsManagerExecutablePath.PathChanged += ProductsManagerExecutablePathChanged;
            this.LoadGeneralOptions();
        }

        private void ProductsManagerExecutablePathChanged(object sender, System.EventArgs e)
        {
            this.GeneralOptions.ProductsManagerExecutablePath = this.txtProductsManagerExecutablePath.Path;
        }

        private void LoadGeneralOptions()
        {
            this.LoadLanguagesCombobox();

            this.txtProductsManagerExecutablePath.Path = this.GeneralOptions.ProductsManagerExecutablePath;
        }

        private void LoadLanguagesCombobox()
        {
            Dictionary<int, string> languages = new Dictionary<int, string>();

            foreach (int code in TranslationManager.LanguageCodes.Keys)
                languages.Add(code, TranslationManager.GetManager().Translate(TranslationManager.LanguageCodes[code]));

            this.cbLanguage.DataSource = new BindingSource(languages, null);
            this.cbLanguage.DisplayMember = "Value";
            this.cbLanguage.ValueMember = "Key";

            foreach (KeyValuePair<int, string> value in this.cbLanguage.Items)
                if (value.Key == this.GeneralOptions.Language)
                    this.cbLanguage.SelectedItem = value;

            this.cbLanguage.SelectedIndexChanged += new System.EventHandler(this.cbLanguage_SelectedIndexChanged);
        }

        private void cbLanguage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.GeneralOptions.Language = (int)this.cbLanguage.SelectedValue;
            TranslationManager.GetManager().CurrentLanguage = this.GeneralOptions.Language;
            this.Translate();
        }

        public override void Translate()
        {
            base.Translate();
            this.txtProductsManagerExecutablePath.Text = TranslationManager.GetManager().Translate("Products Manager Executable Path");
            this.lblLanguage.Text = TranslationManager.GetManager().Translate("Language");
        }

        #endregion

        #region Signed Event Methods

        private void btnVersionManagement_Click(object sender, System.EventArgs e)
        {
            VersionManagement versionManagementForm = new VersionManagement(new ProductData(null), false);
            versionManagementForm.ShowDialog(this);
        }

        #endregion
    }
}
