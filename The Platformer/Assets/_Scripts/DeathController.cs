using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
[System.Serializable]
public class DeathController : MonoBehaviour
{
    public Transform activeSpawnpoint;
    public GameObject player;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = activeSpawnpoint.position;
        }
    }
}
