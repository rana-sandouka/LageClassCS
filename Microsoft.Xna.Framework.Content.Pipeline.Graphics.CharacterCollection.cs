    internal class CharacterCollection : ICollection<char>
    {
        private List<char> _items;

        public CharacterCollection()
        {
            _items = new List<char>();
        }

        public CharacterCollection(IEnumerable<char> characters)
        {
            _items = new List<char>();
            foreach (var c in characters)
                Add(c);
        }

        #region ICollection<char> Members

        public void Add(char item)
        {
            if (!_items.Contains(item))
                _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(char item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(char[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(char item)
        {
            return _items.Remove(item);
        }

        #endregion

        #region IEnumerable<char> Members

        public IEnumerator<char> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion
    }