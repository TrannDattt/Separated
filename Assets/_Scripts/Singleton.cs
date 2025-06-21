using UnityEngine;

namespace Separated.Helpers
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        // public static T Instance { get; private set; }

        // protected virtual void Awake()
        // {
        //     if (Instance == null)
        //     {
        //         Instance = this as T;
        //     }
        //     else if (Instance != this)
        //     {
        //         Destroy(gameObject);
        //     }
        // }
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject($"New {typeof(T).Name}");
                        _instance = singletonObject.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                // DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
