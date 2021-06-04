using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Systems
{
    public class FxControllingSystem : IEcsRunSystem
    {
        private EcsFilter<Displacement, Destroy>.Exclude<FxPlaying> _displacementFilter;
        private EcsFilter<FxPlaying> _fxPlayingFilter;
        public void Run()
        {
            PlayDisplacementDestroy();

            RemoveFxPlayingIfFinished();
        }

        private void RemoveFxPlayingIfFinished()
        {
            foreach (var fxIndex in _fxPlayingFilter)
            {
                if (_fxPlayingFilter.Get1(fxIndex).ParticleSystem.IsAlive() == false)
                {
                    _fxPlayingFilter.GetEntity(fxIndex).Del<FxPlaying>();
                }
            }
        }

        private void PlayDisplacementDestroy()
        {
            foreach (var displacementIndex in _displacementFilter)
            {
                ref FxPlaying fxComponent = ref _displacementFilter.GetEntity(displacementIndex).Get<FxPlaying>();
                Transform displacementTransform = _displacementFilter.Get1(displacementIndex).DisplacementTransform;
                displacementTransform.GetComponentInChildren<MeshRenderer>()?.gameObject.SetActive(false);
                fxComponent.ParticleSystem = displacementTransform.GetComponentInChildren<ParticleSystem>();
                fxComponent.ParticleSystem.Play();
            }
        }
    }
}