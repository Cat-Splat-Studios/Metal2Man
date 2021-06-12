using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudComponents : MonoBehaviour
{
    public GameObject leftSection;
    public GameObject rightSection;
    public GameObject circuitBoardCardPrefab;

    private List<ComponentCard> leftSectionComponents = new List<ComponentCard>();
    
    private List<ComponentCard> rightSectionComponents = new List<ComponentCard>();

    public void populateLeftSecion(ComponentInfo [] componentInfo)
    {
        foreach (var info in componentInfo)
        {
            GameObject cardObj = Instantiate(info.ComponentCardPrefab, leftSection.transform);
            leftSectionComponents.Add(cardObj.GetComponent<ComponentCard>());
        }
        
        GameObject circuitCardObj = Instantiate(circuitBoardCardPrefab, leftSection.transform);
        leftSectionComponents.Add(circuitCardObj.GetComponent<ComponentCard>());
    }

    public void populateRightSection(ComponentInfo [] componentInfo)
    {
        foreach (var info in componentInfo)
        {
            GameObject cardObj =Instantiate(info.ComponentCardPrefab, rightSection.transform);
            rightSectionComponents.Add(cardObj.GetComponent<ComponentCard>());
        }
        
        GameObject circuitCardObj = Instantiate(circuitBoardCardPrefab, rightSection.transform);
        rightSectionComponents.Add(circuitCardObj.GetComponent<ComponentCard>());
    }

    public void removeCardLeftSection(EPartName partName)
    {
        ComponentCard componentCard = leftSectionComponents.Find((card) => card.partName == partName);
        leftSectionComponents.Remove(componentCard);
        componentCard.HideCard();
    }
    
    public void removeCardRightSection(EPartName partName)
    {
        ComponentCard componentCard = rightSectionComponents.Find((card) => card.partName == partName);
        rightSectionComponents.Remove(componentCard);
        componentCard.HideCard();
        
    }

    public void removeallLeftCards()
    {
        foreach (var leftSectionComponent in leftSectionComponents)
        {
            Destroy(leftSectionComponent.gameObject);
        }
        
        leftSectionComponents.Clear();
    }

    public void removeallRightCards()
    {
        foreach (var rightSectionComponent in rightSectionComponents)
        {
            Destroy(rightSectionComponent.gameObject);
        }
        
        rightSectionComponents.Clear();
    }

}
