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

    public Image[] Robo_Components = new Image[3];

    public GameObject testCube;

    private GameObject armComp = null;
    private GameObject bodyComp = null;
    private GameObject bottomComp = null;

    public Component testComponent;


    public float yOffset = 0.25f;
    private void Start()
    {
        //ComponentRetrieved(1, testCube);
        AddComponentToUI(testComponent);
    }

    public void AddComponentToUI(Component component)
    {
        switch (component.partType)
        {
            case EPartType.Arms:
                if (armComp) return; //if we already have it dont do anyfing
                component.transform.position = SetComponentWorldPos(Robo_Components[1].transform); //set compo to the set position on the canvas
                armComp = component.gameObject;
                break;
            
            case EPartType.Body:
                if (bodyComp) return; //if we already have it dont do anyfing
                component.transform.position = SetComponentWorldPos(Robo_Components[2].transform); //set compo to the set position on the canvas
                bodyComp = component.gameObject;
                break;

            case EPartType.Bottom:
                if (bottomComp) return; //if we already have it dont do anyfing
                component.transform.position = SetComponentWorldPos(Robo_Components[3].transform); //set compo to the set position on the canvas
                bottomComp = component.gameObject;
                break;
        }
    }

    private Vector3 SetComponentWorldPos(Transform ImageLocation)
    {
        Vector3 newPos = new Vector3(ImageLocation.position.x, ImageLocation.position.y + yOffset, ImageLocation.position.z);
        return newPos;
    }

    public void ComponentRetrieved(int cardIndex, GameObject Component)
    {
          if (Robo_Components[cardIndex - 1] != null)
          {
                MaterialHandler.ImageColorChanger(Robo_Components[cardIndex - 1], Color.black);

                Vector3 componentPos = Robo_Components[cardIndex - 1].transform.position;
                testCube.transform.position = new Vector3(componentPos.x, componentPos.y + 0.55f, componentPos.z);
          }
        
    }
}
