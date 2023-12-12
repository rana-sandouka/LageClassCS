	public class SpriteAnnotation : IEffect, IEffectAnnotation
	{
		readonly string palette;
		readonly Animation anim;
		readonly WPos pos;

		public SpriteAnnotation(WPos pos, World world, string image, string sequence, string palette)
		{
			this.palette = palette;
			this.pos = pos;
			anim = new Animation(world, image);
			anim.PlayThen(sequence, () => world.AddFrameEndTask(w => { w.Remove(this); w.ScreenMap.Remove(this); }));
			world.ScreenMap.Add(this, pos, anim.Image);
		}

		void IEffect.Tick(World world)
		{
			anim.Tick();
			world.ScreenMap.Update(this, pos, anim.Image);
		}

		IEnumerable<IRenderable> IEffect.Render(WorldRenderer wr) { yield break; }

		IEnumerable<IRenderable> IEffectAnnotation.RenderAnnotation(WorldRenderer wr)
		{
			var screenPos = wr.Viewport.WorldToViewPx(wr.ScreenPxPosition(pos));
			return anim.RenderUI(wr, screenPos, WVec.Zero, 0, wr.Palette(palette), 1f);
		}
	}