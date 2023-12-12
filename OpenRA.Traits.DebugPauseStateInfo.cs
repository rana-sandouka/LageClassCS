	[Desc("Checks for pause related desyncs. Attach this to the world actor.")]
	public class DebugPauseStateInfo : TraitInfo
	{
		public override object Create(ActorInitializer init) { return new DebugPauseState(init.World); }
	}