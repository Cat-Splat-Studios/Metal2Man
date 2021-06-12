using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardStation : BaseStation
{
	
	private Player _player;
	protected override void StationAction()
	{
		if(!_canInteract) return;
		if(!_player)
		{
			_player = DataManager.MakeItRain<Player>(DataKeys.LOCALPLAYER);
		}

		if(_player.IsHoldingItem)
		{
			var itemToKill = _player.HoldItemPosition.GetChild(0).gameObject;

			StartCoroutine(LerpObjectFromPlayer(itemToKill));
		}
	}

	protected IEnumerator LerpObjectFromPlayer(GameObject objectToLerp)
	{
		objectToLerp.transform.position = PlacementPosition.position;
		yield return new WaitForSeconds(1);
		Destroy(objectToLerp);
		_player.IsHoldingItem = false;
	}
}
