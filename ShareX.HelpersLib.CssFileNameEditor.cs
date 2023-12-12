    public class CssFileNameEditor : FileNameEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || provider == null)
            {
                return base.EditValue(context, provider, value);
            }
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.FileName = "Default.css";
                dlg.Title = Resources.CssFileNameEditor_EditValue_Browse_for_a_Cascading_Style_Sheet___;
                dlg.Filter = "Cascading Style Sheets (*.css)|*.css";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    value = dlg.FileName;
                }
            }
            return value;
        }
    }