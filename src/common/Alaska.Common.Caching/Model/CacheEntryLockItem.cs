using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Common.Caching.Model
{
    internal enum CacheEntryState { Uninitialized, Loading, Ready }

    internal class CacheEntryLockItem
    {
        public CacheEntryLockItem(CacheEntryState initialState)
        {
            State = initialState;
        }

        public CacheEntryState State { get; set; }
    }
}
