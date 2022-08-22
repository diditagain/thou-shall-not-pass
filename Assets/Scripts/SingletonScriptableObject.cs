using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class SingletonScriptableObjects<T> : ScriptableObject where T :SingletonScriptableObjects<T>
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if(_instance==null)
            {
                T[] results = Resources.LoadAll<T>("");
                if(results==null || results.Length<1)
                {
                    throw new System.Exception("Could not find any singleton scriptable object instances in the resources.");
                }
                else if(results.Length>1)
                {
                    Debug.LogWarning("Multiple instances of the singleton scriptable object found in the resources.");
                }
                _instance = results[0];
            }
            return _instance;
        }
    }
}
