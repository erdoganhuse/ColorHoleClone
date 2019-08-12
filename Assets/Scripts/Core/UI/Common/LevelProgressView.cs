using System.Collections.Generic;
using System.Linq;
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

        [SerializeField] private float _sliderAreaWidth;
        [SerializeField] private float _gapBetweenSliders;
        [SerializeField] private Slider _progressSliderPrefab;
        
        private SignalBus _signalBus;

        private int _currentStageIndex;
        private int _stageAbsorbableObjectCount;
        private int _currentAbsorbedObjectCount;
        
        private List<Slider> _activeProgressSliders = new List<Slider>();
        private List<Slider> _spawnedProgressSliders = new List<Slider>();

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<LevelLoadedSignal>(OnLevelLoaded);
            _signalBus.Subscribe<StageLoadedSignal>(OnStageLoaded);
            _signalBus.Subscribe<EnteredToHoleSignal>(OnEnteredToHole);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private float GetSliderWidth(float areaWidth, float gap, int count)
        {
            return (areaWidth - (count - 1) * gap) / count;
        }

        private Vector2 GetSliderPosition(float areaWidth, float gap, float sliderWidth, int index)
        {
            float leftPosX = -1f * areaWidth / 2f;
            return new Vector2(leftPosX + ((index + 0.5f) * sliderWidth) + (index * gap), 0f);
        }
        
        #region Signal Listeners

        private void OnLevelLoaded(LevelLoadedSignal levelLoadedSignal)
        {
            if (_spawnedProgressSliders.Count < levelLoadedSignal.StageCount)
            {
                for (int i = _spawnedProgressSliders.Count; i < levelLoadedSignal.StageCount; i++)
                {
                    _spawnedProgressSliders.Add(Instantiate(_progressSliderPrefab, transform));
                }                
            }

            float sliderWidth = GetSliderWidth(_sliderAreaWidth, _gapBetweenSliders, levelLoadedSignal.StageCount);
            Vector2 size = _spawnedProgressSliders.First().GetComponent<RectTransform>().sizeDelta;
            
            _activeProgressSliders.Clear();
            for (int i = 0; i < levelLoadedSignal.StageCount; i++)
            {
                _activeProgressSliders.Add(_spawnedProgressSliders[i]);

                _activeProgressSliders[i].value = 0f;
                RectTransform rect = _activeProgressSliders[i].GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(sliderWidth, size.y);
                rect.anchoredPosition = GetSliderPosition(_sliderAreaWidth, _gapBetweenSliders, sliderWidth, i);
            }
        }
        
        private void OnStageLoaded(StageLoadedSignal stageLoadedSignal)
        {
            _currentStageIndex = stageLoadedSignal.StageIndex;
            _stageAbsorbableObjectCount = stageLoadedSignal.AbsorbableObjectCount;
            _currentAbsorbedObjectCount = 0;
            
            _currentLevelIdText.text = (stageLoadedSignal.LevelIndex + 1).ToString();
            _nextLevelIdText.text = (stageLoadedSignal.LevelIndex + 2).ToString();
        }
        
        private void OnEnteredToHole(EnteredToHoleSignal enteredToHoleSignal)
        {
            if (enteredToHoleSignal.ObjectTag == Tags.Absorbable)
            {
                _currentAbsorbedObjectCount++;
                
                _activeProgressSliders[_currentStageIndex].value = (float)_currentAbsorbedObjectCount / _stageAbsorbableObjectCount;
            }
        }        

        #endregion
    }
}