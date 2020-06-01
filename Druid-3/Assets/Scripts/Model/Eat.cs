using Interface;
using UnityEngine;


namespace Model
{
    public class Eat: BaseObjectScene, ISelectObject
    {
        #region Fields

        [SerializeField] private float _healingPoint = 20;

        #endregion

        
        #region Properties

        public float HealingPoint => _healingPoint;

        #endregion


        #region Methods

        public string GetMessage()
        {
            return $"It`s healing in {_healingPoint}";
        }

        #endregion
    }
}