using System;

namespace CriticalStrike.WheelOfFortuneMiniGame.Events
{
    public static class EventManager
    {
        public static event Action SpinEndedEvent;

        public static void CallSpinEndedEvent()
        {
            SpinEndedEvent?.Invoke();
        }
        
        public static event Action UpdateUIEvent;

        public static void CallUpdateUIEvent()
        {
            UpdateUIEvent?.Invoke();
        }
    }
}
