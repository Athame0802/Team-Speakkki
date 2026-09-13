using UnityEngine;
using System;

namespace TeamSpeakkki.Chaewon
{
    public class PlayerState : MonoBehaviour
    {
        public event Action OnDied;

        private PlayerPowerUp currentPowerUp;

        public void ChangePowerUp(PlayerPowerUp powerUp)
        {
            currentPowerUp = powerUp;
        }

        public void TakeDamage()
        {
            currentPowerUp?.OnDamaged(this);
        }

        public PlayerPowerUp GetPowerUpWhenBeDamaged(int powerUpLevelBeforeBeDamaged)
        {
            return powerUpLevelBeforeBeDamaged switch
            {
                2 => new MushroomPowerUp(),
                1 => new BasicPowerUp(),
                _ => throw new ArgumentException($"[PlayerState] 데미지를 입었을 때 바뀔 파워업을 구하는 메서드에 1 혹은 2가 아닌 {powerUpLevelBeforeBeDamaged}가 전달되었습니다.")
            };
        }

        public void Die()
        {
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
