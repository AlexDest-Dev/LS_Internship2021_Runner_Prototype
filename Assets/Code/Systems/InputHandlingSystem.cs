using Code.Components;
using Lean.Touch;
using Leopotam.Ecs;

namespace Code.Systems
{
    public class InputHandlingSystem : IEcsRunSystem
    {
        private EcsFilter<Movable> _movableFilter;
        public void Run()
        {
            foreach (var movableIndex in _movableFilter)
            { 
                EcsEntity entity = _movableFilter.GetEntity(movableIndex);
                if (LeanTouch.Fingers.Count > 1 && LeanTouch.Fingers[1].Set)
                {
                    entity.Get<DoMovable>();
                }
            }
        }
    }
}