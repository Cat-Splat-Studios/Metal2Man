using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StationInfo",menuName = "Station/StationInfo")]
public class StationInfo : ScriptableObject
{
    public List<MaterialTypes> RequiredMaterials = new List<MaterialTypes>();
    public GameObject ProcessedResultPrefab;
    public float ProcessDuration;
}
