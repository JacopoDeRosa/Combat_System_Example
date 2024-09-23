using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Damage;

namespace HitDetection
{
    public class HitboxController : MonoBehaviour
    {
        [SerializeField] private HitboxPiece[] _hitboxPieces;
        [SerializeField] private DamagePayload _damagePayload;
        [SerializeField] private int _lifeTime;
        
        private List<Guid> _foundHurtboxes;
        private List<Guid> _idsToIgnore;

        
        public void SetLifetime(int lifetime)
        {
           _lifeTime = lifetime;
            
        }
        
        private void Awake()
        {
            _foundHurtboxes = new List<Guid>();
        }
        
        public void SetTransformsToIgnore(params Guid[] transformsToIgnore)
        {
            // Down with Linq!
            _idsToIgnore = transformsToIgnore.ToList();
        }
        
        private void CheckPieces()
        {
            foreach (HitboxPiece hitboxPiece in _hitboxPieces)
            {
                 if(hitboxPiece.CheckPiece(out IDamageable damageable) && _foundHurtboxes.Contains(damageable.GetDamageableId()) == false && _idsToIgnore.Contains(damageable.GetDamageableId()) == false)
                 {
                     _foundHurtboxes.Add(damageable.GetDamageableId());
                     damageable.TakeDamage(_damagePayload);
                     Debug.Log("Hit");
                 }
            }
        }

        private void FixedUpdate()
        {
            CheckPieces();
            CheckLifetime();
        }
        
        private void CheckLifetime()
        {
            _lifeTime--;
            if (_lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}