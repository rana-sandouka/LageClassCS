	class EditActorPreview
	{
		readonly EditorActorPreview actor;
		readonly SetActorIdAction setActorIdAction;
		readonly List<IEditActorHandle> handles = new List<IEditActorHandle>();

		public EditActorPreview(EditorActorPreview actor)
		{
			this.actor = actor;
			setActorIdAction = new SetActorIdAction(actor.ID);
			handles.Add(setActorIdAction);
		}

		public bool IsDirty
		{
			get { return handles.Any(h => h.IsDirty); }
		}

		public void SetActorID(string actorID)
		{
			setActorIdAction.Set(actorID);
		}

		public void Add(IEditActorHandle editActor)
		{
			handles.Add(editActor);
		}

		public IEnumerable<IEditActorHandle> GetDirtyHandles()
		{
			return handles.Where(h => h.IsDirty);
		}

		public void Reset()
		{
			foreach (var handle in handles.Where(h => h.IsDirty))
				handle.Undo(actor);
		}
	}