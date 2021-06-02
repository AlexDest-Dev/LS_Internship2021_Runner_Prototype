using Code.Components;
using Leopotam.Ecs;
using UnityEngine;
using Touch = Code.Components.Touch;

namespace Code.Systems
{
    public class DoMovableSystem : IEcsRunSystem
    {
        private EcsFilter<Touch> _touchFilter;
        private EcsFilter<Movable> _movableFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        public void Run()
        {
            if (_gameStoppedFilter.IsEmpty() && _touchFilter.IsEmpty() == false)
            {
                foreach (var movableIndex in _movableFilter)
                {
                    EcsEntity movableEntity = _movableFilter.GetEntity(movableIndex);
                    movableEntity.Get<DoAccelerate>();
                }
            }
        }
    }
}