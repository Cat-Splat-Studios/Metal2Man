using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Part
{
    public Sprite image;
    public GameObject prefab;
}

[System.Serializable]
public struct BuildPart {
    public EPartName name;
    public Part part;
}

public class BuildLibrary : MonoBehaviour
{
    public BuildPart[] parts;

    private readonly Dictionary<EPartName, Part> library = new Dictionary<EPartName, Part>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var buildPart in parts)
        {
            library.Add(buildPart.name, buildPart.part);
        }
    }

    public GameObject GetBuildObject(EPartName partName)
    {
        return library.ContainsKey(partName) ? library[partName].prefab : null;
    }

    public Sprite GetBuildImage(EPartName partName)
    {
        return library.ContainsKey(partName) ? library[partName].image : null;
    }
}
