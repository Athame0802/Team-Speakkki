using System;
using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int health = 3;
    
        public event Action<int> OnHealthChanged;

        public int Health => health;
    
        public void TakeDamage(int amount)
        {
    
        }
    
        public void Heal(int amount)
        {
    
        }
    
        private void Die()
        {
    
        }
    }
}