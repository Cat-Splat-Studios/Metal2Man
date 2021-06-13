using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialStation : BaseStation
{
    public MaterialItem Prefab;


    public Animator anim;
    

    protected override void StationAction()
    {
        if(!Prefab)
        {
            Debug.LogWarning("Material Station isn't set up !! Needs prefab AND spawnpoint");
            return;
        }
        if(!_canInteract) return;
        if(_currentPlayer.IsHoldingItem) return;
        
        //Spawn Item then wait to replenish
        MaterialItem spawnedMaterialItem = Instantiate(Prefab, PlacementPosition.transform.position,PlacementPosition.rotation);
        spawnedMaterialItem.GiveItem(_currentPlayer);
        anim.SetTrigger("Grab");
        base.StationAction();
    }
}
