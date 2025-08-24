using System.Linq;
using Game.Services;
using GameCore.Configs;
using GameCore.LifetimeScopes;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.LifetimeScopes
{
    // хз почему не работает через parent, так что пришлось всё в один скоп запихать, потом разберусь(
    public class AppLifetimeScope : GameCoreLifetimeScope
    {
        [SerializeField] private AssetLabelReference _gameConfigsAssetLabel;
        
        protected override void RegisterConfigs()
        {
            base.RegisterConfigs();
            
            var configs = Addressables.LoadAssetsAsync<Config>(_gameConfigsAssetLabel, null)
                .WaitForCompletion().ToList();
            foreach (var config in configs)
            {
                Register(config);
            }
        }

        protected override void RegisterServices()
        {
            base.RegisterServices();
            Register<NetworkService>();
        }
    }
}