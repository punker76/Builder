using System.ComponentModel;

namespace Promob.Builder.Core
{
    public class AllowedDistributions : BindingList<string>
    {
        #region Overriden Methods

        public override string ToString()
        {
            string ret = string.Empty;

            for (int i = 0; i < this.Count - 1; i++)
                ret += this[i] + "; ";

            if (this.Count > 0)
                ret += this[this.Count - 1];

            return ret;
        }

        #endregion

        #region Public Methods

        public static AllowedDistributions FromString(string value)
        {
            AllowedDistributions allowedDistributions = new AllowedDistributions();

            foreach (string s in value.Split(';'))
                allowedDistributions.Add(s.Trim());

            return allowedDistributions;
        }

        #endregion
    }
}
