	public class DebugVisualizationCommands : IChatCommand, IWorldLoaded
	{
		DebugVisualizations debugVis;
		DeveloperMode devMode;

		public void WorldLoaded(World w, WorldRenderer wr)
		{
			var world = w;
			debugVis = world.WorldActor.TraitOrDefault<DebugVisualizations>();

			if (world.LocalPlayer != null)
				devMode = world.LocalPlayer.PlayerActor.Trait<DeveloperMode>();

			if (debugVis == null)
				return;

			var console = world.WorldActor.Trait<ChatCommands>();
			var help = world.WorldActor.Trait<HelpCommand>();

			Action<string, string> register = (name, helpText) =>
			{
				console.RegisterCommand(name, this);
				help.RegisterHelp(name, helpText);
			};

			register("showcombatgeometry", "toggles combat geometry overlay.");
			register("showrendergeometry", "toggles render geometry overlay.");
			register("showscreenmap", "toggles screen map overlay.");
			register("showdepthbuffer", "toggles depth buffer overlay.");
			register("showactortags", "toggles actor tags overlay.");
		}

		public void InvokeCommand(string name, string arg)
		{
			switch (name)
			{
				case "showcombatgeometry":
					debugVis.CombatGeometry ^= true;
					break;

				case "showrendergeometry":
					debugVis.RenderGeometry ^= true;
					break;

				case "showscreenmap":
					if (devMode == null || devMode.Enabled)
						debugVis.ScreenMap ^= true;
					break;

				case "showdepthbuffer":
					debugVis.DepthBuffer ^= true;
					break;

				case "showactortags":
					debugVis.ActorTags ^= true;
					break;
			}
		}
	}