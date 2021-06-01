using UnityEngine;

namespace Code.Components
{
    public struct Movable
    {
        public Transform Transform;
        public float Acceleration;
        public float Speed;
        public float MaxSpeed;
        public float CurrentCurveDistance;
    }
}