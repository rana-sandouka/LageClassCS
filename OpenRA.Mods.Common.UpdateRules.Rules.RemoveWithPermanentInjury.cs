	public class RemoveWithPermanentInjury : UpdateRule
	{
		public override string Name { get { return "WithPermanentInjury trait has been removed."; } }
		public override string Description
		{
			get
			{
				return "The WithPermanentInjury trait has been removed, and should be replaced by\n" +
				       "TakeCover with negative ProneTime value + GrantConditionOnDamageState/-Health.\n" +
				       "Affected actors are listed so that these traits can be defined.";
			}
		}

		readonly List<string> locations = new List<string>();

		public override IEnumerable<string> AfterUpdate(ModData modData)
		{
			if (locations.Any())
				yield return "The WithPermanentInjury trait has been removed from the following actors.\n" +
				             "You must manually define TakeCover with a negative ProneTime and use\n" +
				             "GrantConditionOnDamageState/-Health with 'GrantPermanently: true'\n" +
							 "to enable TakeCover at the desired damage state:\n" +
				             UpdateUtils.FormatMessageList(locations);

			locations.Clear();
		}

		public override IEnumerable<string> UpdateActorNode(ModData modData, MiniYamlNode actorNode)
		{
			if (actorNode.RemoveNodes("WithPermanentInjury") > 0)
				locations.Add("{0} ({1})".F(actorNode.Key, actorNode.Location.Filename));

			yield break;
		}
	}