using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Source File: PlayerState.cs
/// Author: Geerthan Kanthasamy
/// This program creates an enumeration that for the player character's animations
/// </summary>
namespace Util
{
    [System.Serializable]
    public enum PlayerState
    {
        IDLE,
        WALK,
        JUMP
    }
}
