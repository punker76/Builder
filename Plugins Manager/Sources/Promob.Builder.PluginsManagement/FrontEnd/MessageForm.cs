using System.Drawing;
using System.Windows.Forms;
using Promob.Builder.Translation;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class MessageForm : Form, ITranslatable
    {
        #region Constructors

        public MessageForm(string errorMessage)
        {
            InitializeComponent();
            //this.txtErrorMessage.Enabled = false;
            this.txtMessage.BackColor = Color.White;
            this.txtMessage.Text = errorMessage;
            this.Translate();
        }

        #endregion

        public void Translate()
        {
            this.Text = TranslationManager.GetManager().Translate("Message");
        }
    }
}
