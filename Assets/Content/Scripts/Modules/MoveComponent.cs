using Game.Configs;
using UnityEngine;

namespace Game.Components
{
    public class MoveComponent : HandlerComponent
    {
        private CharacterController _characterController;
        private ControllerComponent _controllerComponent;

        private MoveData _moveData;

        public void Init(CharacterController characterController, ControllerComponent controllerComponent, MoveData moveData)
        {
            _characterController = characterController;
            _controllerComponent = controllerComponent;
            _moveData = moveData;
        }

        public override void Tick(float deltaTime)
        {
            if(!isLocalPlayer)
                return;

            if (!Handler)
                return;
            
            var moveDirection = _controllerComponent.MoveDirection;
            moveDirection = Handler.transform.TransformDirection(moveDirection);
            moveDirection *= _moveData.Speed;
        
            _characterController.Move(moveDirection * Time.deltaTime);
        }
    }
}