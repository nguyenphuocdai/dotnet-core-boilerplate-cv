using AspNetCoreHero.Library.Enum;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AspNetCoreHero.Library.Extensions
{
    public sealed class InsensitiveDictionary<TValue> : ConcurrentDictionary<string, TValue>
    {
        public InsensitiveDictionary() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public InsensitiveDictionary(IDictionary<string, TValue> dictionary)
            : base(dictionary, StringComparer.OrdinalIgnoreCase)
        {
        }

        public void Add(string key, TValue value)
        {
            TryAdd(key, value);
        }

        public new TValue this[string key]
        {
            get
            {
                if (ContainsKey(key))
                {
                    return base[key];
                }
                throw new KeyNotFoundException(string.Format(ResourceEnum.NotFoundKey, key));
            }
        }

        /// <summary>
        ///     Return the default value when key is not found. Don't throw exception.
        /// </summary>
        public TValue GetValue(string key)
        {
            return ContainsKey(key) ? base[key] : default(TValue);
        }

        /// <summary>
        ///     Auto append new key into dictionary if that key is not exist.
        ///     Otherwise update the existing key with new value.
        /// </summary>
        public void SetValue(string key, TValue value)
        {
            if (ContainsKey(key))
            {
                base[key] = value;
            }
            else
            {
                TryAdd(key, value);
            }
        }
    }

}
