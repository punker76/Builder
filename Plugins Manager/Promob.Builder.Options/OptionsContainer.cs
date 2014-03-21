using System.Windows.Forms;
using Promob.Builder.Translation;

namespace Promob.Builder.Options
{
    public partial class OptionsContainer : UserControl, ITranslatable
    {
        #region Constructors

        public OptionsContainer()
        {
            InitializeComponent();
            this.Load += OptionsContainer_Load;
        }

        #endregion

        #region Events Methods

        public void OptionsContainer_Load(object sender, System.EventArgs e)
        {
            if (!DesignMode)
                this.Dock = DockStyle.Fill;

            this.Translate();
        }

        #endregion

        #region Virtual Methods

        public virtual void Translate()
        {
            if (base.Parent != null)
                base.Parent.Parent.Refresh();
        }

        #endregion
    }
}
