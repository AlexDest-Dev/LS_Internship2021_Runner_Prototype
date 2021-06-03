using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Systems
{
    public class EntityDestroyingSystem : IEcsRunSystem
    {
        private EcsFilter<Displacement, Destroy>.Exclude<FxPlaying> _displacementDestroy;
        public void Run()
        {
            foreach (var displacementIndex in _displacementDestroy)
            {
                GameObject.Destroy(_displacementDestroy.Get1(displacementIndex).DisplacementTransform.gameObject);
                _displacementDestroy.GetEntity(displacementIndex).Destroy();
            }
        }
    }
}