using BansheeGz.BGSpline.Curve;
using Cinemachine;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "WorldConfiguration", menuName = "Configurations/WorldConfiguration")]
    public class WorldConfiguration : ScriptableObject
    {
        [SerializeField] private BGCurve _path;
        [SerializeField] private CinemachineVirtualCamera _virtualMainCamera;
        [SerializeField] private Vector3 _cameraFollowOffset = new Vector3(0, 5, -10);
        
        public BGCurve Path => _path;
        public CinemachineVirtualCamera VirtualCamera => _virtualMainCamera;

        public Vector3 CameraFollowOffset => _cameraFollowOffset;
    }
}