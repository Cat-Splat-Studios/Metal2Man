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

        //lobbyInfo[0].UpdateInfo(controller.PlayerInputs);
    }

    private void OnDestroy()
    {
        SecondPlayerButton.Disable();
        SecondPlayerButton.performed -= ManualJoinPlayer;
    }

    private void ManualJoinPlayer(InputAction.CallbackContext obj)
    {        
        controller.AllowSplitKeyboard();

        for(int i = 0; i < lobbyInfo.Length; i++)
        {
            if (i == 1 && PlayerManager.GetIsSplitKeyboard) 
            {
                lobbyInfo[i].UpdateInfo(controller, true);
            }

            lobbyInfo[i].UpdateInfo(PlayerManager.GetController(i));
        }

        SecondPlayerButton.performed -= ManualJoinPlayer;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Debug.Log("Menu Handler: Player Index on join is: " + input.playerIndex);
        lobbyInfo[input.playerIndex].UpdateInfo(input);
        playerCount.text = inputManager.playerCount.ToString() + "/" +
            inputManager.maxPlayerCount.ToString() + " Joined";
    } 
   

    public void SetSelected(GameObject gameobject)
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(gameobject);
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
