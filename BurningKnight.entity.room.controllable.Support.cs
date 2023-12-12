	public class Support : RoomControllable {
		public List<Entity> Supporting = new List<Entity>();
	
		public virtual void Apply(Entity e, float dt) {
			
		}

		public class StartedSupportingEvent : Event {
			public Support Support;
			public Entity Entity;
		}
		
		public class EndedSupportingEvent : Event {
			public Support Support;
			public Entity Entity;
		}
	}