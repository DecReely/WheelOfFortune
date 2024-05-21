using CriticalStrike.WheelOfFortuneMiniGame.Core;
using CriticalStrike.WheelOfFortuneMiniGame.Events;
using CriticalStrike.WheelOfFortuneMiniGame.Item;
using CriticalStrike.WheelOfFortuneMiniGame.Misc;
using CriticalStrike.WheelOfFortuneMiniGame.UI;
using CriticalStrike.WheelOfFortuneMiniGame.Zone;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CriticalStrike.WheelOfFortuneMiniGame.Wheel
{
    [HelpURL("https://www.youtube.com/watch?v=eti87kSD_9U")] // Refactored and improved the code in the video.
    [RequireComponent(typeof(Rigidbody2D))]
    public class WheelOfFortune : SingletonMonoBehaviour<WheelOfFortune>
    {
        private const int WheelSlotCount = 8; // Made it a constant since the visual asset has 8 slots.

        [Header("Settings")]
        [Range(200, 1440)] [SerializeField] private float rotatePower = 1200; // Not recommended to make it more than 1440, rotates too much.
        [Range(0, 50)] [SerializeField] private float rotatePowerRandomnessPercentage = 30;
        [Range(100, 600)] [SerializeField] private float stopPower = 200;
        [Range(0.1f, 1f)] [SerializeField] private float stopWaitTime = 0.3f;

        #region Variables

        private WheelUISlot[] _wheelSlots;
        private bool _isRotating;
        private float _stopTimer;

        #endregion

        #region Dependencies

        private Rigidbody2D _rigidbody2D;

        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _wheelSlots = GetComponentsInChildren<WheelUISlot>();
        }

        private void Start()
        {
            RandomizeSlotRewards();
        }

        private void Update()
        {
            HandleRotating();
        }

        public void RandomizeSlotRewards()
        {
            if (ZoneManager.Instance.GetCurrentZoneType() == Enums.ZoneType.Normal)
            {
                #region Assign Death Slot

                int deathSlotIndex = Random.Range(0, WheelSlotCount);
            
                _wheelSlots[deathSlotIndex].SetCurrentReward(RewardGenerator.Instance.GetDeathReward());

                #endregion

                #region Assign Rest Of The Slots

                for (int i = 0; i < WheelSlotCount; i++)
                {
                    if (i != deathSlotIndex)
                    {
                        _wheelSlots[i].SetCurrentReward(RewardGenerator.Instance.GenerateRandomReward());
                    }
                }

                #endregion
            }
            else
            {
                for (int i = 0; i < WheelSlotCount; i++)
                {
                    _wheelSlots[i].SetCurrentReward(RewardGenerator.Instance.GenerateRandomReward());
                }
            }
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
                    EventManager.CallSpinEndedEvent();
                }
            }
        }
        
        public void Rotate()
        {
            if (_isRotating || ZoneManager.Instance.HasExceededMaxLevel() || GameManager.Instance.IsGameEnded())
            {
                Debug.LogError("Wheel can't be spun!");
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
    
                if (rotation >= 360 - slotGap) // Covering edge case where it lands on the left side of 0.
                {
                    transform.DORotate(new Vector3(0,0,0), stopWaitTime).OnComplete(GameManager.Instance.HandleGameState);
                    
                    HandlePrize(0);
                    
                    StopRotating();

                    break;
                }
                else
                {
                    if (rotation >= (slotAngle - slotGap) % 360 && rotation <= (slotAngle + slotGap) % 360)
                    {
                        transform.DORotate(new Vector3(0,0,slotAngle), stopWaitTime).OnComplete(GameManager.Instance.HandleGameState);
                        
                        HandlePrize(i);
                        
                        StopRotating();

                        break;
                    }
                }
            }
        }

        private void HandlePrize(int slot)
        {
            Debug.Log(slot);
            PlayerInventory.Instance.AddToInventory(_wheelSlots[slot].GetCurrentReward());
        }

        public bool IsRotating()
        {
            return _isRotating;
        }
    }
}

