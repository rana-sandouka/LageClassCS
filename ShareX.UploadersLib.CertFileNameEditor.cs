    internal class CertFileNameEditor : FileNameEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || provider == null)
            {
                return base.EditValue(context, provider, value);
            }
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = Resources.CertFileNameEditor_EditValue_Browse_for_a_certificate_file___;
                dlg.Filter = "Certificate file (*.cer)|*.cer";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    value = dlg.FileName;
                }
            }
            return value;
        }
    }