using UnityEditor.Animations;
using UnityEngine;

namespace Fighters
{
    [CreateAssetMenu(fileName = "FighterData", menuName = "Fighters/FighterData")]
    public class FighterData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _health;
        [SerializeField] private RuntimeAnimatorController _animator;
        
        public string Name => _name;
        public int Health => _health;
        public RuntimeAnimatorController Animator => _animator;
    
    }
}
