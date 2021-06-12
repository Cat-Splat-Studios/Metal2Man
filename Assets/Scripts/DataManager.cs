using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// - Adding data to the manager
/// - Accessing the data from the manager
///  (Example of use: A station needs to know if player is pressing interact button; instead of GetComponent<Player>().IsInteracting
/// with the Data Manager it would be DataManager.MakeItRain<Player>(DataKeys.LOCALPLAYER).isInteracting.
/// In order to achieve this, there are two steps:
/// 1: Set up a data key entry to your class
/// 2: Register the class to the data manager in your Awake() with DataManager.ToTheCloud(newDataKey, this). 
/// </summary>
public static class DataManager
{
    private static Dictionary<string, object> _data = new Dictionary<string, object>();

    //Register the class to the cloud
    public static void ToTheCloud(string key, object value)
    {
        if(_data.ContainsKey(key))
        {
            _data[key] = value;
        }
        else
        {
            _data.Add(key, value);
        }
    }
    //Get registered class from the class
    public static AnyType MakeItRain<AnyType>(string key)
    {
        if(_data.ContainsKey(key))
        {
            return (AnyType) _data[key];
        }

        return default;
    }
}
