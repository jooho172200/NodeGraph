using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Contexts
{
    public sealed class Context
    {
        private readonly Dictionary<string, object> _data = new();

        public void Set<T>(string key, T value) => _data[key] = value!;

        public T Get<T>(string key)
        {
            return _data.TryGetValue(key, out var v) ? (T)v! :
                  throw new System.InvalidOperationException($"Context key not found: '{key}'");
        }

        public bool Has(string key) => _data.ContainsKey(key);

    }
}
