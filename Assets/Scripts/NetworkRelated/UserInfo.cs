using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the information needed from a User
/// </summary>

public class UserInfo
{
    //Used to know if local user is hosting
    public string ID;
    //Used for displaying and identifying users
    public string Username;
    //if this user should show shockwaves locally
    public bool AllowSendTo = true;     
    //Is this user still connected
    public bool IsAlive;
    //Current Mouse Position to display
    public Vector2 MovePosition;
    
    //public UserCursor UserCursor;
    public UserInfo() { }
    public string cxId;
    public UserInfo(Dictionary<string, object> userJson)
    {
        cxId = userJson["cxId"] as string;
        ID = userJson["profileId"] as string;
        Username = userJson["name"] as string;
    }
}