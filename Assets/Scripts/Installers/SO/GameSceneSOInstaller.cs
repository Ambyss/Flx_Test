using Data;
using UnityEngine;
using Zenject;

namespace Installers.SO
{
    [CreateAssetMenu(menuName = "SO/Game SO installer")]
    public class GameSceneSOInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CannonAimSettings _cannonAimSettings;
        [SerializeField] private ProjectileSettings _projectileSettings;
        [SerializeField] private VFXDatabase _vfxDatabase;
        [SerializeField] private CamShakeSettings _camShakeSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_cannonAimSettings).IfNotBound();
            Container.BindInstance(_projectileSettings).IfNotBound();
            Container.BindInstance(_vfxDatabase).IfNotBound();
            Container.BindInstance(_camShakeSettings).IfNotBound();
        }
    }
}