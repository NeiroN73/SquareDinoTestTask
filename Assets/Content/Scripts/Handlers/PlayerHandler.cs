using System;
using Game.Components;
using Game.Configs;
using Game.UI.PlayerName;
using GameCore.Factories;
using GameCore.Services;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using Random = UnityEngine.Random;

namespace Game.Handlers
{
    public class PlayerHandler : NetworkHandler, ITickable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        
        [Inject] private PlayerConfig _playerConfig;
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private TickService _tickService;
        [Inject] private ViewsFactory _viewsFactory;
        [Inject] private HandlersFactory _handlersFactory;
        
        private Camera _playerCamera;

        [SerializeField] private ControllerComponent _controllerComponent;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private SpawnComponent _spawnComponent;
        [SerializeField] private SendDebugComponent _sendDebugComponent;
        [SerializeField] private PlayerNameComponent _playerNameComponent;

        [SerializeField] private PlayerNameView _playerNameView;

        public override void OnStartClient()
        {
            base.OnStartClient();
            
            if (!isServer)
            {
                //CmdSpawn();
                //CmdSetUsername("");
            }
        }
        
        [Command]
        public void CmdSpawn()
        {
            var player = _handlersFactory.Create<PlayerHandler>();
            _handlersFactory.InitializeHandler(player);
            player.Initialize();
            
            NetworkServer.Spawn(player.gameObject, connectionToClient);
        }
        
        public void Initialize()
        {
            Components.Add(_controllerComponent);
            Components.Add(_moveComponent);
            Components.Add(_spawnComponent);
            Components.Add(_sendDebugComponent);
            Components.Add(_playerNameComponent);
            
            foreach (var component in Components)
            {
                _objectResolver.Inject(component);
                component.Initialize(this);
            }
            
            _controllerComponent.Init();
            _moveComponent.Init(_characterController, _controllerComponent, _playerConfig.MoveData);
            _spawnComponent.Init(_controllerComponent);
            _sendDebugComponent.Init(_controllerComponent, _playerNameComponent);
            _playerNameComponent.Init(_playerNameView);
            
            _tickService.RegisterTick(this);
        }

        public void Tick(float deltaTime)
        {
            foreach (var component in Components)
            {
                component.Tick(Time.deltaTime);
            }
        }
    }
}