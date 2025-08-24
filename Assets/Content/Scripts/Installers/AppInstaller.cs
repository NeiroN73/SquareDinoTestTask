using Content.Scripts.Installers;
using Game.LifetimeScopes;
using UnityEngine;

namespace Game.Installers
{
    public class AppInstaller : GameCoreController
    {
        [SerializeField] private AppLifetimeScope _appLifetimeScope;

        protected override void Awake()
        {
            _appLifetimeScope.Build();
            
            base.Awake();
        }
    }
}