using UnityEngine;
using UnityEngine.UI;


namespace View
{
    public sealed class FlashLightUiText : MonoBehaviour
    {

        #region Properties

        public float Text
        {
            set => _text.text = $"{value:0.0}";
        }

        public Color Color
        {
            set => _text.color = value;
        }      

        public void SetActive(bool value)
        {
            //print($"FlashLightUiText.SetActive({value})");
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
            //print($"FlashLightUiText.Awake()");
        }

        #endregion
    }
}