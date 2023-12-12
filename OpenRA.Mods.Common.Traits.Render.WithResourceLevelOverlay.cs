	class WithResourceLevelOverlay : ConditionalTrait<WithResourceLevelOverlayInfo>, INotifyOwnerChanged, INotifyDamageStateChanged
	{
		readonly AnimationWithOffset anim;
		readonly RenderSprites rs;
		readonly WithSpriteBody wsb;

		PlayerResources playerResources;

		public WithResourceLevelOverlay(Actor self, WithResourceLevelOverlayInfo info)
			: base(info)
		{
			rs = self.Trait<RenderSprites>();
			wsb = self.Trait<WithSpriteBody>();
			playerResources = self.Owner.PlayerActor.Trait<PlayerResources>();

			var a = new Animation(self.World, rs.GetImage(self));
			a.PlayFetchIndex(info.Sequence, () =>
				playerResources.ResourceCapacity != 0 ?
				((10 * a.CurrentSequence.Length - 1) * playerResources.Resources) / (10 * playerResources.ResourceCapacity) :
				0);

			anim = new AnimationWithOffset(a, null, () => IsTraitDisabled, 1024);
			rs.Add(anim, info.Palette, info.IsPlayerPalette);
		}

		void INotifyDamageStateChanged.DamageStateChanged(Actor self, AttackInfo e)
		{
			if (anim.Animation.CurrentSequence != null)
				anim.Animation.ReplaceAnim(wsb.NormalizeSequence(self, Info.Sequence));
		}

		void INotifyOwnerChanged.OnOwnerChanged(Actor self, Player oldOwner, Player newOwner)
		{
			playerResources = newOwner.PlayerActor.Trait<PlayerResources>();
		}
	}