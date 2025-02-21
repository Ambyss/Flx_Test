using Cinemachine;
using Components.Views;
using Data;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Zenject;

namespace Controllers.Cannon
{
    public class CamShakeController : IInitializable
    {
        [Inject] private CamLink _camLink;
        [Inject] private CamShakeSettings _settings;
        [Inject] private CannonView _cannon;
        
        private CinemachineBasicMultiChannelPerlin _camNoise;
        private TweenerCore<float, float, FloatOptions> _tween;

        public void Initialize()
        {
            _camNoise = _camLink.VCamMain.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _camNoise.m_FrequencyGain = _settings.Frequency;
            _cannon.FireController.OnFired.On(Shake);
        }

        private void Shake()
        {
            _camNoise.m_AmplitudeGain = _settings.Amplitude;
            _tween.Kill();
            _tween = DOTween.To(() => _camNoise.m_AmplitudeGain, x => _camNoise.m_AmplitudeGain = x, 0,
                _settings.FadeOutTime).SetEase(Ease.InSine);
        }
    }
}