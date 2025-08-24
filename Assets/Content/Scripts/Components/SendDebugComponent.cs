using Mirror;
using R3;
using UnityEngine;

namespace Game.Components
{
    public class SendDebugComponent : HandlerComponent
    {
        private ChangeNameComponent _changeNameComponent;

        public void Init(ControllerComponent controllerComponent, ChangeNameComponent changeNameComponent)
        {
            controllerComponent.DebugPerformed.Subscribe(SendChatMessage).AddTo(Disposable);
            
            _changeNameComponent = changeNameComponent;
        }
        
        private void SendChatMessage()
        {
            if (isServer)
            {
                RpcReceiveChatMessage(_changeNameComponent.UserName);
            }
            else
            {
                CmdSendChatMessage(_changeNameComponent.UserName);
            }
        }
        
        [Command]
        private void CmdSendChatMessage(string sender)
        {
            RpcReceiveChatMessage(sender);
        }
        
        [ClientRpc]
        private void RpcReceiveChatMessage(string sender)
        {
            Debug.Log($"Привет от {sender}");
        }
    }
}