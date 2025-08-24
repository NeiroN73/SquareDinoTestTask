using System;
using Game.Handlers;
using Mirror;
using R3;

namespace Game.Components
{
    public abstract class HandlerComponent : NetworkBehaviour, IDisposable
    {
        protected NetworkHandler Handler;
        protected CompositeDisposable Disposable = new();
        
        public virtual void Initialize(NetworkHandler handler)
        {
            Handler = handler;
        }

        public virtual void Tick(float deltaTime) {}

        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}