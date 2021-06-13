using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInputs;
    [SerializeField] bool isSplitKeyboard = false;

    public PlayerInput PlayerInputs => playerInputs;

    private PlayerInput splitKeyboardInputs;
    public PlayerInput SplitKeyboard => splitKeyboardInputs;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerInputs = GetComponent<PlayerInput>();
        PlayerManager.AddPlayer(this);

        PlayerInputs.onDeviceLost += DebugDeviceLost;
    }

    private void DebugDeviceLost(PlayerInput obj)
    {
        Debug.Log("Device is lost on " + gameObject.name);
    }

    public void AllowSplitKeyboard()
    {
        PlayerManager.SetSplitKeyboard(true);
    }

    private void OnDestroy()
    {
        PlayerManager.RemovePlayer(this);
    }
}
