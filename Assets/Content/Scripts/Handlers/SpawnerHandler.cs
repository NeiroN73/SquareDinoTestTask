using System;
using GameCore.Factories;
using Mirror;
using VContainer;

namespace Game.Handlers
{
    public class SpawnerHandler : NetworkHandler
    {
        [Inject] private HandlersFactory _handlersFactory;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            //CmdSpawn();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            
            //CmdSpawn();
        }

        [Command]
        private void CmdSpawn()
        {
            var player = _handlersFactory.Create<PlayerHandler>();
            _handlersFactory.InitializeHandler(player);
            player.Initialize();
            
            NetworkServer.Spawn(player.gameObject);
        }
    }
}