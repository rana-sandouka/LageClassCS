	public class SoundPool
	{
		public readonly float VolumeModifier;
		readonly string[] clips;
		readonly List<string> liveclips = new List<string>();

		public SoundPool(float volumeModifier, params string[] clips)
		{
			VolumeModifier = volumeModifier;
			this.clips = clips;
		}

		public string GetNext()
		{
			if (liveclips.Count == 0)
				liveclips.AddRange(clips);

			// Avoid crashing if there's no clips at all
			if (liveclips.Count == 0)
				return null;

			var i = Game.CosmeticRandom.Next(liveclips.Count);
			var s = liveclips[i];
			liveclips.RemoveAt(i);
			return s;
		}
	}