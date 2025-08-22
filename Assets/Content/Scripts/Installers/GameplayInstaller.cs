using GameCore.Factories;
using GameCore.Services;
using UnityEngine;
using VContainer;

namespace Game.Installers
{
    public class GameplayInstaller : MonoBehaviour
    {
        [Inject] private HandlersFactory _entitiesFactory;
        [Inject] private ScreensService _screensService;
        
        private void Awake()
        {
            // var board = _entitiesFactory.Create(_boardPrefab);
            // board.Initialize();
        }
    }
}