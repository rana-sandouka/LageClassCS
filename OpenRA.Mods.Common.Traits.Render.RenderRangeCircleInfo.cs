	[Desc("Draw a circle indicating my weapon's range.")]
	class RenderRangeCircleInfo : TraitInfo, IPlaceBuildingDecorationInfo, IRulesetLoaded, Requires<AttackBaseInfo>
	{
		public readonly string RangeCircleType = null;

		[Desc("Range to draw if no armaments are available.")]
		public readonly WDist FallbackRange = WDist.Zero;

		[Desc("Which circle to show. Valid values are `Maximum`, and `Minimum`.")]
		public readonly RangeCircleMode RangeCircleMode = RangeCircleMode.Maximum;

		[Desc("Color of the circle.")]
		public readonly Color Color = Color.FromArgb(128, Color.Yellow);

		[Desc("Range circle line width.")]
		public readonly float Width = 1;

		[Desc("Color of the border.")]
		public readonly Color BorderColor = Color.FromArgb(96, Color.Black);

		[Desc("Range circle border width.")]
		public readonly float BorderWidth = 3;

		// Computed range
		Lazy<WDist> range;

		public IEnumerable<IRenderable> RenderAnnotations(WorldRenderer wr, World w, ActorInfo ai, WPos centerPosition)
		{
			if (range == null || range.Value == WDist.Zero)
				return SpriteRenderable.None;

			var localRange = new RangeCircleAnnotationRenderable(
				centerPosition,
				range.Value,
				0,
				Color,
				Width,
				BorderColor,
				BorderWidth);

			var otherRanges = w.ActorsWithTrait<RenderRangeCircle>()
				.Where(a => a.Trait.Info.RangeCircleType == RangeCircleType)
				.SelectMany(a => a.Trait.RangeCircleRenderables(wr));

			return otherRanges.Append(localRange);
		}

		public override object Create(ActorInitializer init) { return new RenderRangeCircle(init.Self, this); }

		public void RulesetLoaded(Ruleset rules, ActorInfo ai)
		{
			// ArmamentInfo.ModifiedRange is set by RulesetLoaded, and may not have been initialized yet.
			// Defer this lookup until we really need it to ensure we get the correct value.
			range = Exts.Lazy(() =>
			{
				var armaments = ai.TraitInfos<ArmamentInfo>().Where(a => a.EnabledByDefault);
				if (!armaments.Any())
					return FallbackRange;

				return armaments.Max(a => a.ModifiedRange);
			});
		}
	}