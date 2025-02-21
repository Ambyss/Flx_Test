using Controllers;
using UnityEngine;
using Zenject;

namespace Components.Views
{
    public class ProjectileView : MonoBehaviour, IPoolable<IMemoryPool>
    {
        [SerializeField] private MeshFilter _meshFilter;
        
        private IMemoryPool _pool;
        [Inject] public ProjectileController Controller { get; }

        public MeshFilter MeshFilter => _meshFilter;

        public void Destroy()
        {
            _pool.Despawn(this);
        }
        
        public void OnDespawned()
        {
            gameObject.SetActive(false);
        }

        public void OnSpawned(IMemoryPool p1)
        {
            gameObject.SetActive(true);
            _pool = p1;
        }
    }
}