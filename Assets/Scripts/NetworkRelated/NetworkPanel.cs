using System.Collections;
using System.Collections.Generic;
using BrainCloud.UnityWebSocketsForWebGL.WebSocketSharp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Main purpose for this class is to hold UI elements and functionality for NetworkManager to call into
public class NetworkPanel : MonoBehaviour
{
    
    [Header("Screens")]
    public GameObject Login;
    public GameObject Menu;
    public GameObject Lobby;
    public GameObject Error;
    public GameObject Loading;

    [Header("Login Elements")]    
    public TMP_InputField UsernameInputField;
    public TMP_InputField PasswordInputField;
    
    [Header("Lobby Elements")]
    public GameObject StartGameButton;
    public TMP_Text ReadyUpText;
    private const string _readyUpMessage = "Ready !";
    private const string _notReadyMessage = "Not Ready";

    [Header("Menu Elements")]
    public TMP_Text UsernameMenuText;
    private const string _menuStartingMessage = "Logged in as ";

    [Header("Error Elements")]
    public TMP_Text ErrorMessageText;

    public bool AreInputFieldsValid()
    {
        if (UsernameInputField.text.IsNullOrEmpty())
        {
            ErrorPopUp("Please provide a username");
            return false;
        }
        
        if (PasswordInputField.text.IsNullOrEmpty())
        {
            ErrorPopUp("Please provide a password");            
            return false;
        }

        return true;
    }

    public void SetUpMenu()
    {
        UsernameMenuText.text = _menuStartingMessage + UsernameInputField.text;
        Login.SetActive(false);
        Menu.SetActive(true);
    }

    public void CloseAllScreens()
    {
        Lobby.SetActive(false);
        Login.SetActive(false);
        Menu.SetActive(false);
        Loading.SetActive(false);
        Error.SetActive(false);
    }
    
    public void ErrorPopUp(string errorMessage)
    {
        CloseAllScreens();

        ErrorMessageText.text = errorMessage;
        Error.SetActive(true);
    }
}
