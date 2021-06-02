using System;
using Code.Components;
using Lean.Touch;
using Leopotam.Ecs;
using UnityEngine;
using Touch = Code.Components.Touch;

namespace Code.Systems
{
    public class DisplacementSystem : IEcsRunSystem
    {
        private EcsFilter<Touch> _touchFilter;
        private EcsFilter<Displacement> _displacementFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private PlayerConfiguration _playerConfiguration;
        
        public void Run()
        {
            if (_gameStoppedFilter.IsEmpty())
            {
                LeanFinger finger = _touchFilter.Get1(0).Finger;
                if (finger != null)
                {
                    MakeDisplacement(finger);
                }
            }
        }

        private void MakeDisplacement(LeanFinger finger)
        {
            foreach (var displacementIndex in _displacementFilter)
            {
                ref Displacement displacementComponent = ref _displacementFilter.Get1(displacementIndex);
                Transform displacementTransform = displacementComponent.DisplacementTransform;
                Vector3 localPosition = displacementTransform.localPosition;
                Vector2 newDisplacementPosition =
                    (Vector2) localPosition + finger.ScaledDelta * Time.deltaTime;

                CalculatePositionComponentWithRespectToMaxOffset(ref newDisplacementPosition.x, displacementComponent);
                CalculatePositionComponentWithRespectToMaxOffset(ref newDisplacementPosition.y, displacementComponent);

                displacementTransform.localPosition =
                    Vector3.Lerp(newDisplacementPosition, localPosition, _playerConfiguration.LerpDisplacementCoefficient);
            }
        }

        private void CalculatePositionComponentWithRespectToMaxOffset(ref float positionComponent, Displacement displacement)
        {
            positionComponent = Math.Max(-displacement.MaxOffset, positionComponent);
            positionComponent = Math.Min(displacement.MaxOffset, positionComponent);
        }
    }
}