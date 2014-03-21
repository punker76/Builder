using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Promob.Builder.Email
{
    public partial class NewEmailForm : Form
    {
        #region Constructors

        public NewEmailForm()
        {
            this.InitializeComponent();
        }

        public NewEmailForm(string title, string body)
        {
            this.InitializeComponent();

            this.txtMessage.Text = body;
            this.txtTitle.Text = title;
            this.txtContacts.Text = EmailManager.GetManager().LoadLastContacts();
        }

        #endregion

        #region Signed Event Methods

        private void btnContacts_Click(object sender, EventArgs e)
        {
            ContactsListForm form = new ContactsListForm();
            form.ShowDialog();

            this.txtContacts.Text = form.Contacts;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            EmailManager.GetManager().SaveLastContacts(this.txtContacts.Text);

            EmailManager.GetManager().SendEmail(this.txtContacts.Text, this.txtTitle.Text, this.txtMessage.Text, false);

            this.Dispose();
        }

        #endregion
    }
}
