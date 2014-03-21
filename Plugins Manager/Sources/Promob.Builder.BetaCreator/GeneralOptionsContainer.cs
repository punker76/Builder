using System.Collections.Generic;
using System.Windows.Forms;
using Promob.Builder.Options;
using Promob.Builder.Translation;

namespace Promob.Builder.BetaCreator
{
    public partial class GeneralOptionsContainer : OptionsContainer
    {
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

        #region Private Methods

        private void InitControls()
        {
            this.LoadGeneralOptions();
        }

        private void LoadGeneralOptions()
        {
            this.LoadLanguagesCombobox();
        }

        private void LoadLanguagesCombobox()
        {
            this.cbLanguage.SelectedIndexChanged -= new System.EventHandler(this.cbLanguage_SelectedIndexChanged);

            this.cbLanguage.DataSource = null;

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
            this.lblLanguage.Text = TranslationManager.GetManager().Translate("Language");
            this.LoadLanguagesCombobox();
        }

        #endregion
    }
}
