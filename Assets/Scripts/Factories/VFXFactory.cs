using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using Zenject;

namespace Components.Views
{
    public class VFXFactory : PlaceholderFactory<ParticleType, VFXItem>
    {
        [Inject] private List<VFXPool> _vfxPools;
        
        private VFXPool GetPool(ParticleType type)
        {
            return _vfxPools.FirstOrDefault(x => x.Type == type);
        }
        
        public override VFXItem Create(ParticleType type)
        {
            return GetPool(type).Spawn();
        }
    }
}