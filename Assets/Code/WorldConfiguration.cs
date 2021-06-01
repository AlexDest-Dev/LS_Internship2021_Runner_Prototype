﻿using BansheeGz.BGSpline.Curve;
using Cinemachine;
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
        
        public BGCurve Path => _path;
        public CinemachineVirtualCamera VirtualCameraPrefab => virtualMainCameraPrefab;
        public Vector3 CameraFollowOffset => _cameraFollowOffset;
        public LeanTouch LeanTouchPrefab => _leanTouchPrefab;
    }
}