using Data;
using Zenject;

namespace Components.Views
{
    public class VFXPool : MonoMemoryPool<VFXItem>
    {
        [Inject] private ParticleType _type;

        public ParticleType Type => _type;

        protected override void Reinitialize(VFXItem item)
        {
            item.gameObject.SetActive(true);
            
        }

        protected override void OnSpawned(VFXItem item)
        {
            item.OnStopped.Once(() =>
            {
                Despawn(item);
            });
        }

        protected override void OnDespawned(VFXItem item)
        {
            item.gameObject.SetActive(false);
        }
    }
}