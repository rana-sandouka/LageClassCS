	public class ShortcutExit : Exit {
		private byte id;
    private bool broken;

    public override void PostInit() {
      if (!Engine.EditingLevel && GlobalSave.IsFalse($"shortcut_{id}")) {
        broken = true;
      }
      
      base.PostInit();
    }

    protected override string GetSlice() {
      return id == 0 ? base.GetSlice() : (broken ? $"shortcut_broken_{id}" : $"shortcut_{id}");
    }

    protected override bool CanUse() {
      return !broken;
    }

    protected override void Descend() {
      Run.StartNew(id);
    }

    protected override string GetFxText() {
      return Locale.Get(broken ? "shortcut_is_broken" : BiomeRegistry.GenerateForDepth(id).Id);
    }

    public override void Load(FileReader stream) {
      base.Load(stream);
      id = stream.ReadByte();
    }

    public override void Save(FileWriter stream) {
      base.Save(stream);
      stream.WriteByte(id);
    }

    public override void RenderImDebug() {
      base.RenderImDebug();
      var v = (int) id;

      if (ImGui.InputInt("To depth", ref v)) {
    	  id = (byte) v;
      }
    }

    public override void Render() {
      base.Render();

      if (Engine.EditingLevel) {
    	  Graphics.Print(id.ToString(), Font.Small, Position);
      }
    }
	}