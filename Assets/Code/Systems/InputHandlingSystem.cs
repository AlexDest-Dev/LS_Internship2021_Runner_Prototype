using Code.Components;
using Lean.Touch;
using Leopotam.Ecs;

namespace Code.Systems
{
    public class InputHandlingSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Touch> _touchFilter;
        public void Run()
        {
            foreach (var touchIndex in _touchFilter)
            {
                _touchFilter.Get1(touchIndex).Finger = LeanTouch.Fingers[0];
            }
        }
    }
}