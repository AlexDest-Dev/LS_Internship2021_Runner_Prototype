using Leopotam.Ecs;
using Code.Components;

namespace Code.Systems
{
    public class VictoryCheckingSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Path, Collided> _collidedPathVictory;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        public void Run()
        {
            if (_gameStoppedFilter.IsEmpty() && _collidedPathVictory.IsEmpty() == false)
            {
                EcsEntity victoryEntity = _world.NewEntity();
                victoryEntity.Get<GameStopped>();
                victoryEntity.Get<Victory>();
            }
        }
    }
}