	public sealed class ModelBone
	{
		private List<ModelBone> children = new List<ModelBone>();
		
		private List<ModelMesh> meshes = new List<ModelMesh>();

		public List<ModelMesh> Meshes {
			get {
				return this.meshes;
			}
			private set {
				meshes = value;
			}
		}

		// Summary:
		//     Gets a collection of bones that are children of this bone.
		public ModelBoneCollection Children { get; private set; }
		//
		// Summary:
		//     Gets the index of this bone in the Bones collection.
		public int Index { get; set; }
		//
		// Summary:
		//     Gets the name of this bone.
		public string Name { get; set; }
		//
		// Summary:
		//     Gets the parent of this bone.
		public ModelBone Parent { get; set; }
		//
		// Summary:
		//     Gets or sets the matrix used to transform this bone relative to its parent
		//     bone.
		internal Matrix transform;
		public Matrix Transform 
		{ 
			get { return this.transform; } 
			set { this.transform = value; }
		}
		
		/// <summary>
		/// Transform of this node from the root of the model not from the parent
		/// </summary>
		public Matrix ModelTransform {
			get;
			set;
		}
		
		public ModelBone ()	
		{
			Children = new ModelBoneCollection(new List<ModelBone>());
		}
		
		public void AddMesh(ModelMesh mesh)
		{
			meshes.Add(mesh);
		}

		public void AddChild(ModelBone modelBone)
		{
			children.Add(modelBone);
			Children = new ModelBoneCollection(children);
		}
	}