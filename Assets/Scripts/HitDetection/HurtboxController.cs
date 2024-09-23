using System;
using Damage;
using UnityEngine;

namespace HitDetection
{
    public class HurtboxController : MonoBehaviour
    {
        [SerializeField] private HurtboxPiece[] _hurtboxPieces;
        
        private Guid _hurtboxId;
        
        public Guid HurtboxId => _hurtboxId;

        private void Awake()
        {
            _hurtboxId = Guid.NewGuid();
            
            foreach (HurtboxPiece piece in _hurtboxPieces)
            {
                piece.SetGuid(_hurtboxId);
                piece.OnDamageTaken += OnDamageTaken;
            }
        }

        private void OnValidate()
        {
            _hurtboxPieces = GetComponentsInChildren<HurtboxPiece>(true);
        }

        private void OnDamageTaken(DamagePayload payload)
        {
            
        }

    }
}
