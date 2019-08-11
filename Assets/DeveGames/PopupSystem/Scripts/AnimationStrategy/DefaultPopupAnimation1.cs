using System;
using DeveGames.Extensions;
using DG.Tweening;
using UnityEngine;

namespace DeveGames.PopupSystem.Scripts.AnimationStrategy
{
    public class SubtleAnimationStrategy : IAnimationStrategy
    {
        private readonly float _openAnimDuration;
        private readonly float _closeAnimDuration;
        
        public SubtleAnimationStrategy(float openAnimDuration = 0.3f, float closeAnimDuration = 0.15f)
        {
            _openAnimDuration = openAnimDuration;
            _closeAnimDuration = closeAnimDuration;
        }
        
        public void PerformOpenAnimation(Popup popup, Action onOpenAnimationEnded = null)
        {            
            popup.CanvasGroup.alpha = 0f;
            popup.CanvasGroup.DOFade(1f, _openAnimDuration);

            popup.transform.localScale = Vector3.one * 1.25f;
            popup.transform.DOScale(1f, _openAnimDuration).OnComplete(onOpenAnimationEnded.SafeInvoke);
        }
    
        public void PerformCloseAnimation(Popup popup, Action onCloseAnimationEnded = null)
        {
            popup.CanvasGroup.DOFade(0f, _closeAnimDuration); 
            popup.transform.DOScale(1.25f, _closeAnimDuration).OnComplete(onCloseAnimationEnded.SafeInvoke);
        }
    }
}

