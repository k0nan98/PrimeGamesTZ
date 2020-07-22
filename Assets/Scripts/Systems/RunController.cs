using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
/// <summary>
/// Контролит бег персонажа, да и в целом всего персонажа, это раннер же)
/// </summary>
public class RunController : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
{
    private Transform Player;
    private float Speed = 2f;
    private Vector3 fromPos;
    private Vector3 toPos;
    private float p_percentage;
    private Vector3 PlayerStartPos;
    public static bool isAlive = false;
    public void Init()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerStartPos = Player.position;
    }

    public void Run()
    {
        GameStats.SetDistance((int)Vector3.Distance(PlayerStartPos, Player.position));
        if(Player.transform.position.z <= 0 && !isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isAlive = true;
            }
        }
        if (isAlive)
        {
            Player.GetComponent<Animator>().enabled = true;
            float coinModificator = Mathf.Clamp(Map(GameStats.GetCoinsCount(), 0, 100, 1, 5), 1, 2.5f);
            Vector3 input = Vector3.zero;
            if (Player != null)
            {
                Player.position += Player.forward * Speed * coinModificator / 10;
            }
            float x = Input.GetAxis("Horizontal");
            if (x < 0)
            {
                input = Vector3.left;
            }
            else if (x > 0)
            {
                input = Vector3.right;
            }


            VisualizePlayerPos(CalculatePlayerPos(input));
        }
        else Player.GetComponent<Animator>().enabled = false;
    }
    /// <summary>
    /// Проверяет ввод и отправляет игрока на нужную полосу.
    /// </summary>
    /// <param name="input">Нормализованый вектор, сформированый по вводу с клавиатуры</param>
    /// <returns></returns>
    private Vector3 CalculatePlayerPos(Vector3 input)
    {
        switch (input.x)
        {
            case 0:
                if(p_percentage == 0)
                {
                    return Player.position;
                } else
                {

                    p_percentage += Time.deltaTime;
                    if (p_percentage >= 1)
                    {
                        GameStats.currentRoad = new Vector3((int)Map(toPos.x, LevelBuilder.GetOnePlaneSize() * -1, LevelBuilder.GetOnePlaneSize(), -1, 1), 0, 0);
                        p_percentage = 0;
                        return Vector3.Lerp(fromPos, toPos, 1);
                    }
                    else
                        return Vector3.Lerp(fromPos, toPos, p_percentage);
                }
                break;
            case -1:
                if (p_percentage == 0 && GameStats.currentRoad != Vector3.left)
                {
                    fromPos = Player.transform.position;
                    toPos = Player.transform.position + new Vector3(LevelBuilder.GetOnePlaneSize() * -1, 0, 0);
                    p_percentage = 0.001f;
                    return Vector3.Lerp(fromPos, toPos, p_percentage);
                }
                if(p_percentage != 0)
                {
                    p_percentage += Time.deltaTime;
                    return Vector3.Lerp(fromPos, toPos, p_percentage);
                }
                if (GameStats.currentRoad == Vector3.left)
                {
                    return Player.transform.position;
                }
                break;
            case 1:
                if (p_percentage == 0 && GameStats.currentRoad != Vector3.right)
                {
                    fromPos = Player.transform.position;
                    toPos = Player.transform.position - new Vector3(LevelBuilder.GetOnePlaneSize() * -1, 0, 0);
                    p_percentage = 0.001f;
                    return Vector3.Lerp(fromPos, toPos, p_percentage);
                }
                if (p_percentage != 0)
                {
                    p_percentage += Time.deltaTime;
                    return Vector3.Lerp(fromPos, toPos, p_percentage);
                }
                if(GameStats.currentRoad == Vector3.right)
                {
                    return Player.transform.position;
                }
                break;
                
        }
        return Vector3.zero;
    }
    /// <summary>
    /// Перемещает модельку игрока на нужную позицию.
    /// </summary>
    /// <param name="pos">Позиция для игрока</param>
    private void VisualizePlayerPos(Vector3 pos)
    {
        Player.transform.position = new Vector3(pos.x, Player.transform.position.y, Player.transform.position.z);
    }
    /// <summary>
    /// Маппинг одного диапазона данных к другому
    /// </summary>
    /// <param name="value">Значение</param>
    /// <param name="fromSource">Начало диапазона 1(минимум)</param>
    /// <param name="toSource">Конец диапазона 2(максимум)</param>
    /// <param name="fromTarget">Начало диапазона 2(минимум)</param>
    /// <param name="toTarget">Конец диапазона 2(максимум)</param>
    /// <returns></returns>
    private float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public void Destroy()
    {
        isAlive = true;

    }
}
