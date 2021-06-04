using System;
using Lean.Touch;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Configurations/PlayerConfiguration")]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] private float _widthScalingFactor;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _maxOffset = 2f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _acceleration = 1f;
        [Range(0f,1f)][SerializeField] private float _lerpRotationCoefficient = 0.5f;
        [SerializeField] private CameraFollowState cameraFollowState;

        public float WidthScalingFactor
        {
            get => _widthScalingFactor;
            set => _widthScalingFactor = value;
        }
        public GameObject PlayerPrefab => _playerPrefab;
        public float MaxOffset => _maxOffset;
        public float MaxSpeed => _maxSpeed;
        public float Acceleration => _acceleration;
        public float LerpRotationCoefficient => _lerpRotationCoefficient;
        public CameraFollowState CameraFollowState => cameraFollowState;
    }

    public enum CameraFollowState
    {
        FollowWay,
        FollowObject
    }
}