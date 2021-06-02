using System;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.EntityMonoBehaviour
{
    public class PathVictoryColliderEntityMonoBehaviour : EntityMonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            _ecsEntity.Get<Collided>();
        }
    }
}