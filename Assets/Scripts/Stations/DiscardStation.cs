using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardStation : BaseStation
{
	
	protected override void StationAction()
	{
		if(!_canInteract) return;
		
		if(_currentPlayer.IsHoldingItem)
		{
			_currentPlayer.IsHoldingItem = false;
			var itemToKill = _currentPlayer.HoldItemPosition.GetChild(0).gameObject;

			StartCoroutine(LerpObjectFromPlayer(itemToKill));
		}
	}

	private IEnumerator LerpObjectFromPlayer(GameObject objectToLerp)
	{
		objectToLerp.transform.position = PlacementPosition.position;
		objectToLerp.transform.parent = PlacementPosition;
		yield return new WaitForSeconds(0.5f);
		Destroy(objectToLerp);
	}
}
