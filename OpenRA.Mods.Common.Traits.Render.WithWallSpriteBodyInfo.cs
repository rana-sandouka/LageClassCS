	[Desc("Render trait for actors that change sprites if neighbors with the same trait are present.")]
	class WithWallSpriteBodyInfo : WithSpriteBodyInfo, IWallConnectorInfo, Requires<BuildingInfo>
	{
		public readonly string Type = "wall";

		public override object Create(ActorInitializer init) { return new WithWallSpriteBody(init, this); }

		public override IEnumerable<IActorPreview> RenderPreviewSprites(ActorPreviewInitializer init, RenderSpritesInfo rs, string image, int facings, PaletteReference p)
		{
			if (!EnabledByDefault)
				yield break;

			var adjacent = 0;
			var locationInit = init.GetOrDefault<LocationInit>();
			var neighbourInit = init.GetOrDefault<RuntimeNeighbourInit>();

			if (locationInit != null && neighbourInit != null)
			{
				var location = locationInit.Value;
				foreach (var kv in neighbourInit.Value)
				{
					var haveNeighbour = false;
					foreach (var n in kv.Value)
					{
						var rb = init.World.Map.Rules.Actors[n].TraitInfos<IWallConnectorInfo>().FirstEnabledTraitOrDefault();
						if (rb != null && rb.GetWallConnectionType() == Type)
						{
							haveNeighbour = true;
							break;
						}
					}

					if (!haveNeighbour)
						continue;

					if (kv.Key == location + new CVec(0, -1))
						adjacent |= 1;
					else if (kv.Key == location + new CVec(+1, 0))
						adjacent |= 2;
					else if (kv.Key == location + new CVec(0, +1))
						adjacent |= 4;
					else if (kv.Key == location + new CVec(-1, 0))
						adjacent |= 8;
				}
			}

			var anim = new Animation(init.World, image);
			anim.PlayFetchIndex(RenderSprites.NormalizeSequence(anim, init.GetDamageState(), Sequence), () => adjacent);

			yield return new SpriteActorPreview(anim, () => WVec.Zero, () => 0, p, rs.Scale);
		}

		string IWallConnectorInfo.GetWallConnectionType()
		{
			return Type;
		}
	}