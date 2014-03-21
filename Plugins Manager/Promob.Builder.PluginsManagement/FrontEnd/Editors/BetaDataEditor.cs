using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Promob.Builder.PluginsManagement.BackEnd;

namespace Promob.Builder.PluginsManagement.FrontEnd.Editors
{
    public class BetaDataEditor : UITypeEditor
    {
        #region Overriden Methods

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            BetaData beta = value as BetaData;

            if (svc != null && beta != null)
            {
                using (BetaDataEditorForm form = new BetaDataEditorForm(beta))
                    form.ShowDialog();
            }

            return beta;
        }

        #endregion
    }
}
