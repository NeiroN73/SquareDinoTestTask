using Game.Handlers;
using GameCore.Factories;
using Mirror;
using VContainer;

namespace Game.NetworkManagers
{
    public class GameNetworkManager : NetworkManager
    {
        [Inject] private HandlersFactory _handlersFactory;
        [Inject] public IObjectResolver ObjectResolver { get; private set; }
        
        public override void OnStartServer()
        {
            NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreateCharacter);
        }
        
        public override void OnClientConnect()
        {
            if (!NetworkClient.ready)
            {
                NetworkClient.Ready();
            }
        }

        public void RequestPlayerSpawn(string playerName)
        {
            if (!NetworkClient.active)
            {
                return;
            }

            if (!NetworkClient.ready)
            {
                NetworkClient.Ready();
            }

            CreatePlayerMessage message = new()
            {
                Name = playerName
            };
            
            NetworkClient.Send(message);
        }
        
        private void OnCreateCharacter(NetworkConnectionToClient conn, CreatePlayerMessage message)
        {
            if (!conn.isReady)
            {
                return;
            }

            var player = _handlersFactory.Create<PlayerHandler>();
            NetworkServer.AddPlayerForConnection(conn, player.gameObject);
            player.TargetRpcInitialize(conn);
            player.RpcSetName(message.Name);
        }

        private struct CreatePlayerMessage : NetworkMessage
        {
            public string Name;
        }
    }
}