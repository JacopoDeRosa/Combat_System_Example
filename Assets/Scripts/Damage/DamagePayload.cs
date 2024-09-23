using System;
using UnityEngine;

namespace Damage
{
    [Serializable]
    public struct DamagePayload
    {
        [SerializeField] private float _damageAmount;
        [SerializeField] private DamageType damageType;
        
        public float DamageAmount => _damageAmount;
        public DamageType DamageType => damageType;
        
        public DamagePayload(float damageAmount, DamageType damageType)
        {
            _damageAmount = damageAmount;
            this.damageType = damageType;
        }
        
        public void ApplyResistance(float resistance)
        {
            _damageAmount *= resistance;
        }
    }
}