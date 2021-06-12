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
    protected Player _currentPlayer;
    protected virtual void OnTriggerStay(Collider other)
    {
	    if(other.tag.Contains("Player"))
	    {
		    _currentPlayer = DataManager.MakeItRain<Player>(other.tag);
		    if(_currentPlayer.IsInteracting)
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
