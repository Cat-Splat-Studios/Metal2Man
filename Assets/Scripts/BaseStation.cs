using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStation : MonoBehaviour
{
    /*
     * Interacts with player
     * Has a virtual function for action
     * Timer to do the action again
     */

    protected virtual void OnTriggerStay(Collider other)
    {
	    if(other.CompareTag("Player"))
	    {
		    bool isPressed = DataManager.MakeItRain<Player>(DataKeys.LOCALPLAYER).IsInteracting;
		    if(isPressed)
		    {
			    StationAction();
		    }
	    }
    }

    protected virtual void StationAction()
    {
	    Debug.Log("Interacted!");
    }
}
