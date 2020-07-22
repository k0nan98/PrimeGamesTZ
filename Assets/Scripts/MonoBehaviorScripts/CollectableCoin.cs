using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Монеточки
/// </summary>
public class CollectableCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameStats.addCoin();
            this.gameObject.SetActive(false);
        }
    }
}
