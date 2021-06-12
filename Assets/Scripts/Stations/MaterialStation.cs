using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialStation : BaseStation
{
    public MaterialItem Prefab;
    private MaterialItem _spawnedMaterialItem;
    
    protected override void StationAction()
    {
        if(!Prefab || !PlacementPosition)
        {
            Debug.LogWarning("Material Station isn't set up !! Needs prefab AND spawnpoint");
            return;
        }
        if(!_canInteract) return;
        if(_spawnedMaterialItem && !_spawnedMaterialItem.InPlayerPossession) return;
        if(_currentPlayer.IsHoldingItem) return;
        
        //Spawn Item then wait to replenish
        _spawnedMaterialItem = Instantiate(Prefab, PlacementPosition.transform.position,PlacementPosition.rotation);
        base.StationAction();
    }
}
