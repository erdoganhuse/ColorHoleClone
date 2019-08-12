using System.Collections;
using Core.Controller.Level;
using Core.Signal.Level;
using DeveGames.CoroutineSystem;
using DeveGames.Extensions;
using DeveGames.PopupSystem.Scripts;
using DeveGames.PopupSystem.Scripts.AnimationStrategy;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Enums;
using Zenject;

namespace Core.UI.Screens
{
    public class ContinueScreen : Popup<ContinueScreen>
    {
        private const float CountdownDuration = 10f;
        private const float NoThanksButtonAppearTime = 3f;
        
        [SerializeField] private Text _timerText;
        [SerializeField] private Image _timerSlider;
        [SerializeField] private Text _stageText;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _noThanksButton;

        private SignalBus _signalBus;
        private LevelController _levelController;
        private PopupManager _gamePopupManager;

        private Coroutine _timerCoroutine;
        private Sequence _timerTweenSequence;
        
        [Inject]
        private void  Construct(
            SignalBus signalBus,
            LevelController levelController,
            [Inject(Id = BindingIds.GamePopupManager)] PopupManager gamePopupManager)
        {
            _signalBus = signalBus;
            _levelController = levelController;
            _gamePopupManager = gamePopupManager;           
        }
        
        private void Awake()
        {
            SetAnimationStrategy(new SubtleAnimationStrategy());
        }
        
        protected override void OnOpenBegan()
        {
            base.OnOpenBegan();

            _timerText.text = CountdownDuration.ToString("f0");
            _timerCoroutine = CoroutineManager.StartChildCoroutine(TimerImplementation());
        }

        protected override void OnClosed()
        {
            base.OnClosed();

            CoroutineManager.StopChildCoroutine(_timerCoroutine);
            _timerTweenSequence.SafeComplete(false);
        }

        private IEnumerator TimerImplementation()
        {
            _timerSlider.fillAmount = 1f;
            _noThanksButton.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(1f);

            _timerTweenSequence = DOTween.Sequence();
            _timerTweenSequence.Insert(0f, _timerSlider.DOFillAmount(0f, CountdownDuration).SetEase(Ease.Linear));
            _timerTweenSequence.Insert(0f, _timerText.DONumberChange((int)CountdownDuration, 0, CountdownDuration).SetEase(Ease.Linear));
            
            yield return new WaitForSeconds(NoThanksButtonAppearTime);

            _noThanksButton.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(CountdownDuration - NoThanksButtonAppearTime);
                        
            Close();
            CloseListener(() =>
            {
                _gamePopupManager.Open<RestartScreen>();
            });
        }
        
        public void OnContinueButtonClicked()
        {
            Close();
            CloseListener(() =>
            {
                _signalBus.TryFire(new StageContinuedSignal());
            });
        }

        public void OnNoThanksButtonClicked()
        {
            _levelController.EndStage(false);
            
            Close();
            CloseListener(() =>
            {
                _levelController.LoadCurrentLevel();
            });
        }
    }
}