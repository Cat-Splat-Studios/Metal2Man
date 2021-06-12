using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected bool _inPlayerPossession;
    protected Collider _collider;
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

    protected virtual void Awake()
    {
        _collider = GetComponent<Collider>();
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
