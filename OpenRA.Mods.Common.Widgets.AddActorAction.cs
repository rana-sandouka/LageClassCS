	class AddActorAction : IEditorAction
	{
		public string Text { get; private set; }

		readonly EditorActorLayer editorLayer;
		readonly ActorReference actor;

		EditorActorPreview editorActorPreview;

		public AddActorAction(EditorActorLayer editorLayer, ActorReference actor)
		{
			this.editorLayer = editorLayer;

			// Take an immutable copy of the reference
			this.actor = actor.Clone();
		}

		public void Execute()
		{
			Do();
		}

		public void Do()
		{
			editorActorPreview = editorLayer.Add(actor);
			Text = "Added {0} ({1})".F(editorActorPreview.Info.Name, editorActorPreview.ID);
		}

		public void Undo()
		{
			editorLayer.Remove(editorActorPreview);
		}
	}