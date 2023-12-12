    internal class KeyFileNameEditor : FileNameEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || provider == null)
            {
                return base.EditValue(context, provider, value);
            }
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = Resources.KeyFileNameEditor_EditValue_Browse_for_a_key_file___;
                dlg.Filter = "Key file (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    value = dlg.FileName;
                }
            }
            return value;
        }
    }