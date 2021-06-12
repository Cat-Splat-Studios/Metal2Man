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

    public Image[] Req_Components = new Image[3];

    public GameObject testCube;

    private void Start()
    {
        ComponentRetrieved(1, testCube);
    }


    public void ComponentRetrieved(int cardIndex, GameObject Component)
    {
       if (Req_Components[cardIndex-1] != null)
       {
            MaterialHandler.ImageColorChanger(Req_Components[cardIndex-1], Color.black);

            Vector3 componentPos = Req_Components[cardIndex - 1].transform.position;
            testCube.transform.position = new Vector3(componentPos.x, componentPos.y + 0.55f, componentPos.z);
       }
    }
}
