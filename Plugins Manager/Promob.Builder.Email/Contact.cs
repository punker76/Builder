namespace Promob.Builder.Email
{
    public class Contact
    {
        #region Constructors

        public Contact(string name, string email)
        {
            this._name = name;
            this._email = email;
        }

        #endregion

        #region Attributes and Properties

        private string _name;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        private string _email;
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        #endregion
    }
}
