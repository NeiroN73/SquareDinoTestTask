using System;
using Cysharp.Threading.Tasks;
using Game.Handlers;
using GameCore.Factories;
using GameCore.Utils;
using Mirror;
using R3;
using UnityEngine;
using VContainer;

namespace Content.Scripts.NetworkManager
{
    public class GameNetworkManager : Mirror.NetworkManager
    {
        [Inject] private HandlersFactory _handlersFactory;

        public NetworkConnectionToClient NetworkConnectionToClient;

        private readonly Subject<NetworkConnectionToClient> _serverAddedPlayer = new();
        public Observable<NetworkConnectionToClient> ServerAddedPlayer => _serverAddedPlayer;
        
        public override void OnStartServer()
        {
            base.OnStartServer();
            
            NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreateCharacter);
            
            Debug.Log("Custom Network Manager: Server started");
        }

        public struct CreatePlayerMessage : NetworkMessage
        {
            
        }
        
        void OnCreateCharacter(NetworkConnectionToClient conn, CreatePlayerMessage message)
        {
            //var spawner = _handlersFactory.Create<SpawnerHandler>();
            var player = _handlersFactory.Create<PlayerHandler>();
            _handlersFactory.InitializeHandler(player);
            player.Initialize();

            NetworkServer.AddPlayerForConnection(conn, player.gameObject);

            NetworkConnectionToClient = conn;
            
            //conn.identity.GetComponent<PlayerHandler>().Initialize();
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();
            
            CreatePlayerMessage characterMessage = new CreatePlayerMessage();
            NetworkClient.Send(characterMessage);
        }
    }
}