using Core.Signal.Level;
using UnityEngine;
using Zenject;

namespace Core.Logic.Hole.Input
{
    public class SwipeInput : MonoBehaviour, IInputHandler
    {    
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        private SignalBus _signalBus;
        
        private bool _isEnabled;
        private Vector2 _previousMousePosition;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<StageStartedSignal>(OnStageStarted);
            _signalBus.Subscribe<StageEndedSignal>(OnStageEnded);
            _signalBus.Subscribe<StageFailedSignal>(OnStageFailed);
            _signalBus.Subscribe<StageContinuedSignal>(OnStageContinued);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<StageStartedSignal>(OnStageStarted);
            _signalBus.Unsubscribe<StageEndedSignal>(OnStageEnded);
            _signalBus.Unsubscribe<StageFailedSignal>(OnStageFailed);
            _signalBus.Unsubscribe<StageContinuedSignal>(OnStageContinued);
        }
        
        public void Update()
        {
            if (!_isEnabled)
            {
                Horizontal = 0f;
                Vertical = 0f;

                return;
            }
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _previousMousePosition = UnityEngine.Input.mousePosition;
            }
            if (UnityEngine.Input.GetMouseButton(0))
            {
                Horizontal = UnityEngine.Input.mousePosition.x - _previousMousePosition.x;
                Vertical = UnityEngine.Input.mousePosition.y - _previousMousePosition.y;

                _previousMousePosition = UnityEngine.Input.mousePosition;
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                Horizontal = 0f;
                Vertical = 0f;
            }
        }
        
        #region Signal Listeners

        private void OnStageStarted()
        {
            _isEnabled = true;
        }

        private void OnStageEnded()
        {
            _isEnabled = false;
        }

        private void OnStageFailed()
        {
            _isEnabled = false;
        }

        private void OnStageContinued()
        {
            _isEnabled = true;
        }
        
        #endregion
    }
}