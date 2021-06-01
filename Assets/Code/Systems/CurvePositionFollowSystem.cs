using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Systems
{
    public class CurvePositionFollowSystem : IEcsRunSystem
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
                
                movableComponent.CurrentCurveDistance += movableComponent.Speed * Time.deltaTime;
                
                Vector3 tangentPosition = 
                    pathCurveMath.CalcTangentByDistance(movableComponent.CurrentCurveDistance);

                Vector3 currentPosition = pathCurveMath.CalcPositionByDistance(movableComponent.CurrentCurveDistance);
                
                movableComponent.Transform.Translate(tangentPosition * Time.deltaTime * movableComponent.Speed);
                
                //TODO: Change checking of ending of path on more efficient
                if (Vector3.Distance(currentPosition, pathCurve.Points[pathCurve.Points.Length - 1].PositionWorld) < 1f)
                {
                    _movableFilter.GetEntity(movableIndex).Del<Movable>();
                }
            }
        }
    }
}