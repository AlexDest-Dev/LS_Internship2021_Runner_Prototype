using System;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Systems
{
    public class AccelerationDecelerationSystem : IEcsRunSystem
    {
        private EcsFilter<DoAccelerate, Movable> _doAccelerationFilter;
        private EcsFilter<Movable>.Exclude<DoAccelerate> _doDecelerationFilter;
        private EcsFilter<Movable, Destroy> _movableDestroyedFilter;
        private PlayerConfiguration playerConfiguration;
        public void Run()
        {
            SetZeroIfDestroyed();
            AccelerateMovable();
            DecelerationMovable();
        }

        private void SetZeroIfDestroyed()
        {
            foreach (var movableIndex in _movableDestroyedFilter)
            {
                _movableDestroyedFilter.Get1(movableIndex).Speed = 0;
            }
        }

        private void DecelerationMovable()
        {
            foreach (var movableIndex in _doDecelerationFilter)
            {
                ref Movable movableComponent = ref _doDecelerationFilter.Get1(movableIndex);
                float newSpeed = CalculateAcceleratedSpeed(ref movableComponent,true);
                movableComponent.Speed = Math.Max(0f, newSpeed);
            }
        }

        private void AccelerateMovable()
        {
            foreach (var movableIndex in _doAccelerationFilter)
            {
                ref Movable movableComponent = ref _doAccelerationFilter.Get2(movableIndex);
                float newSpeed = CalculateAcceleratedSpeed(ref movableComponent, false);
                movableComponent.Speed = Math.Min(newSpeed, movableComponent.MaxSpeed);
            }
        }

        private static float CalculateAcceleratedSpeed(ref Movable movableComponent, bool isDecelerate)
        {
            float acceleration = movableComponent.Acceleration;
            if (isDecelerate)
                acceleration = -acceleration;
            return movableComponent.Speed + acceleration * Time.deltaTime;
        }
    }
}