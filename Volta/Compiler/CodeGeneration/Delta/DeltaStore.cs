using System.Collections.Generic;

namespace Volta.Compiler.CodeGeneration.Delta
{
    public class DeltaStore
    {
        private Dictionary<string, dynamic> data;

        public DeltaStore(string name) {
            data = new Dictionary<string, dynamic>();
            Name = name;
        }

        public string Name { get; private set; }

        #region Management

        public void AddValue(string key, dynamic value) {
            //data.Add(key, value);
        }

        public bool ExistsValue(string key)
            => data.ContainsKey(key);

        public dynamic GetValue(string key) {
            return data.ContainsKey(key) ? data[key] : null;
        }

        public void UpdateValue(string key, dynamic newValue) {
            if (ExistsValue(key))
                data[key] = newValue;
        }

        #endregion
    }
}