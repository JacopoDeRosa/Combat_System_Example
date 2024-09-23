using Damage;
using UnityEngine;

namespace HitDetection
{
    public class HitboxPiece : MonoBehaviour
    {
        [SerializeField] private float _height;
        [SerializeField] private float _radius;

        private void DrawCapsuleGizmo(Vector3 position, float height, float radius)
        {
            // Draw a capsule gizmo
            Gizmos.DrawWireSphere(position + Vector3.up * height * 0.5f, radius);
            Gizmos.DrawWireSphere(position - Vector3.up * height * 0.5f, radius);
            Gizmos.DrawLine(position + Vector3.up * height / 2 + Vector3.right * radius, position - Vector3.up * height / 2 + Vector3.right * radius);
            Gizmos.DrawLine(position + Vector3.up * height / 2 - Vector3.right * radius, position - Vector3.up * height / 2 - Vector3.right * radius);
            Gizmos.DrawLine(position + Vector3.up * height / 2 + Vector3.forward * radius, position - Vector3.up * height / 2 + Vector3.forward * radius);
            Gizmos.DrawLine(position + Vector3.up * height / 2 - Vector3.forward * radius, position - Vector3.up * height / 2 - Vector3.forward * radius);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.matrix = transform.localToWorldMatrix;
            DrawCapsuleGizmo(Vector3.zero, _height, _radius);
        }

        public bool CheckPiece(out IDamageable damageable)
        {
            damageable = null;
            
            Collider[] colliders = Physics.OverlapCapsule(transform.localToWorldMatrix * Vector3.down * (_height * 0.5f), transform.localToWorldMatrix * Vector3.up * (_height * 0.5f), _radius);
            
            foreach (Collider c in colliders)
            {
                Debug.Log("Collider Found");
                if (c.TryGetComponent(out damageable))
                {
                    return true;
                }
            }

            return false;

        }
    }
}