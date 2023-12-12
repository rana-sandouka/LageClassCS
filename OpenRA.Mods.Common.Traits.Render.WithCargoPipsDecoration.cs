	public class WithCargoPipsDecoration : WithDecorationBase<WithCargoPipsDecorationInfo>
	{
		readonly Cargo cargo;
		readonly Animation pips;
		readonly int pipCount;

		public WithCargoPipsDecoration(Actor self, WithCargoPipsDecorationInfo info)
			: base(self, info)
		{
			cargo = self.Trait<Cargo>();
			pipCount = info.PipCount > 0 ? info.PipCount : cargo.Info.MaxWeight;
			pips = new Animation(self.World, info.Image);
		}

		string GetPipSequence(int i)
		{
			var n = i * cargo.Info.MaxWeight / pipCount;

			foreach (var c in cargo.Passengers)
			{
				var pi = c.Info.TraitInfo<PassengerInfo>();
				if (n < pi.Weight)
				{
					var sequence = Info.FullSequence;
					if (pi.CustomPipType != null && !Info.CustomPipSequences.TryGetValue(pi.CustomPipType, out sequence))
						Log.Write("debug", "Actor type {0} defines a custom pip type {1} that is not defined for actor type {2}".F(c.Info.Name, pi.CustomPipType, self.Info.Name));

					return sequence;
				}

				n -= pi.Weight;
			}

			return Info.EmptySequence;
		}

		protected override IEnumerable<IRenderable> RenderDecoration(Actor self, WorldRenderer wr, int2 screenPos)
		{
			pips.PlayRepeating(Info.EmptySequence);

			var palette = wr.Palette(Info.Palette);
			var pipSize = pips.Image.Size.XY.ToInt2();
			var pipStride = Info.PipStride != int2.Zero ? Info.PipStride : new int2(pipSize.X, 0);

			screenPos -= pipSize / 2;
			for (var i = 0; i < pipCount; i++)
			{
				pips.PlayRepeating(GetPipSequence(i));
				yield return new UISpriteRenderable(pips.Image, self.CenterPosition, screenPos, 0, palette, 1f);

				screenPos += pipStride;
			}
		}
	}