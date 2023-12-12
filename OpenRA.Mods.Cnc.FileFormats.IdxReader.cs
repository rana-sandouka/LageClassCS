	public class IdxReader
	{
		public readonly int SoundCount;
		public List<IdxEntry> Entries;

		public IdxReader(Stream s)
		{
			s.Seek(0, SeekOrigin.Begin);

			var id = s.ReadASCII(4);

			if (id != "GABA")
				throw new InvalidDataException("Unable to load Idx file, did not find magic id, found {0} instead".F(id));

			var two = s.ReadInt32();

			if (two != 2)
				throw new InvalidDataException("Unable to load Idx file, did not find magic number 2, found {0} instead".F(two));

			SoundCount = s.ReadInt32();

			Entries = new List<IdxEntry>();

			for (var i = 0; i < SoundCount; i++)
				Entries.Add(new IdxEntry(s));
		}
	}