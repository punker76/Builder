using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Promob.Builder.Email
{
    public partial class ContactsListForm : Form
    {
        #region Constructors

        public ContactsListForm()
        {
            this.InitializeComponent();
            this.LoadContactsList();
            this.DefineColumnSize();
        }

        #endregion

        #region Delegate

        public delegate void EmailAddedHandler();

        #endregion

        #region Events

        public event EmailAddedHandler EmailAdded;

        #endregion

        #region Attributes and Properties

        public string Contacts { get; set; }

        #endregion

        #region Private Fields

        private List<Contact> _foundContacts = new List<Contact>();

        #endregion

        #region Private Methods

        private void AddSelectedContact()
        {
            string email = this.dvgContactsList.Rows[dvgContactsList.CurrentCell.RowIndex].Cells["Email"].Value.ToString();

            foreach (string currentEmail in EmailManager.GetManager().SelectedContacts)
                if (currentEmail.Equals(email))
                    return;

            EmailManager.GetManager().SelectedContacts.Add(email);
            this.txtSelectedContacts.Text += email + "; ";
        }

        private void DefineColumnSize()
        {
            this.dvgContactsList.Columns[0].Width = 250;
            this.dvgContactsList.Columns[1].Width = 244;
            this.dvgContactsList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        }

        private void LoadContactsList()
        {
            List<Contact> contactsList = EmailManager.GetManager().ContactsList;

            this.dvgContactsList.DataSource = contactsList;
        }

        private void SearchContacts()
        {
            this.dvgContactsList.DataSource = null;
            this._foundContacts.Clear();

            this.dvgContactsList.DataSource = EmailManager.GetManager().SearchContacts(this.txtSearch.Text);

            this.DefineColumnSize();
            this.dvgContactsList.Refresh();
        }

        #endregion

        #region Protected Methods

        protected void RaiseEmailAdded(string emails)
        {
            if (this.EmailAdded != null)
                this.EmailAdded();
        }

        #endregion

        #region Signed Events Methods

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.RaiseEmailAdded(this.txtSelectedContacts.Text);
            this.Contacts = this.txtSelectedContacts.Text;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SearchContacts();
        }

        private void btnTo_Click(object sender, EventArgs e)
        {
            this.AddSelectedContact();
        }

        private void dvgContactsList_DoubleClick(object sender, EventArgs e)
        {
            this.AddSelectedContact();
        }

        #endregion
    }
}
