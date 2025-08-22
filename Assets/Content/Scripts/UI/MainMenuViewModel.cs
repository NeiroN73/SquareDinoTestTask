using Game.Handlers;
using Game.Services;
using GameCore.Factories;
using GameCore.Services;
using GameCore.UI;
using GameCore.UI.Loading;
using GameCore.Utils;
using R3;
using VContainer;

namespace Game.UI
{
    public class MainMenuViewModel : ViewModel
    {
        [Inject] private ScreensService _screensService;
        [Inject] private ScenesService _scenesService;
        [Inject] private HandlersFactory _handlersFactory;
        [Inject] private NetworkService _networkService;
        
        private readonly RefTypeViewModelBinder<ReactiveCommand<string>> _playerNameInputField = new("playerName");
        private readonly RefTypeViewModelBinder<ReactiveCommand> _joinButton = new("joinButton");

        private string _playerName;
        
        public override void Initialize()
        {
            Bind(_playerNameInputField, _joinButton);

            _playerNameInputField.Value.Subscribe(OnPlayerNameChanged).AddTo(Disposable);
            _joinButton.Value.Subscribe(OnJoinClicked).AddTo(Disposable);
        }

        private void OnPlayerNameChanged(string code)
        {
            _playerName = code;
        }
        
        private async void OnJoinClicked()
        {
            _screensService.Close();
            _screensService.OpenLoading<LoadingScreen>();
            var player = _handlersFactory.Create<PlayerHandler>();
            _networkService.ServerAddPlayer(player);
            _screensService.CloseLoading();
        }
    }
}