	public abstract class RuntimeFlagInit : ActorInit, ISuppressInitExport
	{
		public override MiniYaml Save()
		{
			throw new NotImplementedException("RuntimeFlagInit cannot be saved");
		}
	}