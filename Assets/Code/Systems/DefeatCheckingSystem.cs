using Code.Components;
using Leopotam.Ecs;

namespace Code.Systems
{
    public class DefeatCheckingSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Obstacle, Collided> _collidedObstacle;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        public void Run()
        {
            if (_gameStoppedFilter.IsEmpty() && _collidedObstacle.IsEmpty() == false)
            {
                EcsEntity defeatEntity = _world.NewEntity();
                defeatEntity.Get<GameStopped>();
                defeatEntity.Get<Defeat>();
            }
        }
    }
}