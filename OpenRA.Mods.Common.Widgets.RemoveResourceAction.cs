	class RemoveResourceAction : IEditorAction
	{
		public string Text { get; private set; }

		readonly CellLayer<ResourceTile> mapResources;
		readonly CPos cell;

		ResourceTile resourceTile;

		public RemoveResourceAction(CellLayer<ResourceTile> mapResources, CPos cell, ResourceType type)
		{
			this.mapResources = mapResources;
			this.cell = cell;

			Text = "Removed {0}".F(type.Info.TerrainType);
		}

		public void Execute()
		{
			Do();
		}

		public void Do()
		{
			resourceTile = mapResources[cell];
			mapResources[cell] = default(ResourceTile);
		}

		public void Undo()
		{
			mapResources[cell] = resourceTile;
		}
	}