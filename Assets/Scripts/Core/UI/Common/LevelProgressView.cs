using Core.Signal.Hole;
using Core.Signal.Level;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Constants;
using Zenject;

namespace Core.UI.Common
{
    public class LevelProgressView : MonoBehaviour
    {
        [SerializeField] private Text _currentLevelIdText;
        [SerializeField] private Text _nextLevelIdText;

        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<StageLoadedSignal>(OnStageLoaded);
            _signalBus.Subscribe<EnteredToHoleSignal>(OnEnteredToHole);
        }

        #region Signal Listeners

        private void OnStageLoaded(StageLoadedSignal stageLoadedSignal)
        {
            _currentLevelIdText.text = (stageLoadedSignal.LevelIndex + 1).ToString();
            _nextLevelIdText.text = (stageLoadedSignal.LevelIndex + 2).ToString();
        }
        
        private void OnEnteredToHole(EnteredToHoleSignal enteredToHoleSignal)
        {
            if (enteredToHoleSignal.ObjectTag == Tags.Absorbable)
            {
                
            }
        }        

        #endregion
    }
}