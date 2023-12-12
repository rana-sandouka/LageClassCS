	public class WithSupportPowerActivationOverlay : ConditionalTrait<WithSupportPowerActivationOverlayInfo>, INotifySupportPower
	{
		readonly Animation overlay;
		bool visible;

		public WithSupportPowerActivationOverlay(Actor self, WithSupportPowerActivationOverlayInfo info)
			: base(info)
		{
			var rs = self.Trait<RenderSprites>();
			var body = self.Trait<BodyOrientation>();

			overlay = new Animation(self.World, rs.GetImage(self));
			overlay.PlayThen(info.Sequence, () => visible = false);

			var anim = new AnimationWithOffset(overlay,
				() => body.LocalToWorld(info.Offset.Rotate(body.QuantizeOrientation(self, self.Orientation))),
				() => IsTraitDisabled || !visible,
				p => RenderUtils.ZOffsetFromCenter(self, p, 1));

			rs.Add(anim, info.Palette, info.IsPlayerPalette);
		}

		void INotifySupportPower.Charged(Actor self) { }

		void INotifySupportPower.Activated(Actor self)
		{
			visible = true;
			overlay.PlayThen(overlay.CurrentSequence.Name, () => visible = false);
		}
	}