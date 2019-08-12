using Core.Data.Level;
using Core.Signal.Hole;
using Core.Signal.Level;
using DeveGames.Extensions;
using UnityEngine;
using Utilities.Constants;
using Zenject;

namespace Core.Controller.Level
{
    public class LevelFinishProcessor
    {
        private readonly SignalBus _signalBus;
        private readonly LevelContainer _levelContainer;
        private readonly LevelController _levelController;

        private int _initialObjectCount;
        private int _absorbedObjectCount;
        
        public LevelFinishProcessor(
            SignalBus signalBus,
            LevelContainer levelContainer,
            LevelController levelController)
        {
            _signalBus = signalBus;
            _levelContainer = levelContainer;
            _levelController = levelController;
            
            _signalBus.Subscribe<StageLoadedSignal>(OnStageLoaded);
            _signalBus.Subscribe<EnteredToHoleSignal>(OnEnteredToHole);
        }

        ~LevelFinishProcessor()
        {
            _signalBus.Unsubscribe<StageLoadedSignal>(OnStageLoaded);
            _signalBus.Unsubscribe<EnteredToHoleSignal>(OnEnteredToHole);            
        }
        
        #region Signal Listeners

        private void OnStageLoaded(StageLoadedSignal stageLoadedSignal)
        {
            StageData stage = _levelContainer.Levels[_levelController.CurrentLevelIndex].Stages[_levelController.CurrentStageIndex];

            _absorbedObjectCount = 0;
            _initialObjectCount = stage.Prefab.transform.GetComponentsInChildrenWithTag<Transform>(Tags.Absorbable).Length;
        }
        
        private void OnEnteredToHole(EnteredToHoleSignal enteredToHoleSignal)
        {
            if (enteredToHoleSignal.ObjectTag == Tags.Absorbable)
            {
                _absorbedObjectCount++;
                if (_absorbedObjectCount == _initialObjectCount)
                {
                    _levelController.EndStage(true);
                }
            }
            else if (enteredToHoleSignal.ObjectTag == Tags.NonAbsorbable)
            {
                _signalBus.TryFire(new StageFailedSignal());
            }
        }

        #endregion        
    }
}