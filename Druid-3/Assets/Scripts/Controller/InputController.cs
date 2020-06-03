using Enums;
using Interface;
using UnityEngine;


namespace Controller
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private Vector3 _movementVector = new Vector3(0.0f, 0.0f);
        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cansel = KeyCode.Escape;
        private KeyCode _moveForward = KeyCode.W;
        private KeyCode _moveBack = KeyCode.S;
        private KeyCode _moveLeft = KeyCode.A;
        private KeyCode _moveRight = KeyCode.D;

        private KeyCode _jump = KeyCode.Space;

        // private KeyCode _reloadClip = KeyCode.R;
        // private KeyCode _selectWeapon1 = KeyCode.Alpha1;
        // private KeyCode _selectWeapon2 = KeyCode.Alpha2;
        // private KeyCode _selectWeapon3 = KeyCode.Alpha3;
        private int _mouseButton = (int) MouseButton.LeftButton;

        private float _cashMouseScrollWheel = 0.0f;

        #endregion

        // private Vector3 _movementVector
        // {
        //     get
        //     {
        //         var horizontal = Input.GetAxis("Horizontal");
        //         var vertical = Input.GetAxis("Vertical");
        //
        //         return new Vector3(horizontal, 0.0f, vertical);
        //     }
        // }

        #region Mehtods

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        #endregion


        #region UnityMethods

        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch();
            }

            _movementVector = Vector3.zero;
            if (Input.GetKeyDown(_moveForward))
                _movementVector.z += 1;
            if (Input.GetKeyDown(_moveBack))
                _movementVector.z -= 1;
            if (Input.GetKeyDown(_moveLeft))
                _movementVector.x -= 1;
            if (Input.GetKeyDown(_moveRight))
                _movementVector.x += 1;
            ServiceLocator.Resolve<MoveController>().Move(_movementVector.normalized);

            if (Input.GetKeyDown(_jump))
                ServiceLocator.Resolve<MoveController>().Jump(Vector3.up);


            //if (Math.Abs(Input.GetAxis("Mouse ScrollWheel") - _cashMouseScrollWheel) > 0.1f)
            // Dbg.Log($"Vector2 scroll = Input.mouseScrollDelta; {Input.mouseScrollDelta.y}");
            // //todo работает очень криво
            // if (Input.mouseScrollDelta.y > _cashMouseScrollWheel)
            // {
            //     _cashMouseScrollWheel = Input.mouseScrollDelta.y;
            //     SelectWeapon(ServiceLocator.Resolve<Inventory>().GetLastIndexWeapon() + 1);
            // }
            // if (Input.mouseScrollDelta.y < _cashMouseScrollWheel)
            // {
            //     _cashMouseScrollWheel = Input.mouseScrollDelta.y;
            //     SelectWeapon(ServiceLocator.Resolve<Inventory>().GetLastIndexWeapon() - 1);
            // }


            // if (Input.GetMouseButton(_mouseButton))
            // {
            //     if (ServiceLocator.Resolve<WeaponController>().IsActive)
            //     {
            //         ServiceLocator.Resolve<WeaponController>().Fire();
            //     }
            // }

            if (Input.GetKeyDown(_cansel))
            {
                // ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }

            // if (Input.GetKeyDown(_reloadClip))
            // {
            //     if (ServiceLocator.Resolve<WeaponController>().IsActive)
            //     {
            //         ServiceLocator.Resolve<WeaponController>().ReloadClip();
            //     }
            // }
        }

        #endregion
    }
}