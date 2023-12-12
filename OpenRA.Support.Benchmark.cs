	class Benchmark
	{
		readonly string prefix;
		readonly Dictionary<string, List<BenchmarkPoint>> samples = new Dictionary<string, List<BenchmarkPoint>>();

		public Benchmark(string prefix)
		{
			this.prefix = prefix;
		}

		public void Tick(int localTick)
		{
			foreach (var item in PerfHistory.Items)
				samples.GetOrAdd(item.Key).Add(new BenchmarkPoint(localTick, item.Value.LastValue));
		}

		class BenchmarkPoint
		{
			public int Tick { get; private set; }
			public double Value { get; private set; }

			public BenchmarkPoint(int tick, double value)
			{
				Tick = tick;
				Value = value;
			}
		}

		public void Write()
		{
			foreach (var sample in samples)
			{
				var name = sample.Key;
				Log.AddChannel(name, "{0}{1}.csv".F(prefix, name));
				Log.Write(name, "tick,time [ms]");

				foreach (var point in sample.Value)
					Log.Write(name, "{0},{1}".F(point.Tick,  point.Value));
			}
		}

		public void Reset()
		{
			samples.Clear();
		}
	}