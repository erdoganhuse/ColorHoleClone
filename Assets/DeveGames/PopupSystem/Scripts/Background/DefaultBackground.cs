using System;
using DeveGames.Extensions;
using DG.Tweening;
using Gamelogic.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DeveGames.PopupSystem.Scripts.Background
{
    public class DefaultBackground : BackgroundItem
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private float _destinationAlphaValue = 0.75f;
        [SerializeField] private float _openAnimDuration = 0.25f;
        [SerializeField] private float _closeAnimDuration = 0.25f;
   
        public override void Setup()
        {
            _backgroundImage.color = _backgroundImage.color.WithAlpha(0f);
        }

        public override void Clear()
        {
            CloseCallback = null;
            _backgroundImage.color = _backgroundImage.color.WithAlpha(0f);
        }

        public override void Open(Action closeCallback, Action onOpened = null)
        {
            CloseCallback = closeCallback;
            
            Setup();
            _backgroundImage.DOFade(_destinationAlphaValue, _openAnimDuration).OnComplete(onOpened.SafeInvoke);
        }

        public override void Close(Action onClosed = null)
        {
            _backgroundImage.DOFade(0f, _closeAnimDuration).OnComplete(() =>
            {
                Clear();
                onClosed.SafeInvoke();
            });
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            CloseCallback.SafeInvoke();            
        }
    }
}
