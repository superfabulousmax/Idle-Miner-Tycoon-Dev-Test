using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/**
 * Reload proof singleton using scriptable objects
 * */
public abstract class GenericSingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{

    private static T _my_instance = null;

    public static T Instance
    {
        get
        {
            if (!_my_instance)
            {
                _my_instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            }
            if (!_my_instance)
            {
                // could not find so create
                _my_instance = CreateInstance<T>();
            }
            DontDestroyOnLoad(_my_instance);
            return _my_instance;
        }
    }
}
