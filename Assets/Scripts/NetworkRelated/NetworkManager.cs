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
    void Awake()
    {
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
        NetworkPanel networkPanel = FindObjectOfType<NetworkPanel>();

        string username = networkPanel.UsernameField.text;
        string password = networkPanel.PasswordField.text;
        if (username.IsNullOrEmpty())
        {
            //Need an error pop up thingy
            return;
        }
        if (password.IsNullOrEmpty())
        {
            //Need an error pop up thingy
            return;
        }

        _currentUser.Username = networkPanel.UsernameField.text;
        InitializeBC();
        _bcWrapper.AuthenticateUniversal
            (
                username,
                password,
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
    }

#endregion
}
