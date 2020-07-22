using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Используется для отображения информации о смерти ГГ
/// </summary>
public class DeathCanvasSystem : IEcsInitSystem
{
    public static DeathCanvas deathCanvas;

    public void Init()
    {
        Transform ui = GameObject.Find("UI").transform;
        foreach(Transform panel in ui)
        {
            
            if(panel.TryGetComponent<DeathCanvas>(out deathCanvas))
            {
                break;
            }
            
        }
        deathCanvas.gameObject.SetActive(false);
    }
    public static void Die()
    {
        RunController.isAlive = false;
        deathCanvas.gameObject.SetActive(true);
    }

}
