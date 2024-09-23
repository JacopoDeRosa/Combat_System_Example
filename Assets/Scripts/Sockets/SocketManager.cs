using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sockets
{
    public class SocketManager : MonoBehaviour
    {
        [SerializeField] private SocketDescriptor[] _socketDescriptors;
        
        private Dictionary<string, Transform> _sockets;

        private void Awake()
        {
            _sockets = new Dictionary<string, Transform>();
            
            foreach (SocketDescriptor descriptor in _socketDescriptors)
            {
                _sockets.Add(descriptor.Name, descriptor.SocketTransform);
            }
        }
        
        public Transform GetSocket(string socketName)
        {
            if (_sockets.ContainsKey(socketName))
            {
                return _sockets[socketName];
            }

            return null;
        }
    }
}