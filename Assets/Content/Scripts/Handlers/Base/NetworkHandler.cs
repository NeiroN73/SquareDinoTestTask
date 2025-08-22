using GameCore.Handlers;
using Mirror;
using UnityEngine;

namespace Game.Handlers
{
    public abstract class NetworkHandler : NetworkBehaviour, IHandlerable
    {
        [field: SerializeField] public string Id { get; private set; }
    }
}