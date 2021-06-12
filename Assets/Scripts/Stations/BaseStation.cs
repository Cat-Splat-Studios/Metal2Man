using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BaseStation : MonoBehaviour
{
    /*
     * Interacts with player
     * Has a virtual function for action
     * Timer to do the action again
     */
    public Transform PlacementPosition;
    public float InteractRate = 1;
    protected bool _canInteract;
    
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

    private void OnEnable()
    {
	    _canInteract = true;
    }

    protected IEnumerator ReplenishDuration()
    {
	    _canInteract = false;
	    yield return new WaitForSeconds(InteractRate);
	    _canInteract = true;
    }

    protected virtual void StationAction()
    {
	    StartCoroutine(ReplenishDuration());
    }
}
