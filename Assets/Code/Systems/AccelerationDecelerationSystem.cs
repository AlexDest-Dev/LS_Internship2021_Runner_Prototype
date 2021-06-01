using System;
using Code.Components;
using Leopotam.Ecs;

namespace Code.Systems
{
    public class AccelerationDecelerationSystem : IEcsRunSystem
    {
        private EcsFilter<DoMovable, Movable> _doAccelerationFilter;
        private EcsFilter<Movable>.Exclude<DoMovable> _doDecelerationFilter;
        private PlayerConfiguration playerConfiguration;
        public void Run()
        {
            AccelerateMovable();
            DecelerationMovable();
        }

        private void DecelerationMovable()
        {
            foreach (var movableIndex in _doDecelerationFilter)
            {
                ref Movable movableComponent = ref _doDecelerationFilter.Get1(movableIndex);
                movableComponent.Speed = Math.Max(0f, movableComponent.Speed - movableComponent.Acceleration);
            }
        }

        private void AccelerateMovable()
        {
            foreach (var movableIndex in _doAccelerationFilter)
            {
                ref Movable movableComponent = ref _doAccelerationFilter.Get2(movableIndex);

                movableComponent.Speed = Math.Min(movableComponent.Speed + movableComponent.Acceleration,
                    movableComponent.MaxSpeed);
            }
        }
    }
}