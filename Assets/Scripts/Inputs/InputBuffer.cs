using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    [Serializable]
    public class InputBuffer
    { 
        private List<InputBufferAction> _buffer = new List<InputBufferAction>();

        public event Action<string> OnActionEnqueued;
        public event Action<string> OnActionDequeued; 
        
        public void AddAction(InputBufferAction bufferAction)
        {
            foreach (InputBufferAction inputAction in _buffer)
            {
                if (inputAction.ControlName == bufferAction.ControlName)
                {
                    return;
                }
            }
           
            _buffer.Add(bufferAction);
            OnActionEnqueued?.Invoke(bufferAction.ControlName);
        }
        
        public void TickFrame()
        {
            List<InputBufferAction> bufferCopy = new List<InputBufferAction>(_buffer);
            
            foreach (InputBufferAction action in bufferCopy)
            {
                if (action.ActionTick())
                {
                    _buffer.Remove(action);
                    OnActionDequeued?.Invoke(action.ControlName);
                }
            }
        }
    }
}