using CriticalStrike.WheelOfFortuneMiniGame.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortuneMiniGame.UI
{
    public class RestartButton : MonoBehaviour
    {
        private Button _button;
    
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Restart);
        }

        private static void Restart()
        {
            GameManager.Instance.ResetGame();
        }
    }
}
