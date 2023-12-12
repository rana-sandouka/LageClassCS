	public class WithAcceptDeliveredCashAnimation : ConditionalTrait<WithAcceptDeliveredCashAnimationInfo>, INotifyCashTransfer
	{
		readonly WithSpriteBody wsb;

		public WithAcceptDeliveredCashAnimation(Actor self, WithAcceptDeliveredCashAnimationInfo info)
			: base(info)
		{
			wsb = self.TraitsImplementing<WithSpriteBody>().Single(w => w.Info.Name == info.Body);
		}

		bool playing;
		void INotifyCashTransfer.OnAcceptingCash(Actor self, Actor donor)
		{
			if (IsTraitDisabled || playing)
				return;

			playing = true;
			wsb.PlayCustomAnimation(self, Info.Sequence, () => playing = false);
		}

		void INotifyCashTransfer.OnDeliveringCash(Actor self, Actor acceptor) { }
	}