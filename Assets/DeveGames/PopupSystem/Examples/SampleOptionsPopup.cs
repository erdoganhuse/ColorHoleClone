using DeveGames.PopupSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace DeveGames.PopupSystem.Examples
{
    public class SampleOptionsPopup : Popup<SampleOptionsPopup>
    {
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _closeButton.onClick.AddListener(() => Close());
        }

        protected override void OnOpenBegan()
        {
            base.OnOpenBegan();
            
            Debug.Log("[SampleOptionsPopup] => OnOpenBegan");
        }

        protected override void OnOpened()
        {
            base.OnOpened();
            
            Debug.Log("[SampleOptionsPopup] => OnOpened");
        }

        protected override void OnCloseBegan()
        {
            base.OnCloseBegan();
            
            Debug.Log("[SampleOptionsPopup] => OnCloseBegan");
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            
            Debug.Log("[SampleOptionsPopup] => OnClosed");
        }
    }
}

