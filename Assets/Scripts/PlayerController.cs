using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInputs;

    public PlayerInput PlayerInputs => playerInputs;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerInputs = GetComponent<PlayerInput>();
        PlayerManager.AddPlayer(this);
    }

    private void OnDestroy()
    {
        PlayerManager.RemovePlayer(this);
    }
    public void Possess(Player player)
    {
        //playerInputs.actions["Move"].performed += player.mov
    }
}
