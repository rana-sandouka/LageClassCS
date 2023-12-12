		class ShroudSource
		{
			public readonly SourceType Type;
			public readonly PPos[] ProjectedCells;

			public ShroudSource(SourceType type, PPos[] projectedCells)
			{
				Type = type;
				ProjectedCells = projectedCells;
			}
		}