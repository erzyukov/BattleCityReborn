using UnityEngine;

namespace BS
{
    /// <summary>
    /// Базовый класс для синглтонов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool _shuttingDown = false;
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_shuttingDown)
                    return null;

                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }

        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        private void OnDestroy()
        {
            _shuttingDown = true;
        }
    }
}