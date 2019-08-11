using DeveGames.Extensions;
using UnityEngine;

namespace DeveGames.NoticeSystem
{
    public class NoticeManager : MonoBehaviour
    {
        [SerializeField] private Vector2 _anchoredPosition;
        [SerializeField] private TextAnchor _alignment;
        [SerializeField] private BaseNoticeItem _noticeItemPrefab;
        [SerializeField] private Transform _container;        
        
        private bool _isDuringShow;
        private BaseNoticeItem _currentNoticeItem;
            
        private void Awake()
        {
            Setup();
        }
        
        public void Setup()
        {
            _currentNoticeItem = Instantiate(_noticeItemPrefab, _container);
            _currentNoticeItem.gameObject.SetActive(false);
        }
        
        public void Show(string message, NoticeType noticeType = NoticeType.None)
        {
           if(_isDuringShow) _currentNoticeItem.Clear();

           _isDuringShow = true;

           _currentNoticeItem.RectTransform.SetAnchor(_alignment);
           _currentNoticeItem.RectTransform.SetPivot(_alignment);
           _currentNoticeItem.RectTransform.anchoredPosition = _anchoredPosition;
           
           _currentNoticeItem.Setup(message, noticeType);
           _currentNoticeItem.AnimateIn();
           _currentNoticeItem.AnimateInListener(() =>
           {
               _currentNoticeItem.AnimateOut();
               _currentNoticeItem.AnimateOutListener(() =>
               {
                   _currentNoticeItem.Clear();
                   _isDuringShow = false;
               });
           });
        }
    }
}