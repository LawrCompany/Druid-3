using System;
using Helper;
using Interface;
using Model;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Controller
{
    public sealed class MoveController: BaseController, IInitualization, IFixedExecute
    {
        #region Fields

        private CharacterModel _characterModel;

        private Vector3 _cashMovementVector = Vector3.zero;
        private Vector3 _cashJumpVector = Vector3.zero;
        
        #endregion


        #region UnityMethods

        public void Initialization()
        {
            Debug.Log($"Object.FindObjectOfType<CharacterModel>();{Object.FindObjectOfType<CharacterModel>()}");
            _characterModel = Object.FindObjectOfType<CharacterModel>();
        }
        
        public void FixedExecute()
        {
            if(!IsActive) return;
            
            MoveLogic();
            JumpLogic();
        }

        private void MoveLogic()
        {
            // Dbg.Log($"MoveController.MoveLogic _cashMovementVector=:{_cashMovementVector}");
            if (_characterModel.IsGrounded /*&& (_cashMovementVector != Vector3.zero)*/)
            {
                _characterModel.Rigidbody.AddForce(_cashMovementVector * _characterModel.Speed, ForceMode.Impulse);
                _characterModel.NormalizeVelocity();
            }
        }

        private void JumpLogic()
        {
            if (_characterModel.IsGrounded && (_cashJumpVector != Vector3.zero))
            {
                _characterModel.Rigidbody.AddForce(_cashJumpVector * _characterModel.JumpForce, ForceMode.Impulse);
                _cashJumpVector = Vector3.zero;
            }
        }

        #endregion

        
        #region Methods

        public void Move(Vector3 movementVector)
        {
            if(!IsActive) return;

            // Dbg.Log($"MoveController.Move movementVector=:{movementVector}");
            _cashMovementVector = movementVector;
        }

        public void Jump(Vector3 direction)
        {
            if(!IsActive) return;
            
            _cashJumpVector = GetDirectionBetweenVectors(direction, Vector3.up);
        }

        private static Vector3 GetDirectionBetweenVectors(Vector3 a, Vector3 b)
        {
            return (a + b) / 2;
        }
        
        #endregion

    }
}