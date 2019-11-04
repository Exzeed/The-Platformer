using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Source File: DeathController.cs
/// Author: Geerthan Kanthasamy
/// This program resets player location when in contact with DeathPlane
/// </summary>
[System.Serializable]
public class DeathController : MonoBehaviour
{
    public Transform activeSpawnpoint;
    public GameObject player;
    public GameController gameController;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameController.Lives -= 1;
            other.gameObject.transform.position = activeSpawnpoint.position;
        }
    }
}
