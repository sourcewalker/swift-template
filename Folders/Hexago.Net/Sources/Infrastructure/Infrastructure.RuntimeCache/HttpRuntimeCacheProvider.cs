using Core.Infrastructure.Interfaces.Caching;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Caching;

namespace $safeprojectname$
{
    public sealed class CacheProvider : ICacheProvider
    {
        #region Static Fields

        private static readonly object _syncRoot = new object();

        private static readonly Lazy<CacheProvider> _instance =
            new Lazy<CacheProvider>(
                () => 
                    new CacheProvider(), 
                    LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion

        #region Fields

        private readonly Cache _cache;

        #endregion

        #region Constructors and Destructors

        private CacheProvider()
        {
            _cache = HttpRuntime.Cache;
        }

        #endregion

        #region Public Properties

        public static CacheProvider Current => _instance.Value;

        #endregion

        #region Public Methods and Operators

        public void Add<T>(string key, T data, double expiry = 6) where T : class
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Cannot cache a null value.");
            }

            lock (_syncRoot)
            {
                _cache.Insert(key, data, null, DateTime.Now.AddHours(expiry), TimeSpan.Zero);
            }
        }

        public bool ContainsKey(string key)
        {
            lock (_syncRoot)
            {
                return _cache[key] != null;
            }
        }

        public T Get<T>(string key) where T : class
        {
            lock (_syncRoot)
            {
                return ContainsKey(key) ? _cache[key] as T : default(T);
            }
        }

        public IEnumerable<string> GetAllCacheKeys()
        {
            var enumerator = _cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return enumerator.Key?.ToString();
            }
        }

        public void Purge()
        {
            lock (_syncRoot)
            {
                var enumerator = _cache.GetEnumerator();

                var keys = new List<string>();

                while (enumerator.MoveNext())
                {
                    keys.Add(enumerator.Key?.ToString());
                }

                keys.ForEach(Remove);
            }
        }

        public void Remove(string key)
        {
            lock (_syncRoot)
            {
                if (!ContainsKey(key))
                {
                    return;
                }

                _cache.Remove(key);
            }
        }

        public void RemoveByPrefix(string prefix)
        {
            lock (_syncRoot)
            {
                var enumerator = _cache.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    var key = enumerator.Key?.ToString();

                    if (key != null && key.IndexOf(prefix, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        Remove(key);
                    }
                }
            }
        }

        #endregion
    }
}
