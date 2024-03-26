using UnityEngine;

namespace GRIDLabGames.Singleton //Change script to match Player Settings Company to match this
{

    //https://docs.unity3d.com/2022.3/Documentation/ScriptReference/ScriptableSingleton_1-instance.html
    //https://docs.unity3d.com/2022.3/Documentation/ScriptReference/ScriptableSingleton_1.html 
    public class Singleton<T> : MonoBehaviour
        where T : Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objs = FindObjectsOfType(typeof(T)) as T[];
                    if (objs.Length > 0)
                        _instance = objs[0];
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = string.Format("_{0}", typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }
}