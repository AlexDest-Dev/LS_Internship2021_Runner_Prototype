using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
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
            BGCurve curvePath = InitializePath();

            EcsEntity playerEntity = InitializePlayer();

            InitializeVirtualCamera(playerEntity);

            InitializeTouchHandler();
            
            CreateObstacles(curvePath);

            _playerConfiguration.WidthScalingFactor = LeanTouch.ScalingFactor * Screen.width;
        }

        private void CreateObstacles(BGCurve curve)
        {
            BGCcMath curveMath = curve.GetComponent<BGCcMath>();
            float curveDistance = curveMath.GetDistance();
            for (int i = 0; i < _worldConfiguration.ObstaclesAmount; i++)
            {
                GameObject obstacleInstance = GameObject.Instantiate(_worldConfiguration.ObstaclePrefab);
                EcsEntity obstacleEntity = _world.NewEntity();
                obstacleInstance.GetComponent<ObstacleEntityMonoBehaviour>().SetEntity(obstacleEntity);
                obstacleEntity.Get<Obstacle>();

                float randomDistanceOnCurve = Random.Range(0, curveDistance);
                float randomOffset = Random.Range(0, _playerConfiguration.MaxOffset);
                Vector3 randomPosition = curveMath.CalcPositionByDistance(randomDistanceOnCurve);
                randomPosition.x += randomOffset;
                randomPosition.y += randomOffset;
                obstacleInstance.transform.position = randomPosition;
            }
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
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
                _worldConfiguration.CameraFollowOffset;
            switch (_playerConfiguration.CameraFollowState)
            {
                case CameraFollowState.FollowObject:
                    Transform displacementTransform = playerMovable.Transform
                        .GetComponentInChildren<DisplacementEntityMonoBehaviour>().transform;
                    virtualCamera.Follow = displacementTransform;
                    virtualCamera.LookAt = displacementTransform;
                    break;
                case CameraFollowState.FollowWay:
                    virtualCamera.Follow = playerMovable.Transform;
                    virtualCamera.LookAt = playerMovable.Transform;
                    break;
            }
            
            virtualCameraEntity.Get<CameraComponent>().VirtualCamera = virtualCamera;
        }

        private BGCurve InitializePath()
        {
            BGCurve pathCurve = GameObject.Instantiate(_worldConfiguration.Path);
            EcsEntity pathEntity = _world.NewEntity();
            pathEntity.Get<Path>().PathCurve = pathCurve;
            Collider pathCollider = pathCurve.GetComponentInChildren<Collider>();
            pathCollider.transform.position = pathCurve.Points[pathCurve.Points.Length - 1].PositionWorld;
            pathCollider.GetComponent<PathVictoryColliderEntityMonoBehaviour>().SetEntity(pathEntity);

            return pathCurve;
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