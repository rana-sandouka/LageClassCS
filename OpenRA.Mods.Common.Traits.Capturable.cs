	public class Capturable : ConditionalTrait<CapturableInfo>, INotifyCapture
	{
		readonly CaptureManager captureManager;

		public Capturable(Actor self, CapturableInfo info)
			: base(info)
		{
			captureManager = self.Trait<CaptureManager>();
		}

		void INotifyCapture.OnCapture(Actor self, Actor captor, Player oldOwner, Player newOwner, BitSet<CaptureType> captureTypes)
		{
			if (Info.CancelActivity)
				self.CancelActivity();
		}

		protected override void TraitEnabled(Actor self) { captureManager.RefreshCapturable(self); }
		protected override void TraitDisabled(Actor self) { captureManager.RefreshCapturable(self); }
	}