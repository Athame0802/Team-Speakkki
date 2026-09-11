using System;
using UnityEngine;

namespace TeamSpeakkki.Chaewon 
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private int coinCount = 0;
        private IPlayerPowerUp currentPowerUp;
    
        public event Action<int> OnCoinChanged;

        public int CoinCount => coinCount;
    
        public void AddCoin(int amount)
        {
    
        }

        public void ChangePowerUp(IPlayerPowerUp powerUp)
        {
            
        }
    }
}