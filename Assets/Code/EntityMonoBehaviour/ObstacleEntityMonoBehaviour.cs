using System;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.EntityMonoBehaviour
{
    public class ObstacleEntityMonoBehaviour : EntityMonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            _ecsEntity.Get<Collided>();
        }
    }
}