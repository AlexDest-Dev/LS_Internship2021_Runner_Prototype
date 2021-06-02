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
            DisplacementEntityMonoBehaviour playerDisplacement =
                other.gameObject.GetComponentInChildren<DisplacementEntityMonoBehaviour>();
            if (playerDisplacement != null)
            {
                Debug.Log("Obstacle collided by player");
                _ecsEntity.Get<Collided>();
            }
        }
    }
}