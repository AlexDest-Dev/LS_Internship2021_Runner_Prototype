using System.Numerics;
using System.Threading;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using Cinemachine.Utility;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Code.Systems
{
    public class CurvePositionFollowSystem : IEcsRunSystem
    {
        private EcsFilter<Movable> _movableFilter;
        private EcsFilter<CameraComponent> _cameraFilter;
        private EcsFilter<Path> _pathFilter;
        
        public void Run()
        {
            BGCurve pathCurve = _pathFilter.Get1(0).PathCurve;
            BGCcMath pathCurveMath = pathCurve.GetComponent<BGCcMath>();
            foreach (var movableIndex in _movableFilter)
            {
                ref Movable movableComponent = ref _movableFilter.Get1(movableIndex);
                
                movableComponent.CurrentCurveDistance += movableComponent.Speed * Time.deltaTime;
                
                Vector3 curvePosition = 
                    pathCurveMath.CalcPositionAndTangentByDistance(movableComponent.CurrentCurveDistance, out Vector3 tangentPosition);
                
                movableComponent.Transform.position = curvePosition;
                movableComponent.Transform.rotation = Quaternion.LookRotation(tangentPosition * Time.deltaTime);

            }
        }
    }
}