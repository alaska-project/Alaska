﻿using Alaska.Common.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Infrastructure.Caching
{
    public class ContentTypesCache : CacheInstance
    {
        private const string ContentTypeCacheKey = "_content_type_{0}";

        public T RetreiveContentType<T>(string contentTypeId, Func<T> factory)
        {
            return Retreive(string.Format(ContentTypeCacheKey, contentTypeId), factory);
        }
    }
}
