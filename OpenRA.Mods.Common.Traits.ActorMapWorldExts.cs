	public static class ActorMapWorldExts
	{
		public static Dictionary<int, ICustomMovementLayer> GetCustomMovementLayers(this World world)
		{
			return ((ActorMap)world.ActorMap).CustomMovementLayers;
		}
	}