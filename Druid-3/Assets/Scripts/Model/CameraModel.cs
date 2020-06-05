using System;
using Helper;
using UnityEngine;


namespace Model
{
    public sealed class CameraModel : BaseObjectScene
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private LayerMask _layerCamera;
        [SerializeField] private float _sensitivity = 3;
        [SerializeField] private float _limitAxisY = 80;
        [SerializeField] private float _sensitivityZoom = 0.25f;
        [SerializeField] private float _zoomMax = 4;
        [SerializeField] private float _zoomMin = 2;

        private float _x;
        private float _y;

        // private float zoomFaer;
        // private bool leftEndRight;

        public float AxisX
        {
            get { return _x; }
            set { _x += value * _sensitivity; }
        }

        public float AxisY
        {
            get { return _y; }
            set { _y += value * _sensitivity; }
        }

        void Start()
        {
            _limitAxisY = Mathf.Abs(_limitAxisY);
            if (_limitAxisY > 90) _limitAxisY = 90;
            _offset = new Vector3(_offset.x, _offset.y, -Mathf.Abs(_zoomMax) / 2);
            transform.position = _target.position + _offset;
        }

        public void AddOffsetZ()
        {
            _offset.z += _sensitivityZoom;
        }

        public void RemoveOffsetZ()
        {
            _offset.z -= _sensitivityZoom;
        }

        public void ClampByZ()
        {
            _offset.z = Mathf.Clamp(_offset.z, -Mathf.Abs(_zoomMax), -Mathf.Abs(_zoomMin));
        }

        public void ClampByY()
        {
            _y = Mathf.Clamp(_y, -_limitAxisY, _limitAxisY);
        }

        public void Rotate()
        {
            Transform.localEulerAngles = new Vector3(-_y, _x, 0);
            Transform.position = transform.localRotation * _offset + _target.position;
        }

        public void CheckVisibilityTargetAndMoveCamera()
        {
            var direction = Transform.position - _target.transform.position;
            var distance = (direction).magnitude;
            if (Physics.Raycast(_target.transform.position, direction, out var hitInfo, distance))
            {
                // Dbg.Log($"hitInfo.point: {hitInfo.point}, _target.transform.position:{_target.transform.position}, hitInfo.distance:{hitInfo.distance}");
                Transform.position = transform.localRotation * new Vector3(0,0,-hitInfo.distance)  + _target.position;
            }
        }
    }
}