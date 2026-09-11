using System;
using UnityEngine;

namespace TeamSpeakkki.Chaewon 
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private int coinCount = 0;
        [SerializeField] private int maxCoinCount = 99;
        private IPlayerPowerUp currentPowerUp;
    
        public event Action<int> OnCoinChanged;

        public int CoinCount => coinCount;
    
        public void AddCoin(int amount)
        {
            bool isMaxOver = coinCount + amount > maxCoinCount;
            coinCount = isMaxOver ? maxCoinCount : coinCount + amount;

            OnCoinChanged?.Invoke(coinCount);
        }

        public void ChangePowerUp(IPlayerPowerUp powerUp)
        {
            
        }
    }
}