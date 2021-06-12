using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Order Template", menuName = "Order Template")]
[System.Serializable]
public class Order : ScriptableObject
{
    public string robotName;
    public EPartName armPart;
    public EPartName bodyPart;
    public EPartName bottomPart;
}
