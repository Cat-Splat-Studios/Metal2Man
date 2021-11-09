using System.Collections;
using System.Collections.Generic;
using BrainCloud;
using TMPro;
using UnityEngine;

public class DropdownMenuCallback : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }
    //Called from dropdown menu's value change event
    public void OnValueChange()
    {
        NetworkManager.Instance.ConnectionType = (RelayConnectionType)_dropdown.value + 1;
    }
}
