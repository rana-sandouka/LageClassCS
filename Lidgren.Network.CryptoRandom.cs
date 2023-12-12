	public class CryptoRandom : NetRandom
	{
		/// <summary>
		/// Global instance of CryptoRandom
		/// </summary>
		public static new readonly CryptoRandom Instance = new CryptoRandom();

		private RandomNumberGenerator m_rnd = new RNGCryptoServiceProvider();

		/// <summary>
		/// Seed in CryptoRandom does not create deterministic sequences
		/// </summary>
		[CLSCompliant(false)]
		public override void Initialize(uint seed)
		{
			byte[] tmp = new byte[seed % 16];
			m_rnd.GetBytes(tmp); // just prime it
		}

		/// <summary>
		/// Generates a random value from UInt32.MinValue to UInt32.MaxValue, inclusively
		/// </summary>
		[CLSCompliant(false)]
		public override uint NextUInt32()
		{
			var bytes = new byte[4];
			m_rnd.GetBytes(bytes);
			return (uint)bytes[0] | (((uint)bytes[1]) << 8) | (((uint)bytes[2]) << 16) | (((uint)bytes[3]) << 24);
		}

		/// <summary>
		/// Fill the specified buffer with random values
		/// </summary>
		public override void NextBytes(byte[] buffer)
		{
			m_rnd.GetBytes(buffer);
		}

		/// <summary>
		/// Fills all bytes from offset to offset + length in buffer with random values
		/// </summary>
		public override void NextBytes(byte[] buffer, int offset, int length)
		{
			var bytes = new byte[length];
			m_rnd.GetBytes(bytes);
			Array.Copy(bytes, 0, buffer, offset, length);
		}
	}