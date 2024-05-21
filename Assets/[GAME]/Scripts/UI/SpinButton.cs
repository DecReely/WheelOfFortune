using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortuneMiniGame.UI
{
    public class SpinButton : MonoBehaviour
    {
        private Button _button;
    
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Spin);
        }

        private static void Spin()
        {
            Wheel.WheelOfFortune.Instance.Rotate();
        }
    }
}
