using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd
{
    public partial class OpenBetasForm : Form
    {
        #region Attributes and Properties

        private Dictionary<PluginVersion, string> _betasPaths;

        #endregion

        #region Constructors

        public OpenBetasForm(Dictionary<PluginVersion, string> betasPaths)
        {
            this._betasPaths = betasPaths;
            this.InitializeComponent();
            this.Init();
        }

        #endregion

        #region Events Methods

        private void OpenBetasForm_Load(object sender, EventArgs e)
        {
            this.LoadDataGrid();
        }

        private void dgvBetas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dgvBetas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            this.Load += OpenBetasForm_Load;

        }

        private void LoadDataGrid()
        {
            //this.dgvBetas.AutoGenerateColumns = false;
            this.dgvBetas.DataSource = this._betasPaths;
            this.dgvBetas.CellContentClick += dgvBetas_CellContentClick;
            this.dgvBetas.CellClick += dgvBetas_CellClick;
        }

        #endregion
    }
}
