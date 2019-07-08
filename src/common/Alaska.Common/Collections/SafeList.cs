using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Common.Collections
{
    public class SafeList<T>
    {
        private List<T> _innerList = new List<T>();

        public void Add(T item)
        {
            lock (this)
            {
                _innerList.Add(item);
            }
        }

        public void Remove(T item)
        {
            lock (this)
            {
                _innerList.Remove(item);
            }
        }

        public void Clear()
        {
            lock (this)
            {
                _innerList.Clear();
            }
        }

        public IEnumerable<T> Values => _innerList;
    }
}
