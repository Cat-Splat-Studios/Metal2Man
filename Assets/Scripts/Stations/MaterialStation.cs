using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialStation : BaseStation
{
    public MaterialItem Prefab;
    public Transform Spawnpoint;
    private MaterialItem _spawnedMaterialItem;
    protected override void StationAction()
    {
        if(!Prefab || !Spawnpoint)
        {
            Debug.LogWarning("Material Station isn't set up !! Needs prefab AND spawnpoint");
            return;
        }
        if(!_canInteract) return;
        if(_spawnedMaterialItem && !_spawnedMaterialItem.InPlayerPossession) return;
        
        //Spawn Item then wait to replenish
        _spawnedMaterialItem = Instantiate(Prefab, Spawnpoint.transform.position,Spawnpoint.rotation);
        base.StationAction();
    }
}
