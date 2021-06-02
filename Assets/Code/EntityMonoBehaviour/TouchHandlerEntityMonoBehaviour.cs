using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.EntityMonoBehaviour
{
    public class TouchHandlerEntityMonoBehaviour : EntityMonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            _ecsEntity.Get<Components.Touch>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _ecsEntity.Del<Components.Touch>();
        }
    }
}
