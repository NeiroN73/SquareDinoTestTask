using Content.Scripts.NetworkManager;
using Game.Handlers;
using Game.Services;
using GameCore.Factories;
using GameCore.Services;
using GameCore.UI;
using GameCore.UI.Loading;
using GameCore.Utils;
using Mirror;
using R3;
using VContainer;

namespace Game.UI.MainMenu
{
    public class MainMenuViewModel : ViewModel
    {
        [Inject] private ScreensService _screensService;
        [Inject] private ScenesService _scenesService;
        [Inject] private HandlersFactory _handlersFactory;
        [Inject] private NetworkService _networkService;
        
        private readonly RefTypeViewModelBinder<ReactiveCommand<string>> _playerNameInputField = new("playerName");
        private readonly RefTypeViewModelBinder<ReactiveCommand> _hostButton = new("hostButton");
        private readonly RefTypeViewModelBinder<ReactiveCommand> _joinButton = new("joinButton");

        private string _playerName;
        
        public override void Initialize()
        {
            Bind(_playerNameInputField, _hostButton, _joinButton);

            _playerNameInputField.Value.Subscribe(OnPlayerNameChanged).AddTo(Disposable);
            _hostButton.Value.Subscribe(OnHostClicked).AddTo(Disposable);
            _joinButton.Value.Subscribe(OnJoinClicked).AddTo(Disposable);
        }

        private void OnPlayerNameChanged(string code)
        {
            _playerName = code;
        }
        
        private async void OnHostClicked()
        {
            _screensService.OpenLoading<LoadingScreen>();
            
            if (!NetworkManager.singleton.isNetworkActive)
            {
                _screensService.Close();
                await _scenesService.LoadSceneAsync(SceneNames.Gameplay);
                NetworkManager.singleton.StartHost();
            }
            
            _screensService.CloseLoading();
        }
        
        private async void OnJoinClicked()
        {
            _screensService.OpenLoading<LoadingScreen>();
            
            if (!NetworkManager.singleton.isNetworkActive)
            {
                _screensService.Close();
                await _scenesService.LoadSceneAsync(SceneNames.Gameplay);
                NetworkManager.singleton.StartClient();
                // var d = NetworkManager.singleton.GetComponent<GameNetworkManager>();
                // var e = d.NetworkConnectionToClient;
                //         var l = e.identity;
                //     var w = l.GetComponent<PlayerHandler>();
                //             w.CmdSpawn();
            }
            
            _screensService.CloseLoading();
        }
    }
}