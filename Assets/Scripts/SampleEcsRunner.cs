using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
namespace ClientECS
{
    public class SampleEcsRunner : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;
        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems
                .Add(new BasicPoolManager()) //Контролит порядок элементов в пуле
                .Add(new RunController()) //Бег ГГ
                .Add(new LevelBuilder()) //Штука для выстраивания уровня в зоне обзора, использует BasicPoolManager
                .Add(new DeathCanvasSystem()) //Контроллирует интерфейс после смерти
                .Add(new CameraController()) //Толкает камеру вперёд
                .Init();

        }

        void Update()
        {
            _systems?.Run();
        }
        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}