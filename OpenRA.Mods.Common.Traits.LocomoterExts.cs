	public static class LocomoterExts
	{
		public static bool HasCellFlag(this CellFlag c, CellFlag cellFlag)
		{
			// PERF: Enum.HasFlag is slower and requires allocations.
			return (c & cellFlag) == cellFlag;
		}

		public static bool HasMovementType(this MovementType m, MovementType movementType)
		{
			// PERF: Enum.HasFlag is slower and requires allocations.
			return (m & movementType) == movementType;
		}
	}