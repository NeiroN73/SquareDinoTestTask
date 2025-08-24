using GameCore.UI;
using UnityEngine;

namespace Game.UI.PlayerName
{
    public class PlayerNameView : View<PlayerNameViewModel>
    {
        [SerializeField] private TextViewBinder _name = new("name");
        
        public override void Initialize()
        {
            Bind(_name);
        }
    }
    
    public class PlayerNameViewModel : ViewModel
    {
        public ViewModelBinder<string> Name { get; } = new("name");
        
        public override void Initialize()
        {
            Bind(Name);
        }
    }
}