	public class DelayedImpact : IEffect
	{
		readonly Target target;
		readonly IWarhead wh;
		readonly WarheadArgs args;

		int delay;

		public DelayedImpact(int delay, IWarhead wh, Target target, WarheadArgs args)
		{
			this.wh = wh;
			this.delay = delay;
			this.target = target;
			this.args = args;
		}

		public void Tick(World world)
		{
			if (--delay <= 0)
				world.AddFrameEndTask(w => { w.Remove(this); wh.DoImpact(target, args); });
		}

		public IEnumerable<IRenderable> Render(WorldRenderer wr) { yield break; }
	}