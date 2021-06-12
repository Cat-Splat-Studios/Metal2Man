using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BuildPart {
    public EPartName name;
    public GameObject prefab;
}

public class BuildLibrary : MonoBehaviour
{
    public BuildPart[] parts;

    private readonly Dictionary<EPartName, GameObject> library = new Dictionary<EPartName, GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var buildPart in parts)
        {
            library.Add(buildPart.name, buildPart.prefab);
        }
    }

    public GameObject GetBuildObject(EPartName partName)
    {
        return library.ContainsKey(partName) ? library[partName] : null;
    }
}
