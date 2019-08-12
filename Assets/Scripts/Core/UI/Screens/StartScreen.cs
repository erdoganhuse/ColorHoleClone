using Core.Controller.Level;
using Core.Controller.User;
using DeveGames.PopupSystem.Scripts;
using DeveGames.PopupSystem.Scripts.AnimationStrategy;
using Zenject;

namespace Core.UI.Screens
{
    public class StartScreen : Popup<StartScreen>
    {
        private UserDataController _userDataController;
        private LevelController _levelController;
        
        [Inject]
        private void Construct(
            UserDataController userDataController,
            LevelController levelController)
        {
            _userDataController = userDataController;
            _levelController = levelController;
        }

        private void Awake()
        {
            SetAnimationStrategy(new SubtleAnimationStrategy());
        }
        
        #region UI Event Listeners

        public void OnSwipeToStartButtonClicked()
        {
            Close();
            CloseListener(() =>
            {
                _levelController.StartLevel();
            });
        }
        
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
