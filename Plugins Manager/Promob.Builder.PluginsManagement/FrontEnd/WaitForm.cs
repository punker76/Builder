using System.ComponentModel;
using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class WaitForm : Form
    {
        private Console _console;
        public Console Console
        {
            get { return _console; }
            set { _console = value; }
        }

        private BackgroundWorker _worker;
        public BackgroundWorker Worker
        {
            get { return _worker; }
            set { _worker = value; }
        }

        public WaitForm(Console console, BackgroundWorker worker)
        {
            InitializeComponent();

            this.Console = console;
            this.Worker = worker;


            this.Worker.WorkerReportsProgress = true;
            this.Worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        protected override void OnShown(System.EventArgs e)
        {
            base.OnShown(e);
            _worker.RunWorkerAsync();
        }
    }
}
