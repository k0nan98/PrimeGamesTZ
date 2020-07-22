using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
/// <summary>
/// Статистика игры
/// </summary>
public struct GameStats
{
    public static Vector3 currentRoad;  //Vector3 просто заложен на будущее как-бы, может мы прыгать его научим или подкаты делать...
    private static int coins;
    private static int distance;
    /// <summary>
    /// Устанавливает расстояние для вывода
    /// </summary>
    public static void SetDistance(int Distance)
    {
        distance = Distance;
    }
    /// <summary>
    /// Возвращает расстояние от точки старта
    /// </summary>
    public static int GetDistance()
    {
        return distance;
    }
    /// <summary>
    /// Добавить монетку к статистике
    /// </summary>
    public static void addCoin()
    {
        coins++;
    }
    /// <summary>
    /// Узнать сколько монеток собрал игрок
    /// </summary>
    /// <returns>Кол-во монеток</returns>
    public static int GetCoinsCount() {
        return coins;
    }
}
