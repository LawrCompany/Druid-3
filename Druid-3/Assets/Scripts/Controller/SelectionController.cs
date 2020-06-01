using System;
using Helper;
using Interface;
using Model;
using UnityEngine;


namespace Controller
{
    public sealed class SelectionController : BaseController, IExecute
    {
        #region Properties


        #endregion


        #region Fields

        private readonly Camera _mainCamera;
        private readonly Vector2 _center;
        private readonly float _dedicateDistance = 20.0f;
        private GameObject _dedicateObject;
        private ISelectObject _selectObject;
        private bool _nullString;
        private bool _isSelectedObject;

        #endregion


        #region UnityMethods


        #endregion


        #region Methods

        public SelectionController()
        {
            _mainCamera = Camera.main;
            _center = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        }



        public void Execute()
        {
            if (!IsActive) return;
            if (Physics.Raycast(
                _mainCamera.ScreenPointToRay(_center),
                out var hit,
                _dedicateDistance))
            {
                SelectObject(hit.collider.gameObject);
                _nullString = false;
            }
            else if (!_nullString)
            {
                UiInterface.SelectionObjMessageUi.Text = string.Empty;
                _nullString = true;
                _dedicateObject = null;
                _isSelectedObject = false;
            }

            if (_isSelectedObject)
            {
                //действие над объектом

                switch (_selectObject)
                {
                    // case Weapon aim:

                        // в инвентарь


                        //Inventory.AddWeapon(aim)
                        // break;
                    case Eat eat:
                        ServiceLocator.Resolve<PlayerController>().Healing(eat);
                        break;
                    case Wall wall:
                        break;
                }
            }
        }

        public void SelectObject(GameObject obj)
        {
            if (obj == _dedicateObject) return;
            _selectObject = obj.GetComponent<ISelectObject>();
            if (_selectObject != null)
            {
                UiInterface.SelectionObjMessageUi.Text = _selectObject.GetMessage();
                _isSelectedObject = true;
            }
            else
            {
                UiInterface.SelectionObjMessageUi.Text = String.Empty;
                _isSelectedObject = false;
            }

            _dedicateObject = obj;
        }

        #endregion
    }
}