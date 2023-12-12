		class TmpTDFrame : ISpriteFrame
		{
			public SpriteFrameType Type { get { return SpriteFrameType.Indexed; } }
			public Size Size { get; private set; }
			public Size FrameSize { get; private set; }
			public float2 Offset { get { return float2.Zero; } }
			public byte[] Data { get; set; }
			public bool DisableExportPadding { get { return false; } }

			public TmpTDFrame(byte[] data, Size size)
			{
				FrameSize = size;
				Data = data;

				if (data == null)
					Data = new byte[0];
				else
					Size = size;
			}
		}