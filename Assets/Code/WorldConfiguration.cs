using BansheeGz.BGSpline.Curve;
using Cinemachine;
using Code.Components;
using Code.EntityMonoBehaviour;
using Lean.Touch;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "WorldConfiguration", menuName = "Configurations/WorldConfiguration")]
    public class WorldConfiguration : ScriptableObject
    {
        [SerializeField] private BGCurve _path;
        [SerializeField] private CinemachineVirtualCamera virtualMainCameraPrefab;
        [SerializeField] private Vector3 _cameraFollowOffset = new Vector3(0, 5, -10);
        [SerializeField] private LeanTouch _leanTouchPrefab;
        [SerializeField] private Canvas _canvasPrefab;
        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private int _obstaclesAmount;
        
        
        public BGCurve Path => _path;
        public CinemachineVirtualCamera VirtualCameraPrefab => virtualMainCameraPrefab;
        public Vector3 CameraFollowOffset => _cameraFollowOffset;
        public LeanTouch LeanTouchPrefab => _leanTouchPrefab;
        public Canvas CanvasPrefab => _canvasPrefab;
        public GameObject ObstaclePrefab => _obstaclePrefab;
        public int ObstaclesAmount => _obstaclesAmount;
    }
}