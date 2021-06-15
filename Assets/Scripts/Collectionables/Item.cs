using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMaterialTypes
{
    Scrap,
    Bolt,
    SiliconBase,
    Wire,
    Metal,
    Ammo,
    PrintedSilicon,
    NULL=20
}

public enum EOrderTypes
{
    Metal,
    Ammo,
    PrintedSilicon,
    MeleeWeapon,
    RangedWeapon,
    MobileBody,
    CircuitBoard,
    NULL=20
}

public class Item : MonoBehaviour
{
    protected bool _inPlayerPossession;
    protected bool _isActive;

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }
    public bool InPlayerPossession
    {
        get => _inPlayerPossession;
        set => _inPlayerPossession = value;
    }
    
    private void OnEnable()
    {
        StartCoroutine(DelayToGrabItem());
    }

    public void GiveItem(Player player)
    {
        if(player.IsHoldingItem) return;
        
        if(player.IsInteracting)
        {
            DataManager.MakeItRain<AudioHandler>(DataKeys.AUDIO)?.PlayAudio(EAudioEvents.PlayerPickUp);
            _inPlayerPossession = true;
            transform.position = player.HoldItemPosition.position;
            transform.parent = player.HoldItemPosition;
            StartCoroutine(DelayToToggleFlag(player));
        }  
    }

    IEnumerator DelayToToggleFlag(Player player)
    {
        yield return new WaitForSeconds(0.5f);
        player.IsHoldingItem = true;
    }

    IEnumerator DelayToGrabItem()
    {
        yield return new WaitForSeconds(0.5f);
        _isActive = true;
    }
}
