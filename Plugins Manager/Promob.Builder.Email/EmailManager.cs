using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Promob.Builder.Email
{
    public class EmailManager
    {
        #region External

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        #endregion

        #region Contructors

        private EmailManager()
        {
        }

        #endregion

        #region Singleton

        private static EmailManager _instance;
        public static EmailManager GetManager()
        {
            if (EmailManager._instance == null)
                EmailManager._instance = new EmailManager();

            return EmailManager._instance;
        }

        #endregion

        #region Attributes and Properties

        private List<Contact> _contactsList;
        public List<Contact> ContactsList
        {
            get
            {
                if (this._contactsList == null)
                    this.LoadContactsList();

                return this._contactsList;
            }
            set { this._contactsList = value; }
        }

        private List<string> _selectedContacts = new List<string>();
        public List<string> SelectedContacts
        {
            get { return this._selectedContacts; }
            set { this._selectedContacts = value; }
        }

        #endregion

        #region Private Methods

        private Outlook.AddressList GetGlobalAddressList(Outlook.Application outlookApp, Outlook.Store store)
        {
            // Property string for the UID of a store or address list. 
            string PR_EMSMDB_SECTION_UID =
                @"http://schemas.microsoft.com/mapi/proptag/0x3D150102";

            if (store == null)
                throw new ArgumentNullException();

            // Obtain the store UID using the proprety string and  
            // property accessor on the store. 
            Outlook.PropertyAccessor oPAStore = store.PropertyAccessor;

            // Convert the store UID to a string value. 
            string storeUID = oPAStore.BinaryToString(oPAStore.GetProperty(PR_EMSMDB_SECTION_UID));

            // Enumerate each address list associated 
            // with the session. 
            foreach (Outlook.AddressList addrList in outlookApp.Session.AddressLists)
            {
                // Obtain the address list UID and convert it to  
                // a string value. 
                Outlook.PropertyAccessor oPAAddrList = addrList.PropertyAccessor;
                string addrListUID = oPAAddrList.BinaryToString(oPAAddrList.GetProperty(PR_EMSMDB_SECTION_UID));

                // Return the address list associated with the store 
                // if the address list UID matches the store UID and 
                // type is olExchangeGlobalAddressList. 
                if (addrListUID == storeUID && addrList.AddressListType == Outlook.OlAddressListType.olExchangeGlobalAddressList)
                    return addrList;
            }

            return null;
        }

        private void LoadContactsList()
        {
            Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();

            Outlook.Folder contactsFolder = outlookApp.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olPublicFoldersAllPublicFolders) as Outlook.Folder;

            Outlook.Folder currentFolder = outlookApp.ActiveExplorer().CurrentFolder as Outlook.Folder;
            Outlook.Store currentStore = currentFolder.Store;

            Outlook.AddressList addressList = this.GetGlobalAddressList(outlookApp, currentStore);
            Outlook.AddressEntries entries = addressList.AddressEntries;

            this._contactsList = new List<Contact>();

            foreach (Outlook.AddressEntry entry in entries)
            {
                Outlook.ExchangeDistributionList distList = entry.GetExchangeDistributionList();
                Outlook.ExchangeUser user = entry.GetExchangeUser();
                Outlook.ContactItem contact = entry.GetContact();

                if (distList != null)
                    this._contactsList.Add(new Contact(distList.Name, distList.PrimarySmtpAddress));

                if (contact != null)
                    this._contactsList.Add(new Contact(contact.FirstName + " " + contact.LastName, contact.IMAddress));

                if (user != null)
                {
                    if (user.FirstName == null && user.LastName == null)
                        this._contactsList.Add(new Contact(user.Name, user.PrimarySmtpAddress));
                    else
                        this._contactsList.Add(new Contact(user.FirstName + " " + user.LastName, user.PrimarySmtpAddress));
                }
            }
        }

        private void StartOutlook()
        {
            try
            {
                Outlook.Application app = Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
            }
            catch (Exception ex)
            {
                Process outlook = new Process();

                ProcessStartInfo startInfo = new ProcessStartInfo("OUTLOOK");
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                outlook.StartInfo = startInfo;

                outlook.Start();

                var process = Process.GetProcessesByName("OUTLOOK").First();

                Thread.Sleep(100);

                while (!process.HasExited)
                {
                    string title = Process.GetProcessById(process.Id).MainWindowTitle;

                    if (title.ToLower().StartsWith("caixa") || title.ToLower().StartsWith("inbox"))
                    {
                        ShowWindowAsync(Process.GetProcessById(process.Id).MainWindowHandle, 2);
                        break;
                    }

                    Thread.Sleep(100);
                }

                Outlook.Application app = Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
                app.ActiveExplorer().WindowState = Outlook.OlWindowState.olMinimized;
            }
        }

        #endregion

        #region Public Methods

        public string LoadLastContacts()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"BuilderEmail\LastContacts.cfg");
            if (!File.Exists(path))
                return string.Empty;

            using (StreamReader reader = new StreamReader(path))
                return reader.ReadToEnd();
        }

        public void SaveLastContacts(string contacts)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuilderEmail");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (StreamWriter writer = new StreamWriter(Path.Combine(path, "LastContacts.cfg")))
                writer.WriteLine(contacts);
        }

        public List<Contact> SearchContacts(string word)
        {
            List<Contact> foundContacts = new List<Contact>();
            string searchTerm = word.ToLower();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                foreach (Contact contact in EmailManager.GetManager().ContactsList)
                {
                    string name = contact.Name.ToLower();
                    if (name.Contains(searchTerm))
                        foundContacts.Add(contact);
                }

                if (foundContacts.Count > 0)
                    return foundContacts;
            }

            return EmailManager.GetManager().ContactsList;
        }

        public void SendEmail(string contacts, string productsNames)
        {
            try
            {
                this.StartOutlook();

                Outlook.Application outlook = new Outlook.Application();
                Outlook.MailItem message = (Outlook.MailItem)outlook.CreateItem(Outlook.OlItemType.olMailItem);

                message.To = contacts;
                message.Subject = "Liberação de plugins";
                message.Body = "Os seguintes plugins foram liberados:\n\n" + productsNames;
                message.Save();
                message.Send();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("{0} Exception caught: ", e);
            }
        }

        public void SendEmail(string contacts, string subject, string body, bool htmlFormat)
        {
            try
            {
                this.StartOutlook();

                Outlook.Application outlook = new Outlook.Application();
                Outlook.MailItem message = (Outlook.MailItem)outlook.CreateItem(Outlook.OlItemType.olMailItem);

                message.To = contacts;
                message.Subject = subject;

                if(htmlFormat)
                    message.HTMLBody = body;
                else
                    message.Body = body;

                message.Save();
                message.Send();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("{0} Exception caught: ", e);
            }
        }

        #endregion
    }
}
