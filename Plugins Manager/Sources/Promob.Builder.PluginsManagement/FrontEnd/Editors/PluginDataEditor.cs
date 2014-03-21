
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Promob.Builder.PluginsManagement.BackEnd;
namespace Promob.Builder.PluginsManagement.FrontEnd.Editors
{
    public class PluginDataEditor : UITypeEditor
    {
        #region Overriden Methods

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            PluginData pluginData = value as PluginData;

            if (svc != null && pluginData != null)
            {
                using (PluginDataEditorForm form = new PluginDataEditorForm(pluginData))
                    form.ShowDialog();
            }

            return pluginData;
        }

        #endregion
    }
}
