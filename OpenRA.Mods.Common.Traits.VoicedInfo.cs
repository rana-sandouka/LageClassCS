	[Desc("This actor has a voice.")]
	public class VoicedInfo : TraitInfo
	{
		[VoiceSetReference]
		[FieldLoader.Require]
		[Desc("Which voice set to use.")]
		public readonly string VoiceSet = null;

		[Desc("Multiply volume with this factor.")]
		public readonly float Volume = 1f;

		public override object Create(ActorInitializer init) { return new Voiced(init.Self, this); }
	}