using System;
using System.Collections.Generic;
using Damage;
using UnityEngine;
using UnityEngine.Serialization;

namespace HitDetection
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class HurtboxPiece : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _genericDamageMultiplier = 1f;
        [SerializeField] private DamageResistance[] _resistances;

        private Dictionary<DamageType, float> _resistancesDictionary;
        
        private Guid _hurtboxId;
        
        public event Action<DamagePayload> OnDamageTaken;
        
        
        
        private void Awake()
        {
            _resistancesDictionary = new Dictionary<DamageType, float>();
            
            foreach (DamageResistance resistance in _resistances)
            {
                _resistancesDictionary[resistance.Type] = resistance.Multiplier;
            }
        }

        public void SetGuid(Guid guid)
        {
            _hurtboxId = guid;
        }
        
        
        public void TakeDamage(DamagePayload damage)
        {
            if (_resistancesDictionary.TryGetValue(damage.DamageType, out float resistance))
            {
                damage.ApplyResistance(resistance);
            }
            
            damage.ApplyResistance(_genericDamageMultiplier);
            
            Debug.Log($"Took {damage.DamageAmount} damage of type {damage.DamageType}");
            
            OnDamageTaken?.Invoke(damage);
        }
        
        public Guid GetDamageableId()
        {
            return _hurtboxId;
        }
    }
}