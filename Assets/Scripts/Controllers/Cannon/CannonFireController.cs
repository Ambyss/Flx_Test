using Components.Views;
using Data;
using DG.Tweening;
using Extensions;
using PepixSignals;
using UnityEngine;
using Zenject;

namespace Controllers.Cannon
{
    public class CannonFireController : ITickable
    {
        [Inject] private CannonView _view;
        [Inject] private ProjectileSettings _settings;
        [Inject] private CannonAimSettings _aimSettings;
        [Inject] private ProjectileFactory _projectileFactory;
        [Inject] private VFXFactory _vfxFactory;

        public readonly TheSignal OnForceChanged = new();
        public readonly TheSignal OnFired = new();
        
        private float _fireForce;
        private Vector3 _barrelStartPos;
        private float _fireTimer;

        public float FireForce => _fireForce;

        [Inject]
        private void Construct()
        {
            _barrelStartPos = _view.Barrel.localPosition;
        }
        
        public void UpdateFireForce(float value)
        {
            _fireForce = value.Remap(0, 1, _settings.ForceMinMax.x, _settings.ForceMinMax.y);
            OnForceChanged.Dispatch();
        }

        public void Tick()
        {
            if (_fireTimer > 0)
            {
                _fireTimer -= Time.deltaTime;
                return;
            }
            if (Input.GetKey(KeyCode.Space))
                Fire();
        }

        private void Fire()
        {
            _fireTimer = _aimSettings.FirePeriod; 
                
            var projectile = _projectileFactory.Create();
            projectile.transform.position = _view.ProjectileSpawnPos.position;
            projectile.Controller.Launch(_fireForce * _view.ProjectileSpawnPos.forward);

            _view.Barrel.DOKill();
            _view.Barrel.DOMove(_view.Barrel.position + _view.Barrel.right * _aimSettings.FireFeedbackValue, _aimSettings.FireFeedbackTime).SetEase(Ease.OutSine).OnComplete(
                () =>
                {
                    _view.Barrel.DOLocalMove(_barrelStartPos, _aimSettings.FireFeedbackTime).SetEase(Ease.InSine);
                });
            
            var fx = _vfxFactory.Create(ParticleType.Muzzle);
            fx.transform.position = _view.ProjectileSpawnPos.position;
            fx.transform.rotation = _view.ProjectileSpawnPos.rotation;
            OnFired.Dispatch();
        }
    }
}