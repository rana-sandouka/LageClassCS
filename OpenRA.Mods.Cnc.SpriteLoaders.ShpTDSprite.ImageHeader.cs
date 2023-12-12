		class ImageHeader : ISpriteFrame
		{
			public SpriteFrameType Type { get { return SpriteFrameType.Indexed; } }
			public Size Size { get { return reader.Size; } }
			public Size FrameSize { get { return reader.Size; } }
			public float2 Offset { get { return float2.Zero; } }
			public byte[] Data { get; set; }
			public bool DisableExportPadding { get { return false; } }

			public uint FileOffset;
			public Format Format;

			public uint RefOffset;
			public Format RefFormat;
			public ImageHeader RefImage;

			ShpTDSprite reader;

			// Used by ShpWriter
			public ImageHeader() { }

			public ImageHeader(Stream stream, ShpTDSprite reader)
			{
				this.reader = reader;
				var data = stream.ReadUInt32();
				FileOffset = data & 0xffffff;
				Format = (Format)(data >> 24);

				RefOffset = stream.ReadUInt16();
				RefFormat = (Format)stream.ReadUInt16();
			}

			public void WriteTo(BinaryWriter writer)
			{
				writer.Write(FileOffset | ((uint)Format << 24));
				writer.Write((ushort)RefOffset);
				writer.Write((ushort)RefFormat);
			}
		}