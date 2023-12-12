	[ScriptGlobal("Angle")]
	public class AngleGlobal : ScriptGlobal
	{
		public AngleGlobal(ScriptContext context)
			: base(context) { }

		public WAngle North { get { return WAngle.Zero; } }
		public WAngle NorthWest { get { return new WAngle(128); } }
		public WAngle West { get { return new WAngle(256); } }
		public WAngle SouthWest { get { return new WAngle(384); } }
		public WAngle South { get { return new WAngle(512); } }
		public WAngle SouthEast { get { return new WAngle(640); } }
		public WAngle East { get { return new WAngle(768); } }
		public WAngle NorthEast { get { return new WAngle(896); } }

		[Desc("Create an arbitrary angle.")]
		public WAngle New(int a) { return new WAngle(a); }
	}