    public class BlackStyleForm : Form
    {
        public BlackStyleForm()
        {
            Icon = ShareXResources.Icon;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle fillRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

            if (fillRect.IsValid())
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(fillRect, Color.FromArgb(80, 80, 80), Color.FromArgb(40, 40, 40), LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, fillRect);
                }
            }

            base.OnPaint(e);
        }
    }