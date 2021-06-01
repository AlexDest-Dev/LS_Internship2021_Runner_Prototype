using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Systems
{
    public class PositionChangingSystem : IEcsRunSystem
    {
        private EcsFilter<Movable> _movableFilter;
        private EcsFilter<Path> _pathFilter;
        
        public void Run()
        {
            BGCurve pathCurve = _pathFilter.Get1(0).PathCurve;
            BGCcMath pathCurveMath = pathCurve.GetComponent<BGCcMath>();
            foreach (var movableIndex in _movableFilter)
            {
                ref Movable movableComponent = ref _movableFilter.Get1(movableIndex);
                
                movableComponent.Position += movableComponent.Speed * Time.deltaTime;
                
                Vector3 tangentPosition = 
                    pathCurveMath.CalcTangentByDistance(movableComponent.Position);
                
                movableComponent.Transform.Translate(tangentPosition * Time.deltaTime * movableComponent.Speed);
            }
        }
    }
}