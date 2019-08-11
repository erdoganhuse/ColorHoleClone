using System;
using DeveGames.Extensions;
using DeveGames.PopupSystem.Scripts.AnimationStrategy;
using DeveGames.PopupSystem.Scripts.Background;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DeveGames.PopupSystem.Scripts
{
    public class Popup<T> : Popup where T : Popup<T>
    {
        private Action _onClosed;
        private bool _shouldCloseImmediately;
        
        public override void Init()
        {
            SetAnimationStrategy(new DefaultAnimationStrategy());
        }

        public override void Clear()
        {
            Destroy(gameObject);
            if (Background != null)
            {
                Destroy(Background.gameObject);
                Background = null;
            }
        }

        public override void Configure(Action closeCallback = null) 
        {
            CloseCallback = closeCallback;
            CanvasGroup.interactable = true;

            if (BaseSettings.HasBackground && BaseSettings.BackgroundPrefab != null)
            {
                Background = Instantiate(BaseSettings.BackgroundPrefab, transform.parent);
                Background.gameObject.transform.SetAsLastSibling();
            }
        }
        
        public override void Close(bool shouldCloseImmediately = false)
        {
            _shouldCloseImmediately = shouldCloseImmediately;
            CloseCallback.SafeInvoke();
        }
        
        public override void Enable()
        {
            OpenBackground(() => 
            {
                gameObject.SetActive(true);
                CanvasGroup.interactable = false;
                AnimationStrategy.PerformOpenAnimation(this, () =>
                {
                    CanvasGroup.interactable = true;
                });            
            });
        }

        public override void Disable()
        {
            gameObject.SetActive(false);
            if(Background != null) 
                Background.gameObject.SetActive(false);
        }
        
        public override void AnimateIn(Action onOpenAnimationEnded = null)
        {               
            OpenBackground(() =>
            {
                transform.SetAsLastSibling();
                gameObject.SetActive(true);
                CanvasGroup.interactable = false;
                
                OnOpenBegan();
                AnimationStrategy.PerformOpenAnimation(this, () =>
                {
                    CanvasGroup.interactable = true;
                    OnOpened();
                    onOpenAnimationEnded.SafeInvoke();
                });
            });            
        }

        public override void AnimateOut(Action onCloseAnimationEnded = null)
        {
            OnCloseBegan();

            if (_shouldCloseImmediately)
            {
                _shouldCloseImmediately = false;

                if(Background != null) 
                    Background.Clear();
                
                gameObject.SetActive(false);
                onCloseAnimationEnded.SafeInvoke();                
                OnClosed();
            }
            else
            {  
                CanvasGroup.interactable = false;
                AnimationStrategy.PerformCloseAnimation(this, () =>
                {
                    gameObject.SetActive(false);
                    CanvasGroup.interactable = true;
                    CloseBackground(() => 
                    {
                        onCloseAnimationEnded.SafeInvoke();                    
                        OnClosed();
                    });
                });
            }
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            
            _onClosed.SafeInvoke();
            _onClosed = null;
        }

        public void CloseListener(Action onClosed = null)
        {
            _onClosed += onClosed;
        }
        
        private void OpenBackground(Action onOpened = null)
        {
            if(Background != null)
            {
                Background.gameObject.SetActive(true);
                Background.Open(
                () =>
                {
                    if(BaseSettings.BackgroundClosable) Close();    
                }, 
                onOpened.SafeInvoke);            
            }
            else
            {
                onOpened.SafeInvoke();
            }
        }

        private void CloseBackground(Action onClosed = null)
        {
            if(Background != null) 
                Background.Close(onClosed.SafeInvoke);
            else 
                onClosed.SafeInvoke();
        }
    }
    
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Popup : SerializedMonoBehaviour
    {
        [SerializeField] private Settings _baseSettings;
        public Settings BaseSettings => _baseSettings;

        protected Action CloseCallback;  
        protected BackgroundItem Background;
        protected IAnimationStrategy AnimationStrategy;
        
        private CanvasGroup _canvasGroup;
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null) _canvasGroup = GetComponentInChildren<CanvasGroup>(true);
                return _canvasGroup;
            }
        }

        public abstract void Init();
        public abstract void Clear();
        public abstract void Configure(Action closeCallback = null);
        public abstract void Close(bool closeImmediately = false);
        
        public abstract void Enable();
        public abstract void Disable();
        
        public abstract void AnimateIn(Action onOpenAnimationEnded = null);
        public abstract void AnimateOut(Action onCloseAnimationEnded = null);

        protected virtual void OnOpened(){}
        protected virtual void OnOpenBegan(){}
        protected virtual void OnClosed(){}
        protected virtual void OnCloseBegan(){}

        protected void SetAnimationStrategy(IAnimationStrategy animationStrategy)
        {
            AnimationStrategy = animationStrategy;
        }
    }
    
    public enum PriorityLevel { None, Low, Normal, High, Alert }

    [Serializable]
    public class Settings
    {
        public PriorityLevel PriorityLevel;
        public bool ClosePopupsUnderneath;
        public bool DisablePopupsUnderneath;

        //TODO: Add Functionality
        public bool DestroyWhenClosed;
        
        public bool HasBackground;
        [ShowIf("HasBackground")] 
        public BackgroundItem BackgroundPrefab;
        [ShowIf("HasBackground")] 
        public bool BackgroundClosable;        
    }
    
}
