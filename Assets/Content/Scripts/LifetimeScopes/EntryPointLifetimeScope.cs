using Game.Services;
using GameCore.LifetimeScopes;
using VContainer;

namespace Game.LifetimeScopes
{
    public class EntryPointLifetimeScope : BaseLifetimeScope
    {
        protected override void RegisterServices()
        {
            //Register<NetworkService>();
        }
    }
}