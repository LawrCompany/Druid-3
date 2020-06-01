using Interface;
using Model;
using UnityEngine;


namespace Controller
{
    public sealed class PlayerController : BaseController, IExecute, IInitualization
    {
        #region Fields

        private readonly IMotor _motor;

        private CharacterModel _characterModel;

        #endregion


        #region UnityMethods

        public void Initialization()
        {
            _characterModel = Object.FindObjectOfType<CharacterModel>();
        }

        public PlayerController(IMotor motor)
        {
            _motor = motor;
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            _motor.Move();
            UiInterface.CharacterUi.Text = _characterModel.GetHeals();
        }

        #endregion


        public void Healing(Eat eat)
        {
            _characterModel.AddHeals(eat.HealingPoint);
        }
    }
}