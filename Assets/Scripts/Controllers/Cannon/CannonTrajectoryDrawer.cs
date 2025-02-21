using Components.Views;
using Data;
using UnityEngine;
using Zenject;

namespace Controllers.Cannon
{
    public class CannonTrajectoryDrawer
    {
        [Inject] private CannonView _view;
        [Inject] private CannonFireController _fireController;
        [Inject] private CannonAimController _aimController;
        [Inject] private ProjectileSettings _projectileSettings;
        [Inject] private CannonAimSettings _cannonAimSettings;

        private Vector3[] positions;
        private Vector3 _gravity;
        private Ray _ray;
        private RaycastHit[] _hitResults;

        [Inject]
        private void Construct()
        {
            _fireController.OnForceChanged.On(UpdateTrajectory);
            _aimController.OnDirectionChanged.On(UpdateTrajectory);
            _view.Trajectory.positionCount = _cannonAimSettings.TrajectoryDrawPoints;
            positions = new Vector3[_cannonAimSettings.TrajectoryDrawPoints];
            _gravity = Vector3.down * _projectileSettings.Gravity;
            _hitResults = new RaycastHit[1];
        }

        private void UpdateTrajectory()
        {
            var direction = _view.ProjectileSpawnPos.forward * _fireController.FireForce;
            int posCount = positions.Length;

            for (int i = 0; i < positions.Length; i++)
            {
                var t = i * _cannonAimSettings.TrajectoryDrawTimestep;
                var point = _view.ProjectileSpawnPos.position + direction * t - .5f * _gravity * t * t;
                positions[i] = point;
                if (i == 0) continue;
                _ray.origin = positions[i - 1];
                _ray.direction = positions[i] - positions[i - 1];
                var hitsCount = Physics.RaycastNonAlloc(_ray, _hitResults, _ray.direction.magnitude, _projectileSettings.CollisionMask);
                if (hitsCount > 0)
                {
                    posCount = i + 1;
                    var hitResult = _hitResults[0];
                    positions[i] = hitResult.point;
                    _view.HitMarker.position = hitResult.point + hitResult.normal * .01f;
                    _view.HitMarker.rotation = Quaternion.LookRotation(hitResult.normal);
                    break;
                }
            }

            _view.Trajectory.positionCount = posCount;
            _view.Trajectory.SetPositions(positions);
        }
    }
}