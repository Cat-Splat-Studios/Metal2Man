using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudComponents : MonoBehaviour
{
    public GameObject leftSection;
    public GameObject rightSection;

    public GameObject cardPrefabs;

    private List<ComponentCard> leftSectionComponents = new List<ComponentCard>();
    
    private List<ComponentCard> rightSectionComponents = new List<ComponentCard>();

    public void populateLeftSecion(ComponentInfo [] componentInfo)
    {
        
    }

    public void populateRightSection(ComponentInfo [] componentInfo)
    {
        
    }

}
