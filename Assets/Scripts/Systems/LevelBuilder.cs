using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
/// <summary>
/// Генерирует уровень в реальном времени. При перезагрузке ломается <трабл>
/// </summary>
public class LevelBuilder : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
{
    private float lastBlockPos_z;
    private float Player_pos_z;
    private Transform Player;
    private static int onePlaneSize = 10;
    /// <summary>
    /// Создаёт дорогу вперёд по оси Z
    /// </summary>
    private void CreateRoadFromPlane()
    {
        Transform plane = BasicPoolManager.PullPlane();
        for(int i = 1; i < 21; i++) // 1 to 20 + offset 1
        {
            GameObject newPlane = GameObject.Instantiate(plane.gameObject, plane.position + Vector3.forward * onePlaneSize*i, plane.rotation, plane.parent); //Генерация центральной линии
            BasicPoolManager.PushPlane(newPlane.transform);
        }
    }
    /// <summary>
    /// Создаёт монетки для пула
    /// </summary>
    private void CreateCoinsStack()
    {
        Transform coin = BasicPoolManager.PullCoin();
        for (int i = 1; i < 21; i++) // 1 to 20 + offset 1
        {
            GameObject newCoin = GameObject.Instantiate(coin.gameObject, coin.position, coin.rotation, coin.parent); //Генерация нужного кол-ва монет
            BasicPoolManager.PushCoin(newCoin.transform);
        }
    }
    public void Init()
    {
        CreateRoadFromPlane();
        CreateCoinsStack();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Player_pos_z = Player.position.z;
        for (int i = 0; i < BasicPoolManager.getCountPlanes(); i++)
        {
            Transform roadFragment = BasicPoolManager.PullPlane();
            GameObject left = GameObject.Instantiate(roadFragment.gameObject);
            GameObject right = GameObject.Instantiate(roadFragment.gameObject);
            left.name = "LeftCell";
            right.name = "RightCell"; //Просто для красоты
            left.transform.position = roadFragment.transform.position + Vector3.left * onePlaneSize;
            right.transform.position = roadFragment.transform.position + Vector3.right * onePlaneSize; //Создаём вокруг центральной линии ещё две
            left.transform.parent = roadFragment;
            right.transform.parent = roadFragment;
        }

    }
    /// <summary>
    /// Берёт монетки из пула и выстраивает в нужном месте
    /// </summary>
    /// <param name="startPos"></param>
    private void CreateCoins(Vector3 startPos)
    {
        int rand_cs = Random.Range(1, 10);
        for(int i = 0; i < rand_cs; i++)
        {
            Transform coin = BasicPoolManager.PullCoin();
            coin.position = new Vector3(startPos.x, 0, startPos.z + i * (onePlaneSize / 2)); // Можно поставить две сферы на один плейн
            coin.gameObject.SetActive(true);
        }

    }
    public void Run()
    {
        
        if (Player.position.z >= Player_pos_z + onePlaneSize)
        {
            Player_pos_z = Player.position.z;
            Transform cur_plane = BasicPoolManager.PullPlane(); //Взять крайний
            cur_plane.position = cur_plane.position + cur_plane.forward * BasicPoolManager.getCountPlanes() * onePlaneSize; //Движение на расстояние равное кол-ву Planе-ов умноженое на длинну одного
            if (Player.position.z > lastBlockPos_z + Random.Range(1, 3) * onePlaneSize) //Interactable block generation
            {
                //Blocks
                Transform cur_block = BasicPoolManager.PullBlock(); //Взять крайний
                float rand = Random.value;
                if (rand < 0.33f)
                {
                    cur_block.position = new Vector3(cur_block.position.x + Vector3.left.x * onePlaneSize, cur_block.position.y, cur_plane.position.z);
                }
                else if (rand > 0.66f)
                {
                    cur_block.position = new Vector3(cur_block.position.x + Vector3.right.x * onePlaneSize, cur_block.position.y, cur_plane.position.z);

                } else cur_block.position = new Vector3(cur_block.position.x, cur_block.position.y, cur_plane.position.z);
                //Coins
                if (rand < 0.4f)
                {
                    CreateCoins(new Vector3(Vector3.left.x * onePlaneSize, cur_block.position.y, cur_plane.position.z+10));
                }
                else if (rand > 0.7f)
                {
                    CreateCoins(new Vector3(Vector3.right.x * onePlaneSize, cur_block.position.y, cur_plane.position.z+10));

                }
                else CreateCoins(new Vector3(Player.position.x, cur_block.position.y, cur_plane.position.z+10));
                cur_block.gameObject.SetActive(true);
                lastBlockPos_z = cur_block.position.z;
            }
        }

    }
    public static int GetOnePlaneSize()
    {
        return onePlaneSize;
    }

    public void Destroy()
    {
        onePlaneSize = 0;
    }
}
