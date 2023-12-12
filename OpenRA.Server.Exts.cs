	static class Exts
	{
		public static IEnumerable<T> Except<T>(this IEnumerable<T> ts, T t)
		{
			return ts.Except(new[] { t });
		}
	}