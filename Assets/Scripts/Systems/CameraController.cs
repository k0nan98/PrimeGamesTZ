using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
/// <summary>
/// Подключается к миру для контроля положения камеры за игроком.
/// </summary>
public class CameraController : IEcsRunSystem
{
    Transform player = null;
    Transform VMCam = null;
    private float offset = 9f;
    public void Run()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (VMCam == null) VMCam = GameObject.FindGameObjectWithTag("VMCam").transform;
        VMCam.transform.position = new Vector3(VMCam.transform.position.x, VMCam.transform.position.y, player.transform.position.z-offset);
    }

    
}
