using Game.Services;
using GameCore.Factories;
using GameCore.LifetimeScopes;
using GameCore.Services;

namespace Game.LifetimeScopes
{
    public class AppLifetimeScope : BaseLifetimeScope
    {
        protected override void RegisterServices()
        {
            Register<AssetsLoaderService>();
            Register<ScreensService>();
            Register<ScenesService>();
            Register<NetworkService>();
        }

        protected override void RegisterFactories()
        {
            Register<ViewModelFactory>();
            Register<ViewsFactory>();
            Register<ScreensFactory>();
            Register<HandlersFactory>();
        }
    }
}