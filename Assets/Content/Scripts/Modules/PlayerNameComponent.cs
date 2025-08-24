using System;
using Game.UI.PlayerName;
using GameCore.Factories;
using Mirror;
using UnityEngine.Serialization;
using VContainer;

namespace Game.Components
{
    public class PlayerNameComponent : HandlerComponent
    {
        [Inject] private ViewsFactory _viewsFactory;
        
        private PlayerNameView _playerNameView;

        [SyncVar(hook = nameof(OnUsernameChanged))]
        private string _userName;

        public string UserName => _userName;

        public void Init(PlayerNameView playerNameView)
        {
            _playerNameView = playerNameView;
            
            _userName = "Player_" + Guid.NewGuid();
        }
        
        [Command]
        public void CmdSetUsername(string newUsername)
        {
            if (string.IsNullOrEmpty(newUsername))
            {
                newUsername = "Player_" + Guid.NewGuid();
            }
    
            _userName = newUsername;
        }
        
        private void OnUsernameChanged(string oldValue, string newValue)
        {
            if (_playerNameView.ViewModel == null)
            {
                _viewsFactory.InitializeView(_playerNameView);
            }

            if (_playerNameView.ViewModel != null)
                _playerNameView.ViewModel.Name.Value = newValue;
        }
        
        [ClientRpc]
        private void RpcUpdateUsername(string usernameValue)
        {
            OnUsernameChanged("", usernameValue);
        }
    }
}