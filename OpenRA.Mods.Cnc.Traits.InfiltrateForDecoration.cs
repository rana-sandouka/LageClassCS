	class InfiltrateForDecoration : WithDecoration, INotifyInfiltrated
	{
		readonly HashSet<Player> infiltrators = new HashSet<Player>();
		readonly InfiltrateForDecorationInfo info;

		public InfiltrateForDecoration(Actor self, InfiltrateForDecorationInfo info)
			: base(self, info)
		{
			this.info = info;
		}

		void INotifyInfiltrated.Infiltrated(Actor self, Actor infiltrator, BitSet<TargetableType> types)
		{
			if (!info.Types.Overlaps(types))
				return;

			infiltrators.Add(infiltrator.Owner);
		}

		protected override bool ShouldRender(Actor self)
		{
			return infiltrators.Any(i => Info.ValidRelationships.HasStance(i.RelationshipWith(self.World.RenderPlayer)));
		}
	}