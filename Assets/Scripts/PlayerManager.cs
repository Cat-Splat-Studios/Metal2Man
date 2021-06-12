using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class PlayerManager
{
    private static List<PlayerController> ActiveControllers = new List<PlayerController>();

    public static void AddPlayer(PlayerController newController)
    {
        ActiveControllers.Add(newController);
    }

    public static void RemovePlayer(PlayerController controllerToRemove)
    {
        ActiveControllers.Remove(controllerToRemove);
    }

    public static PlayerController GetController(int index)
    {
        return ActiveControllers[index];
    }

    public static bool IsPlayerInRange(int index)
    {
        return index < ActiveControllers.Count;
    }
}