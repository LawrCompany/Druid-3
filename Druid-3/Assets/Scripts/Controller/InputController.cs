using Enums;
using Interface;
using UnityEngine;


namespace Controller
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cansel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _selectWeapon1 = KeyCode.Alpha1;
        private KeyCode _selectWeapon2 = KeyCode.Alpha2;
        private KeyCode _selectWeapon3 = KeyCode.Alpha3;
        private int _mouseButton = (int)MouseButton.LeftButton;

        private float _cashMouseScrollWheel = 0.0f;

        #endregion


        #region Mehtods

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // private void SelectWeapon(int i)
        // {
        //     ServiceLocator.Resolve<WeaponController>().Off();
        //     var tempWeapon = ServiceLocator.Resolve<Inventory>().GetWeaponByIndex(i);
        //     if (tempWeapon != null)
        //     {
        //         //Debug.Log($"tempWeapon: {tempWeapon}, name: {tempWeapon.name}");
        //         ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
        //     }
        // }

        #endregion


        #region UnityMethods

        public void Execute()
        {
            //Debug.Log($"InputController.Execute - IsActive: {IsActive}, Input.GetKeyDown({Input.GetKeyDown(_activeFlashLight)})");
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(); //todo сделать вариант смены фонарика
                //Switch(ServiceLocator.Resolve<Inventory>().FlashLight);
            }

            // if (Input.GetKeyDown(_selectWeapon1))
            // {
            //     SelectWeapon(0);
            // }
            //
            // if (Input.GetKeyDown(_selectWeapon2))
            // {
            //     SelectWeapon(1);
            // }
            //
            // if (Input.GetKeyDown(_selectWeapon3))
            // {
            //     SelectWeapon(2);
            // }


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