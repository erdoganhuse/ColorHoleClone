using System.Collections.Generic;
using DG.Tweening;
using Gamelogic.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace DeveGames.NoticeSystem
{
    public class DefaultNoticeItem : BaseNoticeItem
    {
        [SerializeField] private Text _messageText;
        [SerializeField] private Dictionary<NoticeType, Color> _noticeTypeColors;
        
        public override void AnimateIn()
        {
            gameObject.SetActive(true);

            _messageText.color = _messageText.color.WithAlpha(0f);
            _messageText.DOFade(1f, 0.375f);
            RectTransform.DOAnchorPosY(RectTransform.anchoredPosition.y + 50f, 0.75f)
                .OnComplete(() => OnAnimateInCompleted?.Invoke());
        }

        public override void AnimateOut()
        {
            _messageText.DOFade(0f, 0.375f)
                .OnComplete(() => OnAnimateOutCompleted?.Invoke());;
        }

        public override void Setup(string message, NoticeType noticeType)
        {
            _messageText.text = message;
            _messageText.color = _noticeTypeColors[noticeType];
        }

        public override void Clear()
        {
            RemoveAllListeners();
            gameObject.SetActive(false);
            DOTween.Complete(gameObject.transform, true);
        }
    }
}