using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start.ConfigurationWizard
{
    public class Customer
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set { _adress = value; }
        }
    }
}
