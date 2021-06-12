using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ComponentInfo {
    public EPartType type;
    public EPartName name;
    public GameObject ComponentCardPrefab;
}


[CreateAssetMenu(fileName = "New Order Template", menuName = "Order Template")]
[System.Serializable]
public class Order : ScriptableObject
{
    public string robotName;
    public GameObject robotPrefab;
    public float buildTime;
    public Sprite robotImage;
    public ComponentInfo[] components;
}
