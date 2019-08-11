using Core.Controller.Level;
using Core.Controller.UI;
using Core.Data.Level;
using Core.Signal.Hole;
using Core.Signal.Level;
using Core.View;
using DeveGames.PopupSystem.Scripts;
using UnityEngine;
using Utilities.Enums;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private LevelContainer _levelContainer;
        [SerializeField] private PopupManager _gamePopupManager;
        [SerializeField] private PlatformView _platformView;
        
        public override void InstallBindings()
        {
            InstallSignals();

            Container.Bind<UiController>().AsSingle().NonLazy();
            Container.Bind<LevelController>().AsSingle().NonLazy();
            Container.Bind<LevelFinishProcessor>().AsSingle().NonLazy();

            Container.BindInstance(_levelContainer);
            Container.BindInstance(_gamePopupManager).WithId(BindingIds.GamePopupManager).AsSingle().NonLazy();
            Container.BindInstance(_platformView).AsSingle().NonLazy();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<LevelLoadedSignal>();
            Container.DeclareSignal<LevelStartedSignal>();
            Container.DeclareSignal<LevelEndedSignal>();
            
            Container.DeclareSignal<StageLoadedSignal>();
            Container.DeclareSignal<StageStartedSignal>();
            Container.DeclareSignal<StageEndedSignal>();

            Container.DeclareSignal<EnteredToHoleSignal>();
        }
    }
}