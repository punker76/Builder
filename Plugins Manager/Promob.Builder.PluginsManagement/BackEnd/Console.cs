using System.Collections.Generic;
using System.ComponentModel;
using StepRunner;

namespace Promob.Builder.PluginsManagement.BackEnd
{
    public class Console : IConsole
    {
        #region Attributes and Properties

        private List<string> _messages;
        public List<string> Messages
        {
            get
            {
                if (this._messages == null)
                    this._messages = new List<string>();

                return this._messages;
            }
            set { this._messages = value; }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler MessageWriten;

        #endregion

        #region Public Methods

        public void Clear()
        {
            this.Messages.Clear();
        }

        public void Write(string[] outputArray)
        {
            this.Messages.Add(outputArray.ToString());

            if (this.MessageWriten != null)
                this.MessageWriten(this, new PropertyChangedEventArgs(outputArray.ToString()));
        }

        public void Write(string output)
        {
            this.Messages.Add(output);

            if (this.MessageWriten != null)
                this.MessageWriten(this, new PropertyChangedEventArgs(output));
        }

        #endregion
    }
}
