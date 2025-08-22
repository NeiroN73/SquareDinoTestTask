using UnityEngine;

namespace Game.Installers
{
    public class AppInstaller : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}