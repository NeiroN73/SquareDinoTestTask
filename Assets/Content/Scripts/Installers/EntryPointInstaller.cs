using GameCore.Services;
using UnityEngine;
using VContainer;

namespace Game.Installers
{
    public class EntryPointInstaller : MonoBehaviour
    {
        [Inject] private ScenesService _scenesService;
        
        private async void Awake()
        {
            await _scenesService.LoadSceneAsync(SceneNames.MainMenu);
        }
    }
}