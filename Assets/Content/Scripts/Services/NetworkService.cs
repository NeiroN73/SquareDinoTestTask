using Game.Handlers;
using GameCore.Services;
using Mirror;
using UnityEngine;

namespace Game.Services
{
    public class NetworkService : Service
    {
        public void ServerAddPlayer(PlayerHandler player)
        {
            NetworkServer.AddPlayerForConnection(NetworkServer.localConnection, player.gameObject);
            Debug.Log($"Player connected: {NetworkServer.localConnection.connectionId}");
        }
    }
}