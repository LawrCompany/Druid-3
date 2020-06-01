using Interface;
using UnityEngine;


namespace Controller
{
    internal sealed class UnitMotor : IMotor
    {
        #region Fields

        private Transform _instance;

        private float _speedMove = 10;
        private float _jumpPower = 10;
        private float _gravityForce;
        private Quaternion _characterTargetRotation;
        private Quaternion _cameraTargetRotation;
        private Vector3 _input;
        private Vector3 _moveVector = Vector3.zero;
        private CharacterController _characterController;
        private Transform _head;

        private float XSensivity = 2f;
        private float YSensivity = 2f;
        private bool ClampVerticalRotation = true;
        private float MinimumX = -70f;
        private float MaximumX = 70f;
        private bool Smooth;
        private float SmoothTime = 5f;

        #endregion


        #region UnityMethods

        public UnitMotor(CharacterController instance)
        {
            _instance = instance.transform;
            _characterController = instance;
            _head = Camera.main.transform;

            _characterTargetRotation = _instance.localRotation;
            _cameraTargetRotation = _head.localRotation;
        }

        #endregion


        #region Methods

        public void Move()
        {
            CharacterMove();
            GamingGravity();
         
            LokRotation(_instance, _head);
        }

        void CharacterMove()
        {
            if (_characterController.isGrounded)
            {
                _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                _moveVector = _instance.TransformDirection(_input);
                _moveVector *= _speedMove;
            }

            _moveVector.y = _gravityForce;
            _characterController.Move(_moveVector * Time.deltaTime);
        }

        private void GamingGravity()
        {
            if (!_characterController.isGrounded)
                _gravityForce -= 30 * Time.deltaTime;
            else
                _gravityForce = -1;

            if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
                _gravityForce = _jumpPower;
        }

        private void LokRotation(Transform character, Transform camera)
        {
            float yRotation = Input.GetAxis("Mouse X") * XSensivity;
            float xRotation = Input.GetAxis("Mouse Y") * YSensivity;

            _characterTargetRotation *= Quaternion.Euler(0f, yRotation, 0f);
            _cameraTargetRotation *= Quaternion.Euler(-xRotation, 0f, 0f);

            if (ClampVerticalRotation)
                _cameraTargetRotation = ClampRotationAroundXAxis(_cameraTargetRotation);

            if (Smooth)
            {
                character.localRotation = Quaternion.Slerp(
                                character.localRotation,
                                _characterTargetRotation,
                                SmoothTime * Time.deltaTime);
                character.localRotation = Quaternion.Slerp(
                                camera.localRotation,
                                _cameraTargetRotation,
                                SmoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = _characterTargetRotation;
                camera.localRotation = _cameraTargetRotation;
            }
        }

        private Quaternion ClampRotationAroundXAxis(Quaternion quaternion)
        {
            quaternion.x /= quaternion.w;
            quaternion.y /= quaternion.w;
            quaternion.y /= quaternion.w;
            quaternion.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quaternion.x);

            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

            quaternion.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return quaternion;
        }

        #endregion
    }
}