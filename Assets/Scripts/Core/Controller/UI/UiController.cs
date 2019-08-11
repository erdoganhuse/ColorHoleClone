using Core.Signal;
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
        
        public UiController(
            SignalBus signalBus,
            [Inject(Id = BindingIds.GamePopupManager)] PopupManager gamePopupManager)
        {
            _signalBus = signalBus;
            _gamePopupManager = gamePopupManager;
            
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        #region Signal Listeners

        private void OnGameStarted()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
            
            _gamePopupManager.Open<StartScreen>();
        }

        #endregion
    }
}