using DG.Tweening;
using UnityEngine;

namespace CriticalStrike.WheelOfFortune.Wheel
{
    [HelpURL("https://www.youtube.com/watch?v=eti87kSD_9U")] // Refactored the code in the video.
    [RequireComponent(typeof(Rigidbody2D))]
    public class WheelOfFortune : MonoBehaviour
    {
        private const int WheelSlotCount = 8; // Made it a constant since the visual asset has 8 slots.

        [Header("Settings")]
        [Range(200, 1440)] [SerializeField] private float rotatePower = 1200; // Not recommended to make it more than 1440, rotates too much.
        [Range(0, 50)] [SerializeField] private float rotatePowerRandomnessPercentage = 30;
        [Range(100, 600)] [SerializeField] private float stopPower = 200;
        [Range(0.1f, 1f)] [SerializeField] private float stopWaitTime = 0.3f;

        #region Variables

        private bool _isRotating;
        private float _stopTimer;

        #endregion

        #region Dependencies

        private Rigidbody2D _rigidbody2D;

        #endregion
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    
        private void Update()
        {
            HandleRotating();
        }

        private void HandleRotating()
        {
            if (!_isRotating)
                return;
            
            if (_rigidbody2D.angularVelocity > 0)
            {
                _rigidbody2D.angularVelocity -= stopPower * Time.deltaTime;
            }
            else 
            {
                _stopTimer += Time.deltaTime;
                
                if(_stopTimer >= stopWaitTime)
                {
                    HandleLanding();
    
                    StopRotating();
                }
            }
        }
        
        public void Rotate()
        {
            if (_isRotating)
            {
                Debug.LogError("Wheel is already spinning!");
                return;
            }
    
            StartRotating();
        }
    
        private void StartRotating()
        {
            float currentRotatePower = rotatePower *
                Random.Range(100 - rotatePowerRandomnessPercentage, 100 + rotatePowerRandomnessPercentage) / 100;
            
            _rigidbody2D.AddTorque(currentRotatePower);
            
            _isRotating = true;
        }
    
        private void StopRotating()
        {
            _rigidbody2D.angularVelocity = 0;
            _isRotating = false;
            _stopTimer = 0;
        }
        
        public void HandleLanding()
        {
            float slotGap = 360 / ((float)WheelSlotCount * 2);
            float rotation = transform.eulerAngles.z;
            
            for (int i = 0; i < WheelSlotCount; i++)
            {
                float slotAngle = i * (360 / (float)WheelSlotCount);
    
                if (rotation >= 360 - slotGap) // Covering edge case where it lands on left of 0.
                {
                    transform.DORotate(Vector3.zero, stopWaitTime);
                    
                    HandlePrize(0);
                }
                else
                {
                    if (rotation >= (slotAngle - slotGap) % 360 && rotation <= (slotAngle + slotGap) % 360)
                    {
                        transform.DORotate(new Vector3(0,0,slotAngle), stopWaitTime);
                        
                        HandlePrize(i);
                    }
                }
            }
        }
        
        // TODO: Add more logic here.
        public void HandlePrize(int slot)
        {
            Debug.Log(slot);
        }
    }
}

