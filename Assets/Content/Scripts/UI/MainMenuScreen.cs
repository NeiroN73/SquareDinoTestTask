using GameCore.UI;
using UnityEngine;

namespace Game.UI
{
    public class MainMenuScreen : Screen<MainMenuViewModel>
    {
        [SerializeField] private InputFieldTextChangedViewBinder _playerName = new("playerName");
        [SerializeField] private ButtonViewBinder _joinButton = new("joinButton");
        
        public override void Initialize()
        {
            Bind(_playerName, _joinButton);
        }
    }
}