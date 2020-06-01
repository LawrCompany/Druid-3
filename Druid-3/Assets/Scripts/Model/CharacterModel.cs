using UnityEngine;


namespace Model
{
    public class CharacterModel : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _maxHeals = 100.0f;
        [SerializeField] private float _heals;

        #endregion

        
        #region Methods

        public CharacterModel()
        {
            _heals = _maxHeals;
        }
        
        public void AddHeals(float valuePoint)
        {
            _heals = +valuePoint;
            if (_heals > _maxHeals)
            {
                _heals = _maxHeals;
            }
        }

        public float GetHeals()
        {
            return _heals;
        }

        #endregion
    }
}