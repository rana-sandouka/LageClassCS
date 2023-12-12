	public class IsometricSelectable : IMouseBounds, ISelectable
	{
		readonly IsometricSelectableInfo info;
		readonly string selectionClass = null;
		readonly BuildingInfo buildingInfo;

		public IsometricSelectable(Actor self, IsometricSelectableInfo info)
		{
			this.info = info;
			selectionClass = string.IsNullOrEmpty(info.Class) ? self.Info.Name : info.Class;
			buildingInfo = self.Info.TraitInfo<BuildingInfo>();
		}

		Polygon Bounds(Actor self, WorldRenderer wr, int[] bounds, int height)
		{
			int2 left, right, top, bottom;
			if (bounds != null)
			{
				var offset = bounds.Length >= 4 ? new int2(bounds[2], bounds[3]) : int2.Zero;
				var center = wr.ScreenPxPosition(self.CenterPosition) + offset;
				left = center - new int2(bounds[0] / 2, 0);
				right = left + new int2(bounds[0], 0);
				top = center - new int2(0, bounds[1] / 2);
				bottom = top + new int2(0, bounds[1]);
			}
			else
			{
				var xMin = int.MaxValue;
				var xMax = int.MinValue;
				var yMin = int.MaxValue;
				var yMax = int.MinValue;
				foreach (var c in buildingInfo.OccupiedTiles(self.Location))
				{
					xMin = Math.Min(xMin, c.X);
					xMax = Math.Max(xMax, c.X);
					yMin = Math.Min(yMin, c.Y);
					yMax = Math.Max(yMax, c.Y);
				}

				left = wr.ScreenPxPosition(self.World.Map.CenterOfCell(new CPos(xMin, yMax)) - new WVec(768, 0, 0));
				right = wr.ScreenPxPosition(self.World.Map.CenterOfCell(new CPos(xMax, yMin)) + new WVec(768, 0, 0));
				top = wr.ScreenPxPosition(self.World.Map.CenterOfCell(new CPos(xMin, yMin)) - new WVec(0, 768, 0));
				bottom = wr.ScreenPxPosition(self.World.Map.CenterOfCell(new CPos(xMax, yMax)) + new WVec(0, 768, 0));
			}

			if (height == 0)
				return new Polygon(new[] { top, left, bottom, right });

			var h = new int2(0, height);
			return new Polygon(new[] { top - h, left - h, left, bottom, right, right - h });
		}

		public Polygon Bounds(Actor self, WorldRenderer wr)
		{
			return Bounds(self, wr, info.Bounds, info.Height);
		}

		public Polygon DecorationBounds(Actor self, WorldRenderer wr)
		{
			return Bounds(self, wr, info.DecorationBounds ?? info.Bounds, info.DecorationHeight >= 0 ? info.DecorationHeight : info.Height);
		}

		Polygon IMouseBounds.MouseoverBounds(Actor self, WorldRenderer wr)
		{
			return Bounds(self, wr);
		}

		string ISelectable.Class { get { return selectionClass; } }
	}