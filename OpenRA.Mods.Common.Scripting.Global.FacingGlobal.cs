	[ScriptGlobal("Facing")]
	public class FacingGlobal : ScriptGlobal
	{
		public FacingGlobal(ScriptContext context)
			: base(context) { }

		void Deprecated()
		{
			Game.Debug("The Facing table is deprecated. Use Angle instead.");
		}

		public int North { get { Deprecated(); return 0; } }
		public int NorthWest { get { Deprecated(); return 32; } }
		public int West { get { Deprecated(); return 64; } }
		public int SouthWest { get { Deprecated(); return 96; } }
		public int South { get { Deprecated(); return 128; } }
		public int SouthEast { get { Deprecated(); return 160; } }
		public int East { get { Deprecated(); return 192; } }
		public int NorthEast { get { Deprecated(); return 224; } }
	}