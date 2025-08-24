using System;
using Game.Handlers;
using Game.NetworkManagers;
using Mirror;
using R3;
using VContainer;

namespace Game.Components
{
    public abstract class HandlerComponent : NetworkBehaviour, IDisposable
    {
        protected IObjectResolver ObjectResolver { get; private set; }
        protected NetworkHandler Handler;
        protected CompositeDisposable Disposable = new();
        
        public override void OnStartClient()
        {
            base.OnStartClient();

            if (NetworkManager.singleton is GameNetworkManager manager) // костыльный инжект, ничего лучше не придумать?
            {
                ObjectResolver = manager.ObjectResolver;
                ObjectResolver.Inject(this);
            }
        }
        
        public void Initialize(NetworkHandler handler)
        {
            Handler = handler;
        }

        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}