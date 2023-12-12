    public class SeriesSortNameComparer : IBaseItemComparer
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => ItemSortBy.SeriesSortName;

        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>System.Int32.</returns>
        public int Compare(BaseItem x, BaseItem y)
        {
            return string.Compare(GetValue(x), GetValue(y), StringComparison.CurrentCultureIgnoreCase);
        }

        private static string GetValue(BaseItem item)
        {
            var hasSeries = item as IHasSeries;

            return hasSeries?.FindSeriesSortName();
        }
    }