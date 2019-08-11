using Core.Controller.User;
using DeveGames.PopupSystem.Scripts;
using Zenject;

namespace Core.UI.Screens
{
    public class StartScreen : Popup<StartScreen>
    {
        private UserDataController _userDataController;
        
        [Inject]
        private void Construct(UserDataController userDataController)
        {
            _userDataController = userDataController;
        }
        
        #region UI Event Listeners

        public void OnSettingsButtonClicked()
        {
            
        }
        
        public void OnStoreButtonClicked()
        {
            
        }
        
        public void OnNoAdsButtonClicked()
        {
            
        }
        
        public void OnResetButtonClicked()
        {
            _userDataController.Reset();
        }

        #endregion   
    }
}
