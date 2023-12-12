	public class Armor : ConditionalTrait<ArmorInfo>
	{
		public Armor(Actor self, ArmorInfo info)
			: base(info) { }
	}