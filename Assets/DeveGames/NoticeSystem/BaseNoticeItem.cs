using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DeveGames.NoticeSystem
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class BaseNoticeItem : SerializedMonoBehaviour
    {        
        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        protected Action OnAnimateInCompleted;
        protected Action OnAnimateOutCompleted;

        public abstract void Setup(string message, NoticeType noticeType);
        public abstract void Clear();
        public abstract void AnimateIn();
        public abstract void AnimateOut();

        public void AnimateInListener(Action onComplete = null)
        {
            OnAnimateInCompleted += onComplete;
        }

        public void AnimateOutListener(Action onComplete = null)
        {
            OnAnimateOutCompleted += onComplete;
        }

        public void RemoveAllListeners()
        {
            OnAnimateInCompleted = null;
            OnAnimateOutCompleted = null;
        }        
    }
}