using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System;
//using UnityEngine.InputSystem.Users;

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
