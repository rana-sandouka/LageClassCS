    public class JsonFileNameEditor : FileNameEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || provider == null)
            {
                return base.EditValue(context, provider, value);
            }
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "JavaScript Object Notation files (*.json)|*.json";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    value = dlg.FileName;
                }
            }
            return value;
        }
    }