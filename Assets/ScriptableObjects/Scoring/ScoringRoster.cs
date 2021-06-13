using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoringRoster",menuName = "ScoringRoster")]
public class ScoringRoster : ScriptableObject
{
	public List<ScoreObjectInfo> Roster = new List<ScoreObjectInfo>();
}
[Serializable]
public class ScoreObjectInfo
{
	public EOrderTypes OrderType;
	public float ScoreAmount;
}
