using Components.Views;
using Controllers;
using Zenject;

namespace Installers.Object
{
    public class ProjectileInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<ProjectileView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<ProjectileController>().AsSingle().NonLazy();
        }
    }
}