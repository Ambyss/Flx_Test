using Components.Views;
using Controllers;
using Controllers.Cannon;
using Zenject;

namespace Installers.Object
{
    public class CannonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CannonView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CannonAimController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CannonFireController>().AsSingle().NonLazy();
            Container.Bind<CannonTrajectoryDrawer>().AsSingle().NonLazy();
        }
    }
}