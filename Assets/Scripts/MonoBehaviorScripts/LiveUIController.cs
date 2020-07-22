using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Управляет текстом на основном канвасе
/// </summary>
public class LiveUIController : MonoBehaviour
{
    public Text CoinsCount;
    public Text Distance;
    public Text SpaceToRunText;
    
    void Update()
    {
        Distance.text = string.Empty + GameStats.GetDistance();
        CoinsCount.text = string.Empty + GameStats.GetCoinsCount();
        if (RunController.isAlive) SpaceToRunText.gameObject.SetActive(false);
    }
}
