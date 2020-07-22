using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Затычка, чтоб повесить на объект и проще дёргать с кнопки
/// </summary>
public class DeathCanvas : MonoBehaviour
{
    public void LoadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
