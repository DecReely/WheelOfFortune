using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CriticalStrike.WheelOfFortune.UI
{
    public class WinLoseUIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        [Header("Settings")] 
        [SerializeField] private float animationDuration = 0.5f;
    
        public void HandleWinScreenUI()
        {
            ShowWinPanel();
        }
    
        public void HandleLoseScreenUI()
        {
            ShowLosePanel();
        }

        private void ShowWinPanel()
        {
            winPanel.SetActive(true);
            winPanel.transform.localScale = Vector3.zero;
            winPanel.transform.DOScale(Vector3.one, animationDuration);
        }

        private void HideWinPanel()
        {
            winPanel.SetActive(false);
        }
    
        private void ShowLosePanel()
        {
            losePanel.SetActive(true);
            losePanel.transform.localScale = Vector3.zero;
            losePanel.transform.DOScale(Vector3.one, animationDuration);
        }

        private void HideLosePanel()
        {
            losePanel.SetActive(false);
        }

        public void ResetWinLoseUI()
        {
            HideWinPanel();
            HideLosePanel();
        }
    }
}