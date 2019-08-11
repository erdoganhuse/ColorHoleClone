using Core.Data.Level;
using Core.Signal.Level;
using UnityEngine;
using Zenject;

namespace Core.Controller.Level
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _levelContainerTransform;

        private SignalBus _signalBus;
        private LevelContainer _levelContainer;

        private GameObject _currentStageObject;
        private int _currentLevelId;
        
        [Inject]
        private void Construct(
            SignalBus signalBus,
            LevelContainer levelContainer)
        {
            _signalBus = signalBus;
            _levelContainer = levelContainer;
            
            _signalBus.Subscribe<LevelStartedSignal>(OnLevelStarted);
            _signalBus.Subscribe<LevelEndedSignal>(OnLevelEnded);
            
            _signalBus.Subscribe<StageLoadedSignal>(OnStageLoaded);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<LevelStartedSignal>(OnLevelStarted);
            _signalBus.Unsubscribe<LevelEndedSignal>(OnLevelEnded);
            
            _signalBus.Unsubscribe<StageLoadedSignal>(OnStageLoaded);
        }

        #region Signal Listener

        private void OnStageLoaded(StageLoadedSignal stageLoadedSignal)
        {
            _currentStageObject =
                Instantiate(
                    _levelContainer.Levels[stageLoadedSignal.LevelIndex].Stages[stageLoadedSignal.StageIndex].Prefab,
                    _levelContainerTransform);
        }

        private void OnLevelStarted() { }
        
        private void OnLevelEnded() { }
        
        #endregion
    }
}