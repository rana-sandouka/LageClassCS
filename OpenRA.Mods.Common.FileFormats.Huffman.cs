	class Huffman
	{
		public short[] Count; // number of symbols of each length
		public short[] Symbol; // canonically ordered symbols

		public Huffman(byte[] rep, int n, short symbolCount)
		{
			var length = new short[256]; // code lengths
			var s = 0; // current symbol

			// convert compact repeat counts into symbol bit length list
			foreach (var code in rep)
			{
				var num = (code >> 4) + 1; // Number of codes (top four bits plus 1)
				var len = (byte)(code & 15); // Code length (low four bits)
				do
					length[s++] = len;
				while (--num > 0);
			}

			n = s;

			// count number of codes of each length
			Count = new short[Blast.MAXBITS + 1];
			for (var i = 0; i < n; i++)
				Count[length[i]]++;

			// no codes!
			if (Count[0] == n)
				return;

			// check for an over-subscribed or incomplete set of lengths
			var left = 1; // one possible code of zero length
			for (var len = 1; len <= Blast.MAXBITS; len++)
			{
				left <<= 1;	// one more bit, double codes left
				left -= Count[len];	// deduct count from possible codes
				if (left < 0)
					throw new InvalidDataException("over subscribed code set");
			}

			// generate offsets into symbol table for each length for sorting
			var offs = new short[Blast.MAXBITS + 1];
			for (var len = 1; len < Blast.MAXBITS; len++)
				offs[len + 1] = (short)(offs[len] + Count[len]);

			// put symbols in table sorted by length, by symbol order within each length
			Symbol = new short[symbolCount];
			for (short i = 0; i < n; i++)
				if (length[i] != 0)
					Symbol[offs[length[i]]++] = i;
		}
	}