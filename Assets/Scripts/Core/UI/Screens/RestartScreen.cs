using Core.Controller.Level;
using DeveGames.PopupSystem.Scripts;
using DeveGames.PopupSystem.Scripts.AnimationStrategy;
using Zenject;

namespace Core.UI.Screens
{
    public class RestartScreen : Popup<RestartScreen>
    {
        private LevelController _levelController;
        
        [Inject]
        private void Construct(LevelController levelController)
        {
            _levelController = levelController;
        }

        private void Awake()
        {
            SetAnimationStrategy(new SubtleAnimationStrategy());
        }
        
        public void OnRestartButtonClicked()
        {
            _levelController.EndStage(false);
            
            Close();
            CloseListener(() =>
            {
                _levelController.LoadCurrentLevel(); 
            });            
        }
    }
}