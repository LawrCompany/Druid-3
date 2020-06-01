using UnityEngine;
using UnityEngine.UI;


namespace View
{
    public class CharacterUi: MonoBehaviour
    {
        #region Properties

        public float Text
        {
            set => _text.text = $"Heals: {value:0.0}";
        }

        public Color Color
        {
            set => _text.color = value;
        }      

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }

        #endregion


        #region Fields

        private Text _text;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        #endregion
    }
}