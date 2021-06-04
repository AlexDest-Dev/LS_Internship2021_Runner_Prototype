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
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private WorldConfiguration _worldConfiguration;
        public void Run()
        {
            float currentSpeed = _movableFilter.Get1(0).Speed;
            
            CinemachineVirtualCamera camera = _cameraFilter.Get1(0).VirtualCamera;

            float amplitudeBySpeed = currentSpeed * _worldConfiguration.CameraAmplitudeModifier;

            float fieldOfViewBySpeed = 
                _worldConfiguration.BaseFieldOfView - currentSpeed * _worldConfiguration.FieldOfViewSpeedModifier;
            
            SetFovAndAmplitudeToCamera(camera, amplitudeBySpeed, fieldOfViewBySpeed, _gameStoppedFilter.IsEmpty() == false);
        }

        private void SetFovAndAmplitudeToCamera(CinemachineVirtualCamera camera, float amplitudeBySpeed, float fieldOfViewBySpeed, bool setNoiseZero)
        {
            camera.m_Lens.FieldOfView = Math.Max(fieldOfViewBySpeed, _worldConfiguration.MinimalFieldOfView);
            
            if (setNoiseZero)
            {
                camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
                return;
            }

            camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeBySpeed;
        }
    }
}