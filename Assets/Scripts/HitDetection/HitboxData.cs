using UnityEngine;

namespace HitDetection
{
    [CreateAssetMenu(fileName = "New Hitbox Data", menuName = "Combat/Hitbox Data", order = 0)]
    public class HitboxData : ScriptableObject
    {
        [SerializeField] private HitboxController _hitboxPrefab;
        [SerializeField] private string _attachmentSocket;
        [SerializeField] private bool _attachToSocket;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private int _duration = 15;
        
        public HitboxController HitboxPrefab => _hitboxPrefab;
        public string AttachmentSocket => _attachmentSocket;
        public bool AttachToSocket => _attachToSocket;
        public Vector3 Offset => _offset;
        public int Duration => _duration;
    }
    
    
}