using System;
using System.Collections;
using System.Collections.Generic;
using BrainCloud;
using BrainCloud.JsonFx.Json;
using BrainCloud.UnityWebSocketsForWebGL.WebSocketSharp;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public RelayConnectionType ConnectionType = RelayConnectionType.WEBSOCKET;
    
    public bool LeavingGame { get => _leavingGame; set => _leavingGame = value; }
    public BrainCloudWrapper WrapperBC => _bcWrapper;
    public UserInfo CurrentUser => _currentUser;
    
    public static NetworkManager Instance;

    private BrainCloudWrapper _bcWrapper;
    private UserInfo _currentUser;
    private bool _deadConnection;
    private bool _leavingGame;
    private bool _forceCreate = true;

    private NetworkPanel _networkPanel;
    
    void Awake()
    {
        _networkPanel = FindObjectOfType<NetworkPanel>();
        _networkPanel.CloseAllScreens();
        _networkPanel.gameObject.SetActive(false);
        _bcWrapper = GetComponent<BrainCloudWrapper>();
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_deadConnection)
        {
            UninitializeBC();
            _deadConnection = false;
        }
    }

    public void Login()
    {
        if (!_networkPanel.AreInputFieldsValid()) return;
        _currentUser = new UserInfo();
        _currentUser.Username = _networkPanel.UsernameInputField.text;
        InitializeBC();
        _bcWrapper.AuthenticateUniversal
            (
                _networkPanel.UsernameInputField.text,
                _networkPanel.PasswordInputField.text,
                _forceCreate,
                HandlePlayerState,
                LoggingInError,
                "Login Failed"
            );
    }

    private void InitializeBC()
    {
        _bcWrapper.Init();

        _bcWrapper.Client.EnableLogging(true);
    }
    
    // Uninitialize brainCloud
    void UninitializeBC()
    {
        if (_bcWrapper != null)
        {
            _bcWrapper.Client.ShutDown();
        }
    }
#region BC Callbacks

    void OnLoggedIn(string jsonResponse, object cbObject)
    {
        _networkPanel.SetUpMenu();
    }

    void HandlePlayerState(string jsonResponse, object cbObject)
    {
        var response = JsonReader.Deserialize<Dictionary<string, object>>(jsonResponse);
        var data = response["data"] as Dictionary<string, object>;
        var tempUsername = _currentUser.Username;
        var userInfo = new UserInfo();
        userInfo.ID = data["profileId"] as string;

        if (!data.ContainsKey("playerName"))
        {
            _bcWrapper.PlayerStateService.UpdateName
                (
                    tempUsername,
                    OnLoggedIn,
                    LoggingInError,
                    "Failed to update username to braincloud"
                );
        }
        else
        {
            userInfo.Username = data["playerName"] as string;
            if (userInfo.Username.IsNullOrEmpty())
            {
                userInfo.Username = tempUsername;
            }
            _bcWrapper.PlayerStateService.UpdateName
                (
                    userInfo.Username,
                    OnLoggedIn,
                    LoggingInError,
                    "Failed to update username to braincloud"
                );
        }

        _currentUser = userInfo;
    }

    void LoggingInError(int status, int reasonCode, string jsonError, object cbObject)
    {
        if (_deadConnection) return;

        _deadConnection = true;
        
        _bcWrapper.RelayService.DeregisterRelayCallback();
        _bcWrapper.RelayService.DeregisterSystemCallback();
        _bcWrapper.RelayService.Disconnect();
        _bcWrapper.RTTService.DeregisterAllRTTCallbacks();
        _bcWrapper.RTTService.DisableRTT();

        string message = cbObject as string;
        
        //Need an error pop up thingy
        _networkPanel.ErrorPopUp(message);
    }

#endregion
}
