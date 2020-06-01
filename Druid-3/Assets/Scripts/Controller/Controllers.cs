using Controller.TimeRemaining;
using Helper;
using Interface;
using UnityEngine;


namespace Controller
{
    public sealed class Controllers : IInitualization
    {
        #region Properties

        public int Lenght => _executeControllers.Length;

        public IExecute this[int index] => _executeControllers[index];

        #endregion


        #region Fields

        private readonly IExecute[] _executeControllers;

        #endregion


        #region Methods

        public Controllers()
        {
            //if (Application.platform == RuntimePlatform.PS4)
            
            IMotor motor = default;
            motor = new UnitMotor(ServiceLocatorMonoBehavior.GetService<CharacterController>());
            ServiceLocator.SetService(new PlayerController(motor));
            // ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new SelectionController());
            // ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new PoolController());
            //ServiceLocator.SetService(new EffectController());

            _executeControllers = new IExecute[5];

            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();

            _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();

            _executeControllers[2] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[3] = ServiceLocator.Resolve<InputController>();

            _executeControllers[4] = ServiceLocator.Resolve<SelectionController>();
        }

        #endregion


        #region  UnityMethods   

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitualization initualization)
                {
                    initualization.Initialization();
                }
            }

            // ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<PlayerController>().Initialization();
            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<SelectionController>().On();
            // ServiceLocator.Resolve<WeaponController>().On();
            ServiceLocator.Resolve<PoolController>().Init(Object.FindObjectOfType<GameController>().transform);
            //ServiceLocator.Resolve<EffectController>().Init();
        }

        #endregion
    }
}