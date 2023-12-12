	[Desc("Adds a localized circular light centered on the actor to the world's TerrainLightSource trait.")]
	public class TerrainLightSourceInfo : TraitInfo, INotifyEditorPlacementInfo, IRulesetLoaded, ILobbyCustomRulesIgnore
	{
		public readonly WDist Range = WDist.FromCells(10);
		public readonly float Intensity = 0;
		public readonly float RedTint = 0;
		public readonly float GreenTint = 0;
		public readonly float BlueTint = 0;

		object INotifyEditorPlacementInfo.AddedToEditor(EditorActorPreview preview, World editorWorld)
		{
			var tint = new float3(RedTint, GreenTint, BlueTint);
			return editorWorld.WorldActor.Trait<TerrainLighting>().AddLightSource(preview.CenterPosition, Range, Intensity, tint);
		}

		void INotifyEditorPlacementInfo.RemovedFromEditor(EditorActorPreview preview, World editorWorld, object data)
		{
			editorWorld.WorldActor.Trait<TerrainLighting>().RemoveLightSource((int)data);
		}

		public void RulesetLoaded(Ruleset rules, ActorInfo ai)
		{
			if (!rules.Actors["world"].HasTraitInfo<TerrainLightingInfo>())
				throw new YamlException("TerrainLightSource can only be used with the world TerrainLighting trait.");
		}

		public override object Create(ActorInitializer init) { return new TerrainLightSource(init.Self, this); }
	}