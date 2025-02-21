using Controllers.Cannon;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Components.Views
{
    public class CannonView : MonoBehaviour
    {
        [SerializeField] private Transform _barrel;
        [SerializeField] private Transform _foundation;
        [SerializeField] private Transform _projectileSpawnPos;
        [SerializeField, BoxGroup("Trajectory")] private LineRenderer _trajectory;
        [SerializeField, BoxGroup("Trajectory")] private Transform _hitMarker;

        [Inject] public CannonFireController FireController { get; }
        
        public Transform Barrel => _barrel;

        public Transform Foundation => _foundation;

        public Transform ProjectileSpawnPos => _projectileSpawnPos;

        public LineRenderer Trajectory => _trajectory;

        public Transform HitMarker => _hitMarker;
    }
}