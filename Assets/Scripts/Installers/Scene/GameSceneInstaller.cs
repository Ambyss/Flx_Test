using Components.Views;
using Controllers.Cannon;
using Data;
using Installers.Object;
using UnityEngine;
using Zenject;

namespace Installers.Scene
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Inject] private ProjectileSettings _projectileSettings;
        [Inject] private VFXDatabase _vfxDatabase;
        
        public override void InstallBindings()
        {
            Container.Bind<CannonView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<CamLink>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CamShakeController>().AsSingle().NonLazy();
            BindFactories();
        }

        private void BindFactories()
        {
            Container.BindFactory<ProjectileView, ProjectileFactory>()
            .FromPoolableMemoryPool(poolbinder => poolbinder
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<ProjectileInstaller>(_projectileSettings.ProjectilePrefab)
                .UnderTransformGroup("Projectiles"));

            Container.BindFactory<ParticleType, VFXItem, VFXFactory>();
            foreach (var particle in _vfxDatabase.Particles)
            {
                Container.BindMemoryPool<VFXItem, VFXPool>()
                    .WithFactoryArguments(particle.Key)
                    .FromComponentInNewPrefab(particle.Value)
                    .UnderTransformGroup("VFX");
            }

            Container.BindFactory<MeshRenderer, ImpactMarkFactory>()
                .FromComponentInNewPrefab(_projectileSettings.ImpactMarkPrefab)
                .UnderTransformGroup("Impact marks");
        }
    }
}