using System;
using UnityEngine;

namespace DeveGames.PopupSystem.Examples
{
    public class SampleUiController : MonoBehaviour
    {
        [SerializeField] private Scripts.PopupManager _popupManager;

        private void Awake()
        {
            _popupManager.OnOpened += PopupManager_OnOpened;
            _popupManager.OnClosed += PopupManager_OnClosed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _popupManager.Open<SampleOptionsPopup>();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _popupManager.Close<SampleOptionsPopup>();                
            }   
            if(Input.GetKeyDown(KeyCode.E))
            {
                _popupManager.Open<SamplePausePopup>();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                _popupManager.Close<SamplePausePopup>();
            }            
            if(Input.GetKeyDown(KeyCode.T))
            {
                _popupManager.Open<SampleWinPopup>();
            }
            if(Input.GetKeyDown(KeyCode.Y))
            {
                _popupManager.Close<SampleWinPopup>();
            }            
        }
        
        private void PopupManager_OnOpened(Type type)
        {
            Debug.Log("[SampleUiController] => PopupManager_OnOpened: " + type);
        }
                
        private void PopupManager_OnClosed(Type type)
        {
            Debug.Log("[SampleUiController] => PopupManager_OnClosed: " + type);
        }
    }
}
