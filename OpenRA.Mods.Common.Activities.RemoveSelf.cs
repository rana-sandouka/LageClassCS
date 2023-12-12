	public class RemoveSelf : Activity
	{
		public override bool Tick(Actor self)
		{
			if (IsCanceling) return true;
			self.Dispose();
			Cancel(self);
			return true;
		}
	}