using BansheeGz.BGSpline.Curve;
using Code;
using Code.Components;
using Code.EntityMonoBehaviour;
using Code.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Touch = Code.Components.Touch;

namespace Client {
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;
        [SerializeField] private WorldConfiguration _worldConfiguration;
        [SerializeField] private PlayerConfiguration _playerConfiguration;

        void Start () {
            // void can be switched to IEnumerator for support coroutines.
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your systems here
                .Add(new WorldInitializationSystem())
                .Add(new InputHandlingSystem())
                .Add(new DoMovableSystem())
                .Add(new AccelerationDecelerationSystem())
                .Add(new CurvePositionFollowSystem())
                .Add(new DisplacementSystem())
                .Add(new VictoryCheckingSystem())
                .Add(new DefeatCheckingSystem())
                // register one-frame components (order is important)
                .OneFrame<DoAccelerate>()
                .OneFrame<Collided>()
                // inject service instances here (order doesn't important)
                .Inject(_worldConfiguration)
                .Inject(_playerConfiguration)
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}