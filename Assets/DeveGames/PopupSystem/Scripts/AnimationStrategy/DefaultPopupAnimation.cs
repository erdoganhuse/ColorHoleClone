using System;
using DeveGames.Extensions;
using DG.Tweening;
using UnityEngine;

namespace DeveGames.PopupSystem.Scripts.AnimationStrategy
{
    public class DefaultAnimationStrategy : IAnimationStrategy
    {
        private readonly float _openAnimDuration;
        private readonly float _closeAnimDuration;
        
        public DefaultAnimationStrategy(float openAnimDuration = 0.25f, float closeAnimDuration = 0.25f)
        {
            _openAnimDuration = openAnimDuration;
            _closeAnimDuration = closeAnimDuration;
        }
        
        public void PerformOpenAnimation(Popup popup, Action onOpenAnimationEnded = null)
        {
            popup.transform.localScale = Vector3.zero;
            popup.transform.DOScale(1f, _openAnimDuration).OnComplete(onOpenAnimationEnded.SafeInvoke);
        }
    
        public void PerformCloseAnimation(Popup popup, Action onCloseAnimationEnded = null)
        {
            popup.transform.DOScale(0f, _closeAnimDuration).OnComplete(onCloseAnimationEnded.SafeInvoke);
        }
    }
}

