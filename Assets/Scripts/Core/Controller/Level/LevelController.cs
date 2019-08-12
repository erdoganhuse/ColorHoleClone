using Core.Controller.User;
using Core.Data.Level;
using Core.Signal;
using Core.Signal.Level;
using DeveGames.Extensions;
using UnityEngine;
using Utilities.Constants;
using Zenject;

namespace Core.Controller.Level
{
    public class LevelController
    {
        public int CurrentLevelId
        {
            get
            {
                return _userDataController.Progress.MaxAchievedLevelId;
            }
            private set
            {
                _userDataController.Progress.MaxAchievedLevelId = value;
                _userDataController.Save();
            }
        }

        public int CurrentLevelIndex
        {
            get { return _levelContainer.Levels.FindLastIndex(item => item.Id == CurrentLevelId); }
        }

        public int CurrentStageIndex { get; private set; }
        
        public bool IsDuringLevel { get; private set; }
        
        private readonly SignalBus _signalBus;
        private readonly UserDataController _userDataController;
        private readonly LevelContainer _levelContainer;

        private LevelData _currentLevelData;
        
        public LevelController(
            SignalBus signalBus,
            UserDataController userDataController,
            LevelContainer levelContainer)
        {
            _signalBus = signalBus;
            _userDataController = userDataController;
            _levelContainer = levelContainer;
            
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }

        ~LevelController() { }
        
        public void LoadLevel(int levelId)
        {            
            CurrentLevelId = levelId;

            _currentLevelData = _levelContainer.Levels[CurrentLevelIndex];

            _signalBus.TryFire(new LevelLoadedSignal(CurrentLevelId, _currentLevelData.Stages.Count));
            
            LoadStage(0);
        }

        public void LoadCurrentLevel()
        {
            LoadLevel(CurrentLevelId);
        }

        public void LoadNextLevel()
        {
            LoadLevel(_levelContainer.GetNextLevelId(CurrentLevelIndex));
        }
        
        public void StartLevel()
        {
            IsDuringLevel = true;
            
            _signalBus.TryFire(new LevelStartedSignal(CurrentLevelId));
            
            StartStage();
        }

        public void EndLevel(bool isSuccessful)
        {
            IsDuringLevel = false;
            
            _signalBus.TryFire(new LevelEndedSignal(CurrentLevelId, isSuccessful));
        }

        public void LoadStage(int stageIndex)
        {
            CurrentStageIndex = stageIndex;
            
            StageData stage = _currentLevelData.Stages[CurrentStageIndex];
            int absorbedObjectCount = stage.Prefab.transform.GetComponentsInChildrenWithTag<Transform>(Tags.Absorbable).Length;
            
            _signalBus.TryFire(new StageLoadedSignal(CurrentLevelIndex, CurrentStageIndex, absorbedObjectCount));
        }

        public void StartStage()
        {
            _signalBus.TryFire(new StageStartedSignal(CurrentStageIndex));    
        }
        
        public void EndStage(bool isSuccessful)
        {
            _signalBus.TryFire(new StageEndedSignal(CurrentStageIndex, isSuccessful));

            if (isSuccessful)
            {
                if (CurrentStageIndex + 1 >= _levelContainer.Levels[CurrentLevelIndex].Stages.Count)
                {
                    EndLevel(true);
                }
                else
                {
                    LoadStage(CurrentStageIndex + 1);
                    StartStage();
                }
            }
            else
            {
                EndLevel(false);
            }
        }
                
        #region Signal Listeners

        private void OnGameStarted()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
         
            LoadCurrentLevel();
        }

        #endregion
    }
}