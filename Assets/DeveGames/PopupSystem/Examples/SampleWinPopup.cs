using DeveGames.PopupSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace DeveGames.PopupSystem.Examples
{
    public class SampleWinPopup : Popup<SampleWinPopup>
    {
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _closeButton.onClick.AddListener(() => Close());
        }
        
        protected override void OnOpenBegan()
        {
            base.OnOpenBegan();
            
            Debug.Log("[SampleWinPopup] => OnOpenBegan");
        }

        protected override void OnOpened()
        {
            base.OnOpened();
            
            Debug.Log("[SampleWinPopup] => OnOpened");
        }

        protected override void OnCloseBegan()
        {
            base.OnCloseBegan();
            
            Debug.Log("[SampleWinPopup] => OnCloseBegan");
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            
            Debug.Log("[SampleWinPopup] => OnClosed");
        }
    }
}

