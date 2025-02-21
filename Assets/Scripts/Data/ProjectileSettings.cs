using Components.Views;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SO/Projectile settings")]
    public class ProjectileSettings : ScriptableObject
    {
        [SerializeField] private ProjectileView _projectilePrefab;
        [SerializeField] private float _gravity;
        [SerializeField] private Vector2 _forceMinMax;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private LayerMask _collisionMask;
        [SerializeField] private float _bounciness;
        [SerializeField] private int _numOfPossibleCollisions;
        [SerializeField] private MeshRenderer _impactMarkPrefab;

        public float Gravity => _gravity;

        public Vector2 ForceMinMax => _forceMinMax;

        public ProjectileView ProjectilePrefab => _projectilePrefab;

        public float RotationSpeed => _rotationSpeed;

        public LayerMask CollisionMask => _collisionMask;

        public float Bounciness => _bounciness;

        public int NumOfPossibleCollisions => _numOfPossibleCollisions;

        public MeshRenderer ImpactMarkPrefab => _impactMarkPrefab;
    }
}