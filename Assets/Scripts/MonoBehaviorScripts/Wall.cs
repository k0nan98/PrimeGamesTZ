using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ну это стена, просто... стена...
/// </summary>
public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            DeathCanvasSystem.Die();
        }
    }
}
