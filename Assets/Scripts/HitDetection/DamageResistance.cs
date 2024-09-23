using System;
using Damage;
using UnityEngine;
using UnityEngine.Serialization;

namespace HitDetection
{
    [Serializable]
    public class DamageResistance
    {
        [FormerlySerializedAs("_type")] [SerializeField] private DamageType type;
        [SerializeField] private float _multiplier = 1f;
        
        public DamageType Type => type;
        public float Multiplier => _multiplier;
    }
}