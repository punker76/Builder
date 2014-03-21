
using System;
namespace Promob.Builder.Performance
{
    public struct Take
    {
        #region Attributes and Properties

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private TimeSpan _at;
        public TimeSpan At
        {
            get { return _at; }
            set { _at = value; }
        }

        #endregion

        #region Constructors

        public Take(string description, TimeSpan at)
        {
            this._description = description;
            this._at = at;
        }

        #endregion
    }
}
