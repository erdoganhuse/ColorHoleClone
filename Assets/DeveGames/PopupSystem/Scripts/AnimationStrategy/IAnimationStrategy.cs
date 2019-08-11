using System;

namespace DeveGames.PopupSystem.Scripts.AnimationStrategy
{
    public interface IAnimationStrategy
    {
        void PerformOpenAnimation(Popup popup, Action onOpenAnimationEnded = null);
        void PerformCloseAnimation(Popup popup, Action onCloseAnimationEnded = null);
    }
}

