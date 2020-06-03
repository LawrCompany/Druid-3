using System;
using Helper;
using UnityEngine;


namespace Controller
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields

        private Controllers _controllers;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }

        private void Update()
        {
            // Dbg.Log($"GameController._controllers.Lenghth: {_controllers.Lenghth}");
            for (var i = 0; i < _controllers.Lenghth; i++)
            {
                _controllers[i].Execute();
            }
        }

        private void FixedUpdate()
        {
            // Dbg.Log($"GameController._controllers.FixedControllersLenghth: {_controllers.FixedControllersLenghth}");
            for (var i = 0; i < _controllers.FixedControllersLenghth; i++)
            {
                _controllers.FixedExecuteControllers[i].FixedExecute();
            }
        }

        #endregion
    }
}