using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialTypes{MaterialA,MaterialB,MaterialC}
public class MaterialItem : MonoBehaviour
{
    public MaterialTypes MaterialType;
    private bool _inPlayerPossession;
    private Collider _collider;
    private bool _isActive;

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
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

    private void OnTriggerStay(Collider other)
    {
        if(!_isActive) return;
        if(other.tag.Contains("Player"))
        {
            Player player = DataManager.MakeItRain<Player>(other.tag);
            if(player.IsHoldingItem) return;
            
            if(player.IsInteracting && !player.IsHoldingItem)
            {
                transform.parent = player.HoldItemPosition;
                transform.localPosition = Vector3.zero;
                _inPlayerPossession = true;
                _collider.enabled = false;
                player.IsHoldingItem = true;
            }    
        }
    }

    IEnumerator DelayToGrabItem()
    {
        yield return new WaitForSeconds(0.5f);
        _isActive = true;
    }
}
