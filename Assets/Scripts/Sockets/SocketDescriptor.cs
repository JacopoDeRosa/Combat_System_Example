using System;
using UnityEngine;

namespace Sockets
{
    [Serializable]
    public class SocketDescriptor
    {
        [SerializeField] private string _name;
        [SerializeField] private Transform _socketTransform;
        
        public string Name => _name;
        public Transform SocketTransform => _socketTransform;
        
    }
}