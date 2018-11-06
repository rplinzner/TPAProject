using System.Collections.Generic;

namespace BusinessLogic.Model
{
    public sealed class DictionarySingleton
    {
        #region Static members
        private static DictionarySingleton instance = new DictionarySingleton();

        public static DictionarySingleton Instance
        {
            get { return instance; }
        }
        
        #endregion
        #region Instance members
        private Dictionary<string, TypeMetadata> _data = new Dictionary<string, TypeMetadata>();
        private DictionarySingleton()
        {
            //TODO: LOG THAT SINGLETON IS CREATED
        }

        public void Add(string name, TypeMetadata type)
        {
            _data.Add(name,type);
        }

        public bool ContainsKey(string name)
        {
            return _data.ContainsKey(name);
        }

        public TypeMetadata Get(string key)
        {
            TypeMetadata value;
            _data.TryGetValue(key, out value);
            return value;
        }
        #endregion
    }
}