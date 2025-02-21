using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/VFX database")] 
    public class VFXDatabase : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<ParticleType, ParticleSystem> _particles;

        public Dictionary<ParticleType, ParticleSystem> Particles => _particles;
    }

    public enum ParticleType
    {
        Muzzle,
        Hit,
        Death
    }
}