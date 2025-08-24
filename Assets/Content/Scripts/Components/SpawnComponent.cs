using Game.Handlers;
using GameCore.Factories;
using Mirror;
using R3;
using VContainer;

namespace Game.Components
{
    public class SpawnComponent : HandlerComponent
    {
        [Inject] private HandlersFactory _handlersFactory;

        public void Init(ControllerComponent controllerComponent)
        {
            controllerComponent.SpawnPerformed.Subscribe(CmdSpawnCube).AddTo(Disposable);
        }

        [Command]
        private void CmdSpawnCube()
        {
            var spawnPosition = transform.position + transform.forward * 2f;
            var cube = _handlersFactory.Create<CubeHandler>(spawnPosition);
            NetworkServer.Spawn(cube.gameObject);
        }
    }
}