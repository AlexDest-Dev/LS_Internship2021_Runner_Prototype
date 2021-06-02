using Leopotam.Ecs;
using UnityEngine;

namespace Code.EntityMonoBehaviour
{
    public class EntityMonoBehaviour : MonoBehaviour
    {
        protected EcsEntity _ecsEntity;

        public void SetEntity(EcsEntity entity)
        {
            _ecsEntity = entity;
        }
    }
}