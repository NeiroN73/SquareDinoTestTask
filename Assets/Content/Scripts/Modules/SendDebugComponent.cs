using Mirror;
using R3;
using UnityEngine;

namespace Game.Components
{
    public class SendDebugComponent : HandlerComponent
    {
        private PlayerNameComponent _playerNameComponent;

        public void Init(ControllerComponent controllerComponent, PlayerNameComponent playerNameComponent)
        {
            controllerComponent.DebugPerformed.Subscribe(SendChatMessage).AddTo(Disposable);
            _playerNameComponent = playerNameComponent;
        }
        
        private void SendChatMessage()
        {
            string message = $"Привет от {_playerNameComponent.UserName}";
            CmdSendChatMessage(message);
        }
        
        [Command]
        private void CmdSendChatMessage(string message)
        {
            RpcReceiveChatMessage(_playerNameComponent.UserName, message);
        }
        
        [ClientRpc]
        private void RpcReceiveChatMessage(string senderName, string message)
        {
            //UIManager.Instance?.AddChatMessage(senderName, message);
        }
    }
}