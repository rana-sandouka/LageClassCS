    public class FastRandom
    {
        private const double int_to_real = 1.0 / (int.MaxValue + 1.0);
        private const uint int_mask = 0x7FFFFFFF;
        private const uint y_initial = 842502087;
        private const uint z_initial = 3579807591;
        private const uint w_initial = 273326509;
        private uint x, y = y_initial, z = z_initial, w = w_initial;

        public FastRandom(int seed)
        {
            x = (uint)seed;
        }

        public FastRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Generates a random unsigned integer within the range [<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>).
        /// </summary>
        /// <returns>The random value.</returns>
        public uint NextUInt()
        {
            uint t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;
            return w = w ^ (w >> 19) ^ t ^ (t >> 8);
        }

        /// <summary>
        /// Generates a random integer value within the range [0, <see cref="int.MaxValue"/>).
        /// </summary>
        /// <returns>The random value.</returns>
        public int Next() => (int)(int_mask & NextUInt());

        /// <summary>
        /// Generates a random integer value within the range [0, <paramref name="upperBound"/>).
        /// </summary>
        /// <param name="upperBound">The upper bound.</param>
        /// <returns>The random value.</returns>
        public int Next(int upperBound) => (int)(NextDouble() * upperBound);

        /// <summary>
        /// Generates a random integer value within the range [<paramref name="lowerBound"/>, <paramref name="upperBound"/>).
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range.</param>
        /// <param name="upperBound">The upper bound of the range.</param>
        /// <returns>The random value.</returns>
        public int Next(int lowerBound, int upperBound) => (int)(lowerBound + NextDouble() * (upperBound - lowerBound));

        /// <summary>
        /// Generates a random integer value within the range [<paramref name="lowerBound"/>, <paramref name="upperBound"/>).
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range.</param>
        /// <param name="upperBound">The upper bound of the range.</param>
        /// <returns>The random value.</returns>
        public int Next(double lowerBound, double upperBound) => (int)(lowerBound + NextDouble() * (upperBound - lowerBound));

        /// <summary>
        /// Generates a random double value within the range [0, 1).
        /// </summary>
        /// <returns>The random value.</returns>
        public double NextDouble() => int_to_real * Next();

        private uint bitBuffer;
        private int bitIndex = 32;

        /// <summary>
        /// Generates a reandom boolean value. Cached such that a random value is only generated once in every 32 calls.
        /// </summary>
        /// <returns>The random value.</returns>
        public bool NextBool()
        {
            if (bitIndex == 32)
            {
                bitBuffer = NextUInt();
                bitIndex = 1;

                return (bitBuffer & 1) == 1;
            }

            bitIndex++;
            return ((bitBuffer >>= 1) & 1) == 1;
        }
    }