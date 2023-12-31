		class R8Frame : ISpriteFrame
		{
			public SpriteFrameType Type { get; set; }
			public Size Size { get; private set; }
			public Size FrameSize { get; private set; }
			public float2 Offset { get; private set; }
			public byte[] Data { get; set; }
			public bool DisableExportPadding { get { return true; } }

			public readonly uint[] Palette = null;

			public R8Frame(Stream s)
			{
				// Scan forward until we find some data
				var type = s.ReadUInt8();
				while (type == 0)
					type = s.ReadUInt8();

				var width = s.ReadInt32();
				var height = s.ReadInt32();
				var x = s.ReadInt32();
				var y = s.ReadInt32();

				Size = new Size(width, height);
				Offset = new int2(width / 2 - x, height / 2 - y);

				/*var imageOffset = */
				s.ReadInt32();
				var paletteOffset = s.ReadInt32();
				var bpp = s.ReadUInt8();
				if (bpp != 8)
					throw new InvalidDataException("Error: {0} bits per pixel are not supported.".F(bpp));

				var frameHeight = s.ReadUInt8();
				var frameWidth = s.ReadUInt8();
				FrameSize = new Size(frameWidth, frameHeight);

				// Skip alignment byte
				s.ReadUInt8();

				Data = s.ReadBytes(width * height);

				// Read palette
				if (type == 1 && paletteOffset != 0)
				{
					// Skip header
					s.ReadUInt32();
					s.ReadUInt32();

					Palette = new uint[256];
					for (var i = 0; i < 256; i++)
					{
						var packed = s.ReadUInt16();
						Palette[i] = (uint)((255 << 24) | ((packed & 0xF800) << 8) | ((packed & 0x7E0) << 5) | ((packed & 0x1f) << 3));
					}
				}
			}
		}