using Core.Data.Level;
using Core.Signal.Level;
using UnityEngine;
using Zenject;

namespace Core.Controller.Level
{
    public class LevelSpawner : MonoBehaviour
    {
        private SignalBus _signalBus;
        private LevelContainer _levelContainer;

        private GameObject _currentStageObject;
        
        [Inject]
        private void Construct(
            SignalBus signalBus,
            LevelContainer levelContainer)
        {
            _signalBus = signalBus;
            _levelContainer = levelContainer;
            
            _signalBus.Subscribe<StageLoadedSignal>(OnStageLoaded);
            _signalBus.Subscribe<StageEndedSignal>(OnStageEnded);
        }

        private void OnDestroy()
        {            
            _signalBus.Unsubscribe<StageLoadedSignal>(OnStageLoaded);
            _signalBus.Subscribe<StageEndedSignal>(OnStageEnded);
        }

        #region Signal Listener

        private void OnStageLoaded(StageLoadedSignal stageLoadedSignal)
        {
            _currentStageObject =
                Instantiate(
                    _levelContainer.Levels[stageLoadedSignal.LevelIndex].Stages[stageLoadedSignal.StageIndex].Prefab,
                    transform);
        }

        private void OnStageEnded(StageEndedSignal stageEndedSignal)
        {
            Destroy(_currentStageObject);
        }
        
        #endregion
    }
}