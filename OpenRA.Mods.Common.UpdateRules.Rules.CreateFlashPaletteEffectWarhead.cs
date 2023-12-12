	public class CreateFlashPaletteEffectWarhead : UpdateRule
	{
		public override string Name { get { return "Create FlashPaletteEffectWarhead to replace hardcoded nuke flashing."; } }

		public override string Description
		{
			get
			{
				return "The trait NukePower (via the NukeLaunch projectile that it uses) no longer has built-in palette flashing.";
			}
		}

		readonly List<Tuple<string, string, string>> weaponsToUpdate = new List<Tuple<string, string, string>>();

		public override IEnumerable<string> UpdateActorNode(ModData modData, MiniYamlNode actorNode)
		{
			var nukePowerTraits = actorNode.ChildrenMatching("NukePower");
			foreach (var nukePowerTrait in nukePowerTraits)
			{
				var traitName = nukePowerTrait.Key;
				var weaponNode = nukePowerTrait.ChildrenMatching("MissileWeapon").FirstOrDefault();
				if (weaponNode == null)
					continue;

				var weaponName = weaponNode.Value.Value;

				weaponsToUpdate.Add(new Tuple<string, string, string>(weaponName, traitName, "{0} ({1})".F(actorNode.Key, actorNode.Location.Filename)));

				nukePowerTrait.RemoveNodes("FlashType");
			}

			yield break;
		}

		public override IEnumerable<string> AfterUpdate(ModData modData)
		{
			if (weaponsToUpdate.Any())
				yield return "Add a FlashPaletteEffectWarhead to the following weapons:\n" +
					UpdateUtils.FormatMessageList(weaponsToUpdate.Select(x => "Weapon `{0}`, used by trait `{1}` on actor {2}".F(x.Item1, x.Item2, x.Item3)));

			weaponsToUpdate.Clear();
		}
	}