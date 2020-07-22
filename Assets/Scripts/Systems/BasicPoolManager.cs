using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using ClientECS;
/// <summary>
/// Менеджер всех штук в пуле, которые игра может потребовать
/// </summary>
public class BasicPoolManager : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
{
    private static List<Transform> planes = new List<Transform>(); //Генерируются дополнительно в LevelBuilder
    private static List<Transform> blocks = new List<Transform>();
    private static List<Transform> coins = new List<Transform>(); //Генерируются дополнительно в LevelBuilder
    /// <summary>
    /// Кол-во платформ
    /// </summary>
    /// <returns></returns>
    public static int getCountPlanes()
    {
        return planes.Count;
    }
    /// <summary>
    /// Добавить платформу в список
    /// </summary>
    /// <param name="obj"></param>
    public static void PushPlane(Transform obj)
    {
        planes.Add(obj);
    }
    /// <summary>
    /// Добавить блок в список
    /// </summary>
    /// <param name="obj"></param>
    public static void PushBlock(Transform obj)
    {
        blocks.Add(obj);
    }
    /// <summary>
    /// Добавить монетку в список
    /// </summary>
    /// <param name="obj"></param>
    public static void PushCoin(Transform obj)
    {
        coins.Add(obj);
    }
    /// <summary>
    /// Выбрать платформу
    /// </summary>
    /// <returns></returns>
    public static Transform PullPlane()
    {
        Transform cur_plane = planes[0];
        planes.RemoveAt(0);
        planes.Add(cur_plane); //Перенос элемента в конец массива
        return cur_plane;
    }
    /// <summary>
    /// Выбрать блок
    /// </summary>
    /// <returns></returns>
    public static Transform PullBlock()
    {
        Transform cur_block = blocks[0];
        blocks.RemoveAt(0);
        blocks.Add(cur_block); //Перенос элемента в конец массива
        return cur_block;
    }
    /// <summary>
    /// Выбрать монетку
    /// </summary>
    /// <returns></returns>
    public static Transform PullCoin()
    {
        Transform cur_coin = coins[0];
        coins.RemoveAt(0);
        coins.Add(cur_coin); //Перенос элемента в конец массива
        return cur_coin;
    }

    public void Init()
    {
        Transform pool = GameObject.FindGameObjectWithTag("Pool").transform;
        foreach(Transform poolObject in pool) //Выбираем все объекты из пула и пихаем по спискам
        {
            if (poolObject.name.Contains("Plane"))
            {
                PushPlane(poolObject);
            } else if (poolObject.name.Contains("Cube"))
            {
                PushBlock(poolObject);
            }
            else if (poolObject.name.Contains("Sphere"))
            {
                PushCoin(poolObject);
            }
        }

    }

    public void Run()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            PullPlane();
        }

    }

    public void Destroy()
    {
        planes.Clear();
        blocks.Clear();
        coins.Clear();

    }
}
