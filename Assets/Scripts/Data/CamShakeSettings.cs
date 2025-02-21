using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/Cam shake")]
    public class CamShakeSettings : ScriptableObject
    {
        [SerializeField] private float _frequency;
        [SerializeField] private float _amplitude;
        [SerializeField] private float _fadeOutTime;

        public float Frequency => _frequency;

        public float Amplitude => _amplitude;

        public float FadeOutTime => _fadeOutTime;
    }
}