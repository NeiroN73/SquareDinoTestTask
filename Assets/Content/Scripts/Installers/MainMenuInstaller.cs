using Cysharp.Threading.Tasks;
using Game.UI;
using GameCore.Services;
using UnityEngine;
using VContainer;

namespace Game.Installers
{
    public class MainMenuInstaller : MonoBehaviour
    {
        [Inject] private ScreensService _screensService;
        
        private void Awake()
        {
            _screensService.OpenAsync<MainMenuScreen>().Forget();
        }
    }
}