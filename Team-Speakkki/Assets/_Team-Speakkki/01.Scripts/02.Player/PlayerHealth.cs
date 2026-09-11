using System;
using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private int health = 3;

        public event Action<int> OnHealthChanged;
        public event Action OnDied;

        public int Health => health;
    
        public void TakeDamage(int amount)
        {
            health -= amount;
            OnHealthChanged?.Invoke(health);

            if (health <= 0)
            {
                Die();
            }
        }
    
        public void Heal(int amount)
        {
            bool isMaxOver = health + amount > maxHealth;
            health = isMaxOver ? maxHealth : health + amount;

            OnHealthChanged?.Invoke(health);
        }
    
        private void Die()
        {
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}