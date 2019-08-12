using Core.Signal;
using Core.Signal.Level;
using Core.UI.Common;
using Core.UI.Screens;
using DeveGames.PopupSystem.Scripts;
using Utilities.Enums;
using Zenject;

namespace Core.Controller.UI
{
    public class UiController
    {
        private readonly SignalBus _signalBus;
        private readonly PopupManager _gamePopupManager;
        private readonly LevelProgressView _levelProgressView;
        
        public UiController(
            SignalBus signalBus,
            [Inject(Id = BindingIds.GamePopupManager)] PopupManager gamePopupManager,
            LevelProgressView levelProgressView)
        {
            _signalBus = signalBus;
            _gamePopupManager = gamePopupManager;
            _levelProgressView = levelProgressView;
            
            _signalBus.Subscribe<LevelLoadedSignal>(OnLevelLoaded);
            _signalBus.Subscribe<LevelEndedSignal>(OnLevelEnded);
            _signalBus.Subscribe<StageFailedSignal>(OnStageFailed);            
        }

        ~UiController()
        {
            _signalBus.Unsubscribe<LevelLoadedSignal>(OnLevelLoaded);
            _signalBus.Unsubscribe<LevelEndedSignal>(OnLevelEnded);
            _signalBus.Unsubscribe<StageFailedSignal>(OnStageFailed);            
        }

        #region Signal Listeners

        private void OnLevelLoaded()
        {            
            _levelProgressView.Open();
            _gamePopupManager.Open<StartScreen>();
        }
        
        private void OnLevelEnded(LevelEndedSignal levelEndedSignal)
        {
            if (levelEndedSignal.IsSuccessful)
            {
                _gamePopupManager.Open<WinScreen>();
            }
        }
        
        private void OnStageFailed()
        {
            _gamePopupManager.Open<ContinueScreen>();
        }
        
        #endregion
    }
}