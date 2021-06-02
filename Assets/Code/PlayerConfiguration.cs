using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Configurations/PlayerConfiguration")]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _maxOffset = 2f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _acceleration = 1f;
        [SerializeField] private float _lerpDisplacementCoefficient = 0.5f;

        public GameObject PlayerPrefab => _playerPrefab;
        public float MaxOffset => _maxOffset;
        public float MaxSpeed => _maxSpeed;
        public float Acceleration => _acceleration;
        public float LerpDisplacementCoefficient => _lerpDisplacementCoefficient;
    }
}