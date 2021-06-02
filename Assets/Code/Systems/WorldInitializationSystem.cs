﻿using BansheeGz.BGSpline.Curve;
using Cinemachine;
using Code.Components;
using Code.EntityMonoBehaviour;
using Lean.Touch;
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

            InitializeTouchHandler();
            
            
        }

        private void InitializeTouchHandler()
        {
            LeanTouch leanTouch = GameObject.Instantiate(_worldConfiguration.LeanTouchPrefab);
            Canvas canvas =
                GameObject.Instantiate(_worldConfiguration.CanvasPrefab);
            TouchHandlerEntityMonoBehaviour touchHandler =
                canvas.GetComponentInChildren<TouchHandlerEntityMonoBehaviour>();
            EcsEntity touchHandlerEntity = _world.NewEntity();
            touchHandler.SetEntity(touchHandlerEntity);
            touchHandlerEntity.Get<TouchHandler>().Handler = touchHandler;
        }

        private void InitializeVirtualCamera(EcsEntity playerEntity)
        {
            CinemachineVirtualCamera virtualCamera = GameObject.Instantiate(_worldConfiguration.VirtualCameraPrefab);
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
            BGCurve pathCurve = GameObject.Instantiate(_worldConfiguration.Path);
            EcsEntity pathEntity = _world.NewEntity();
            pathEntity.Get<Path>().PathCurve = pathCurve;
            Collider pathCollider = pathCurve.GetComponentInChildren<Collider>();
            pathCollider.transform.position = pathCurve.Points[pathCurve.Points.Length - 1].PositionWorld;
            pathCollider.GetComponent<PathVictoryColliderEntityMonoBehaviour>().SetEntity(pathEntity);
        }

        private EcsEntity InitializePlayer()
        {
            GameObject player = GameObject.Instantiate(_playerConfiguration.PlayerPrefab);
            EcsEntity playerEntity = _world.NewEntity();
            
            ref Movable playerMovable = ref playerEntity.Get<Movable>();
            playerMovable.Transform = player.transform;
            playerMovable.Acceleration = _playerConfiguration.Acceleration;
            playerMovable.Speed = 0f;
            playerMovable.MaxSpeed = _playerConfiguration.MaxSpeed;
            playerMovable.CurrentCurveDistance = 0f;

            DisplacementEntityMonoBehaviour displacement =
                player.GetComponentInChildren<DisplacementEntityMonoBehaviour>();
            displacement.SetEntity(playerEntity);
            
            ref Displacement playerDisplacement = ref playerEntity.Get<Displacement>();
            playerDisplacement.MaxOffset = _playerConfiguration.MaxOffset;
            playerDisplacement.DisplacementTransform = displacement.transform;
            playerDisplacement.StartPosition = displacement.transform.localPosition;
            
            return playerEntity;
        }
    }
}