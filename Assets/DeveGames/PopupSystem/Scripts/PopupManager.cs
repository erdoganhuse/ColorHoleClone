using System;
using System.Collections.Generic;
using System.Linq;
using DeveGames.Extensions;
using ModestTree;
using Sirenix.Utilities;
using UnityEngine;

namespace DeveGames.PopupSystem.Scripts
{
    [DisallowMultipleComponent]
    public class PopupManager : MonoBehaviour
    {
        public event Action<Type> OnOpening;
        public event Action<Type> OnOpened;
        public event Action<Type> OnClosing;
        public event Action<Type> OnClosed;

        public Popup CurrentPopup { get; private set; }
        public Popup PreviousPopup { get; private set; }
        
        [SerializeField] private RectTransform _container;
        [SerializeField] private Popup[] _popups;
        
        private readonly Stack<Popup> _activePopupStack = new Stack<Popup>();

        public T Get<T>() where T : Popup
        {
            return _activePopupStack.Get<T>();
        }
        
        public void Open<T>() where T : Popup
        {
            if (CurrentPopup != null && CurrentPopup.GetType() == typeof(T)) return;
            if (_activePopupStack.Any<T>()) return;
            
            _popups.Get<T>().gameObject.SetActive(false);
            Popup popup = Instantiate(_popups.Get<T>(), _container);
            popup.Init();
            
            if(CurrentPopup != null && CurrentPopup.BaseSettings.PriorityLevel > _popups.Get<T>().BaseSettings.PriorityLevel)
            {
                _activePopupStack.Push(popup);
                return;
            }
            
            Register(popup);
            Open(popup);
        }
        
        public void Close<T>() where T : Popup
        {
            if (_activePopupStack.IsEmpty() || _activePopupStack.Peek().GetType() != typeof(T)) return;
            
            Popup popup = _activePopupStack.Get<T>();
            OnClosing.SafeInvoke(popup.GetType());
            popup.AnimateOut(() =>
            {
                _activePopupStack.Pop();
                
                PreviousPopup = CurrentPopup;
                CurrentPopup = null;
                    
                OnClosed.SafeInvoke(popup.GetType());
                popup.Clear();

                if (_activePopupStack.Count > 0) OpenFromStack();
            });
        }

        private void Register(Popup popup)
        {
            if (_activePopupStack.Count > 0)
            {
                if (popup.BaseSettings.ClosePopupsUnderneath)
                {
                    for (int i = 0; i < _activePopupStack.Count; i++)
                        _activePopupStack.ElementAt(i).Close(true);
                }
                else if(popup.BaseSettings.DisablePopupsUnderneath)
                    _activePopupStack.ForEach(item => item.Disable());
            }
            
            CurrentPopup = popup;
            _activePopupStack.Push(popup);
        }
        
        private void Open(Popup popup)
        {
            popup.Configure(() => 
            {
                typeof(PopupManager)
                    .GetMethod("Close")
                    .MakeGenericMethod(popup.GetType())
                    .Invoke(this, null);
            });
                       
            OnOpening.SafeInvoke(popup.GetType());
            popup.AnimateIn(() =>
            {
                OnOpened.SafeInvoke(popup.GetType());
            });
        }
        
        private void OpenFromStack()
        {
            Popup popup = _activePopupStack.Pop();
            
            Register(popup);
            if(!popup.gameObject.activeSelf) popup.Enable();
        }        
    }
}
