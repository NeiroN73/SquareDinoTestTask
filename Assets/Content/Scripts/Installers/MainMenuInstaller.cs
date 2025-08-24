using Game.UI.MainMenu;
using GameCore.Services;
using UnityEngine;
using VContainer;

namespace Game.Installers
{
    public class MainMenuInstaller : MonoBehaviour
    {
        [Inject] private ScreensService _screensService;
        
        private async void Awake()
        {
            await _screensService.OpenAsync<MainMenuScreen>();
        }
    }
}