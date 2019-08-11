using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeveGames.PopupSystem.Scripts.Background
{   
    public abstract class BackgroundItem : MonoBehaviour, IPointerClickHandler
    {
        protected Action CloseCallback;

        public abstract void Setup();
        public abstract void Clear();
        
        public abstract void Open(Action closeCallback, Action onOpened = null);
        public abstract void Close(Action onClosed = null);

        public abstract void OnPointerClick(PointerEventData eventData);
    }
}


