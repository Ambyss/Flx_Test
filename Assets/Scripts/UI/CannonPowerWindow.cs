using Components.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class CannonPowerWindow : MonoBehaviour, IInitializable
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _forceText;
        
        [Inject] private CannonView _cannonView;

        public void Initialize()
        {
            _slider.onValueChanged.AddListener(SliderValueChanged);
            _slider.value = .5f;
            SliderValueChanged(.5f);
        }

        private void UpdateText()
        {
            _forceText.text = $"{(int)(_slider.value * 100)}";
        }

        private void SliderValueChanged(float value)
        {
            _cannonView.FireController.UpdateFireForce(value);
            UpdateText();
        }
    }
}