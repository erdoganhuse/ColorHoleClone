using DeveGames.PopupSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace DeveGames.PopupSystem.Examples
{
    public class SamplePausePopup : Popup<SamplePausePopup>
    {
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _closeButton.onClick.AddListener(() => Close());
        }
        
        protected override void OnOpenBegan()
        {
            base.OnOpenBegan();
            
            Debug.Log("[SamplePausePopup] => OnOpenBegan");
        }

        protected override void OnOpened()
        {
            base.OnOpened();
            
            Debug.Log("[SamplePausePopup] => OnOpened");
        }

        protected override void OnCloseBegan()
        {
            base.OnCloseBegan();
            
            Debug.Log("[SamplePausePopup] => OnCloseBegan");
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            
            Debug.Log("[SamplePausePopup] => OnClosed");
        }
    }
}

