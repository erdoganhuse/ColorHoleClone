using Core.Controller.Level;
using DeveGames.CoroutineSystem;
using DeveGames.PopupSystem.Scripts;
using DeveGames.PopupSystem.Scripts.AnimationStrategy;
using Utilities.Enums;
using Zenject;

namespace Core.UI.Screens
{
    public class WinScreen : Popup<WinScreen>
    {
        private LevelController _levelController;
        private PopupManager _gamePopupManager;
        
        [Inject]
        private void Construct(
            LevelController levelController,
            [Inject(Id = BindingIds.GamePopupManager)] PopupManager gamePopupManager)
        {
            _levelController = levelController;
            _gamePopupManager = gamePopupManager;

        }

        private void Awake()
        {
            SetAnimationStrategy(new SubtleAnimationStrategy());
        }

        protected override void OnOpened()
        {
            base.OnOpened();
            
            CoroutineManager.DoAfterGivenTime(3f, () =>
            {
                Close();
                CloseListener(() =>
                {
                    _levelController.LoadNextLevel();
                });
            });
        }
    }
}