using UnityEngine;

namespace CriticalStrike.WheelOfFortuneMiniGame.Misc
{
    [HelpURL("https://www.udemy.com/share/103rnM3@ufU8Eha82SG3bRWK68B59oDh99kHcVFXVGjCToYVR6oSGhLRQnmsLZUk_dOJxZ2p/")] // A script I learned from an udemy course.
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T:MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance; }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
