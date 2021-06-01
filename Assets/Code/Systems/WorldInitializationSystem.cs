using BansheeGz.BGSpline.Curve;
using Cinemachine;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Systems
{
    public class WorldInitializationSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private WorldConfiguration _worldConfiguration;
        private PlayerConfiguration _playerConfiguration;
        
        public void Init()
        {
            InitializePath();

            EcsEntity playerEntity = InitializePlayer();

            InitializeVirtualCamera(playerEntity);
        }

        private void InitializeVirtualCamera(EcsEntity playerEntity)
        {
            CinemachineVirtualCamera virtualCamera = GameObject.Instantiate(_worldConfiguration.VirtualCamera);
            EcsEntity virtualCameraEntity = _world.NewEntity();
            ref Movable playerMovable = ref playerEntity.Get<Movable>();
            virtualCamera.Follow = playerMovable.Transform;
            virtualCamera.LookAt = playerMovable.Transform;
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
                _worldConfiguration.CameraFollowOffset;
            virtualCameraEntity.Get<CameraComponent>().VirtualCamera = virtualCamera;
        }

        private void InitializePath()
        {
            GameObject path = GameObject.Instantiate(_worldConfiguration.Path.gameObject);
            EcsEntity pathEntity = _world.NewEntity();
            pathEntity.Get<Path>().PathCurve = path.GetComponent<BGCurve>();
        }

        private EcsEntity InitializePlayer()
        {
            GameObject player = GameObject.Instantiate(_playerConfiguration.PlayerPrefab);
            EcsEntity playerEntity = _world.NewEntity();
            ref Movable playerMovable = ref playerEntity.Get<Movable>();
            playerMovable.Transform = player.transform;
            playerMovable.Acceleration = _playerConfiguration.Acceleration;
            playerMovable.Speed = 1f;
            playerMovable.MaxSpeed = _playerConfiguration.MaxSpeed;
            playerMovable.Position = 0f;
            return playerEntity;
        }
    }

    public struct CameraComponent
    {
        public CinemachineVirtualCamera VirtualCamera;
    }
}