using Interface;
using Model;
using UnityEngine;
using View;


namespace Controller
{
    public sealed class PlayerController : BaseController,  IExecute, IInitualization
    {
        #region Fields

        private readonly IMotor _motor;

        private CharacterModel _characterModel;
        // private CharacterUi _characterUi;

        #endregion


        #region UnityMethods

        public void Initialization()
        {
            _characterModel = Object.FindObjectOfType<CharacterModel>();
            // _characterUi = Object.FindObjectOfType<CharacterUi>();
        }
        
        public PlayerController(IMotor motor)
        {
            _motor = motor;
        }

        public void Execute()
        {
            if (!IsActive) { return; }
            _motor.Move();
            // var test = _characterModel.GetHeals();
            // UiInterface.CharacterUi.Text = test;
        }

        #endregion


        public void Healing(Eat eat)
        {
            _characterModel.AddHeals(eat.HealingPoint);
        }
    }
}