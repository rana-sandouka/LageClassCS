	public sealed class FreeTypeFont : IFont
	{
		static readonly FontGlyph EmptyGlyph = new FontGlyph
		{
			Offset = int2.Zero,
			Size = new Size(0, 0),
			Advance = 0,
			Data = null
		};

		static IntPtr library = IntPtr.Zero;
		readonly GCHandle faceHandle;
		readonly IntPtr face;
		bool disposed;

		public FreeTypeFont(byte[] data)
		{
			if (library == IntPtr.Zero && FreeType.FT_Init_FreeType(out library) != FreeType.OK)
				throw new InvalidOperationException("Failed to initialize FreeType");

			faceHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			if (FreeType.FT_New_Memory_Face(library, faceHandle.AddrOfPinnedObject(), data.Length, 0, out face) != FreeType.OK)
				throw new InvalidDataException("Failed to initialize font");
		}

		public FontGlyph CreateGlyph(char c, int size, float deviceScale)
		{
			var scaledSize = (uint)(size * deviceScale);
			if (FreeType.FT_Set_Pixel_Sizes(face, scaledSize, scaledSize) != FreeType.OK)
				return EmptyGlyph;

			if (FreeType.FT_Load_Char(face, c, FreeType.FT_LOAD_RENDER) != FreeType.OK)
				return EmptyGlyph;

			// Extract the glyph data we care about
			// HACK: This uses raw pointer offsets to avoid defining structs and types that are 95% unnecessary
			var glyph = Marshal.ReadIntPtr(IntPtr.Add(face, FreeType.FaceRecGlyphOffset)); // face->glyph

			var metrics = IntPtr.Add(glyph, FreeType.GlyphSlotMetricsOffset); // face->glyph->metrics
			var metricsWidth = Marshal.ReadIntPtr(IntPtr.Add(metrics, FreeType.MetricsWidthOffset)); // face->glyph->metrics.width
			var metricsHeight = Marshal.ReadIntPtr(IntPtr.Add(metrics, FreeType.MetricsHeightOffset)); // face->glyph->metrics.width
			var metricsAdvance = Marshal.ReadIntPtr(IntPtr.Add(metrics, FreeType.MetricsAdvanceOffset)); // face->glyph->metrics.horiAdvance

			var bitmap = IntPtr.Add(glyph, FreeType.GlyphSlotBitmapOffset); // face->glyph->bitmap
			var bitmapPitch = Marshal.ReadInt32(IntPtr.Add(bitmap, FreeType.BitmapPitchOffset)); // face->glyph->bitmap.pitch
			var bitmapBuffer = Marshal.ReadIntPtr(IntPtr.Add(bitmap, FreeType.BitmapBufferOffset)); // face->glyph->bitmap.buffer

			var bitmapLeft = Marshal.ReadInt32(IntPtr.Add(glyph, FreeType.GlyphSlotBitmapLeftOffset)); // face->glyph.bitmap_left
			var bitmapTop = Marshal.ReadInt32(IntPtr.Add(glyph, FreeType.GlyphSlotBitmapTopOffset)); // face->glyph.bitmap_top

			// Convert FreeType's 26.6 fixed point format to integers by discarding fractional bits
			var glyphSize = new Size((int)metricsWidth >> 6, (int)metricsHeight >> 6);
			var glyphAdvance = (int)metricsAdvance >> 6;

			var g = new FontGlyph
			{
				Advance = glyphAdvance,
				Offset = new int2(bitmapLeft, -bitmapTop),
				Size = glyphSize,
				Data = new byte[glyphSize.Width * glyphSize.Height]
			};

			unsafe
			{
				var p = (byte*)bitmapBuffer;
				var k = 0;
				for (var j = 0; j < glyphSize.Height; j++)
				{
					for (var i = 0; i < glyphSize.Width; i++)
						g.Data[k++] = p[i];

					p += bitmapPitch;
				}
			}

			return g;
		}

		public void Dispose()
		{
			if (!disposed)
			{
				if (faceHandle.IsAllocated)
				{
					FreeType.FT_Done_Face(face);

					faceHandle.Free();
					disposed = true;
				}
			}
		}
	}