using System;

namespace Damage
{
    public interface IDamageable
    {
        public void TakeDamage(DamagePayload damage);
        
        public Guid GetDamageableId();
    }
}