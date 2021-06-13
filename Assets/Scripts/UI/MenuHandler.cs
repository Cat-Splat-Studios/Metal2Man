using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.InputSystem.Users;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject lobbyPanel;

    [Header("Lobby Panel Stuff")]
    [SerializeField] PlayerLobbyInfo[] lobbyInfo;
    [SerializeField] TextMeshProUGUI playerCount;

    public string LevelName = "Level1";

    [SerializeField] PlayerController controller;
    [SerializeField] PlayerInputManager inputManager;

    [SerializeField] InputAction SecondPlayerButton;


    private void Start()
    {
        SecondPlayerButton.Enable();
        SecondPlayerButton.performed += ManualJoinPlayer;
    }

    private void OnDestroy()
    {
        SecondPlayerButton.Disable();
        SecondPlayerButton.performed -= ManualJoinPlayer;
    }

    private void ManualJoinPlayer(InputAction.CallbackContext obj)
    {        
        controller.AllowSplitKeyboard();

        //GameObject temp = Instantiate(inputManager.playerPrefab);
        //PlayerInput inputs = temp.GetComponent<PlayerInput>();
        //inputs.defaultActionMap = "SplitKeyboard";
        //inputs.SwitchCurrentControlScheme(Keyboard.current);
        //inputs.SwitchCurrentActionMap("SplitKeyboard");

        SecondPlayerButton.performed -= ManualJoinPlayer;

        //InputUser.PerformPairingWithDevice(Keyboard.current, inputs.user);
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Debug.Log("Player has joined");
        Instantiate(input.gameObject);
        playerCount.text = input.playerIndex + " /2 Players Joined";
    } 

    public void OnStartButton()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void OnQuitButton()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
