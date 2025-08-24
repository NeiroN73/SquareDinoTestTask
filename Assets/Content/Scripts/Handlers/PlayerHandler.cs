using Game.Components;
using Game.Configs;
using GameCore.Factories;
using GameCore.Services;
using Mirror;
using UnityEngine;
using VContainer;

namespace Game.Handlers
{
    public class PlayerHandler : NetworkHandler
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private ControllerComponent _controllerComponent;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private SpawnComponent _spawnComponent;
        [SerializeField] private SendDebugComponent _sendDebugComponent;
        [SerializeField] private ChangeNameComponent _changeNameComponent;
        
        [Inject] private PlayerConfig _playerConfig;
        [Inject] private TickService _tickService;
        [Inject] private ViewsFactory _viewsFactory;
        [Inject] private HandlersFactory _handlersFactory;
        
        [TargetRpc]
        public void TargetRpcInitialize(NetworkConnection conn)
        {
            Components = new()
            {
                _controllerComponent,
                _moveComponent,
                _spawnComponent,
                _changeNameComponent,
                _sendDebugComponent
            };
            
            foreach (var component in Components)
            {
                component.Initialize(this);
            }
            
            _controllerComponent.Init();
            _moveComponent.Init(_characterController, _controllerComponent, _playerConfig.MoveData, _animator);
            _spawnComponent.Init(_controllerComponent);
            _sendDebugComponent.Init(_controllerComponent, _changeNameComponent);
            
            _tickService.RegisterTick(_moveComponent);
        }

        [ClientRpc]
        public void RpcSetName(string newName)
        {
            _changeNameComponent.Init(newName);
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            
            foreach (var component in Components)
            {
                component?.Dispose();
            }
        }
    }
}