using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Promob.Builder.Reflection;
using Promob.Builder.Translation;

namespace Promob.Builder.Options
{
    public partial class OptionsForm : Form
    {
        #region Constructors

        public OptionsForm(Assembly applicationAssembly)
        {
            this._applicationAssembly = applicationAssembly;
            this.InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            TranslationManager.GetManager().OnCurrentLanguageChanged += TranslationManager_OnCurrentLanguageChanged;
            this.Load += OptionsForm_Load;
        }

        #endregion

        #region Attributes and Properties

        private ITranslatable _parentForm;
        private Assembly _applicationAssembly;
        private bool _loaded = false;
        #endregion

        #region Events Methods

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            if (!this._loaded)
            {
                this.LoadOptions();
                this.Translate();
            }

            if (this.tvOptions.Nodes.Count > 0)
                this.tvOptions.SelectedNode = this.tvOptions.Nodes[0];
        }

        private void TranslationManager_OnCurrentLanguageChanged(object sender, EventArgs e)
        {
            this.Translate();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            OptionsManager.GetManager().Save();
        }

        private void tvOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OptionsTreeNode optionsTreeNode = e.Node as OptionsTreeNode;

            if (optionsTreeNode != null)
            {
                this.pnlOptionsContainer.Controls.Clear();
                this.pnlOptionsContainer.Controls.Add(optionsTreeNode.Options.OptionsContainer);
            }
        }

        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            List<Type> types = ReflectionHelper.GetSubClasses(typeof(AbstractOptions), this._applicationAssembly);

            foreach (Type type in types)
            {
                if (OptionsManager.GetManager().OptionsCollection.ContainsKey(type.Name))
                {
                    AbstractOptions options = OptionsManager.GetManager().OptionsCollection[type.Name];
                    this.tvOptions.Nodes.Add(new OptionsTreeNode(options));
                }
            }

            this._loaded = true;
        }

        private void Translate()
        {
            this.Text = TranslationManager.GetManager().Translate("Options");
            this.btnCancel.Text = TranslationManager.GetManager().Translate("Cancel");

            foreach (OptionsTreeNode node in this.tvOptions.Nodes)
            {
                node.Text = TranslationManager.GetManager().Translate(node.Name);
                node.Options.OptionsContainer.Translate();
            }
        }

        #endregion
    }
}
