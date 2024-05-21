using CriticalStrike.WheelOfFortune.Core;
using UnityEngine;

namespace CriticalStrike.WheelOfFortune.Item
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Card Item Base")]
    public class CardItemBase : ScriptableObject
    {
        [Header("Basic Information")]
        [SerializeField] private new string name;
        [SerializeField] private Sprite icon;
        [SerializeField] private Enums.ItemType type;

        [Header("Probability Settings")]
        [Range(0,1)] [SerializeField] private float normalZoneWeight;
        [Range(0,1)] [SerializeField] private float safeZoneWeight;
        [Range(0,1)] [SerializeField] private float superZoneWeight;
        
        [SerializeField] private AnimationCurve rewardQuantityCurve;
        
        public string Name => name;
        public Sprite Icon => icon;
        public Enums.ItemType Type => type;
        public float NormalZoneWeight => normalZoneWeight;
        public float SafeZoneWeight => safeZoneWeight;
        public float SuperZoneWeight => superZoneWeight;
        public AnimationCurve RewardQuantityCurve => rewardQuantityCurve;
    }
}

