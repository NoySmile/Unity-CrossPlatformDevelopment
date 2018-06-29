using System.Linq;
using UnityEngine;

namespace CrossPlatformDevelopment.ScriptableObject.GameState
{
    public class ScriptableSingleton<T> : UnityEngine.ScriptableObject where T : UnityEngine.ScriptableObject
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                    _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                if(!_instance)
                    _instance = CreateInstance<T>();
                return _instance;
            }
        }
    }
}