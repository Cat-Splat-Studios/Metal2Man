using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject lobbyPanel;

    [Header("Lobby Panel Stuff")]
    [SerializeField] PlayerLobbyInfo[] lobbyInfo;
    [SerializeField] TextMeshProUGUI playerCount;
    public string LevelName = "Level1";
    private void Start()
    {

    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Debug.Log("Hello is this being called");
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
