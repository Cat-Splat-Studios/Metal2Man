using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StationInfo",menuName = "StationInfo")]
public class WorkStationInfo : ScriptableObject
{
    
    public List<WorkOrder> Orders;
    public float ProcessDuration;
}
[Serializable]
public class WorkOrder
{
    public List<EMaterialTypes> RequiredMaterials;
    public GameObject ProcessedResultPrefab;
    public EOrderTypes OrderType;
}
