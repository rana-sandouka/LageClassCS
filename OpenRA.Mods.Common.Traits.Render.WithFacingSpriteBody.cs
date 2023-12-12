	public class WithFacingSpriteBody : WithSpriteBody
	{
		public WithFacingSpriteBody(ActorInitializer init, WithFacingSpriteBodyInfo info)
			: base(init, info, RenderSprites.MakeFacingFunc(init.Self)) { }
	}