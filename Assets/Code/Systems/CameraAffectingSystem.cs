using System;
using Cinemachine;
using Code.Components;
using Code.EntityMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Systems
{
    public class CameraAffectingSystem : IEcsRunSystem
    {
        private EcsFilter<CameraComponent> _cameraFilter;
        private EcsFilter<Movable> _movableFilter;
        private EcsFilter<Obstacle, Collided> _collidedObstacleFilter;
        private WorldConfiguration _worldConfiguration;
        public void Run()
        {
            float currentSpeed = _movableFilter.Get1(0).Speed;
            
            CinemachineVirtualCamera camera = _cameraFilter.Get1(0).VirtualCamera;
            camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain =
                currentSpeed / _worldConfiguration.CameraAmplitudeModifier;
            
            float fieldOfViewBySpeed = 
                _worldConfiguration.BaseFieldOfView - currentSpeed * _worldConfiguration.FieldOfViewSpeedModifier;
            camera.m_Lens.FieldOfView = Math.Max(fieldOfViewBySpeed, _worldConfiguration.MinimalFieldOfView);
        }
    }
}