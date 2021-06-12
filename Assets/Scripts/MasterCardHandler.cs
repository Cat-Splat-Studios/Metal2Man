using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterCardHandler : MonoBehaviour
{
    //this is our build order card - it holds 3 checks boxes (images) to see if the player has retreived a component
    //Once a player brings 1 of 3 desired components back to the card, the image of that component
    //should show a check mark to indicate that component has been fetched
    //when 3 are done we advance to our next build

    //big picture of final product
    public Image FinalDisplay;
    private Order currentOrder;

    public Image[] Robo_Components = new Image[3];

    public GameObject testCube;

    private GameObject armComp = null;
    private GameObject bodyComp = null;
    private GameObject bottomComp = null;

    public Component testComponent;


    public float yOffset = 0.25f;
    private void Start()
    {
        //if we need a reference to currentOrder
        currentOrder = DataManager.MakeItRain<AssemblyTray>(DataKeys.ASSEMBLYTRAY).currentOrder;

        if (!currentOrder)
            Debug.LogError("Current order not found");
        //ComponentRetrieved(1, testCube);
        if (currentOrder)
            AddComponentToUI(testComponent);
    }

    public void AddComponentToUI(Component component)
    {
        switch (component.partType)
        {
            case EPartType.Arms:
                if (armComp) return; //if we already have it dont do anyfing

                if (component.partName == currentOrder.armPart) //make sure the part is the one on the order
                {
                    component.transform.position = SetComponentWorldPos(Robo_Components[0].transform); //set compo to the set position on the canvas
                    armComp = component.gameObject;
                }
                break;
            
            case EPartType.Body:
                if (bodyComp) return; //if we already have it dont do anyfing

                if (component.partName == currentOrder.bodyPart)
                {
                    component.transform.position = SetComponentWorldPos(Robo_Components[1].transform); //set compo to the set position on the canvas
                    bodyComp = component.gameObject;
                }
                break;

            case EPartType.Bottom:
                if (bottomComp) return; //if we already have it dont do anyfing

                if (component.partName == currentOrder.bottomPart)
                {
                    component.transform.position = SetComponentWorldPos(Robo_Components[2].transform); //set compo to the set position on the canvas
                    bottomComp = component.gameObject;
                }
                break;
   
        }

        CheckOrderProgress();
    }

    private Vector3 SetComponentWorldPos(Transform ImageLocation)
    {
        Vector3 newPos = new Vector3(ImageLocation.position.x, ImageLocation.position.y + yOffset, ImageLocation.position.z);
        return newPos;
    }
    private void CheckOrderProgress()
    {
        if (armComp && bodyComp && bottomComp)
        {
            //if all our components are not null -> they have been placed.
            Debug.Log("Order Completed");
            ClearComponentsOnComplete();
        }    
    }

    private void ClearComponentsOnComplete()
    {
        armComp = null;
        bodyComp = null;
        bottomComp = null;
    }

}
