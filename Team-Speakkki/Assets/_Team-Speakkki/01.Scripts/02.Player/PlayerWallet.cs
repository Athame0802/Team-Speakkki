using System;
using UnityEngine;

namespace TeamSpeakkki.Chaewon 
{
    public class PlayerWallet : MonoBehaviour
    {
        [SerializeField] private PlayerState playerState;
        [SerializeField] private int coinCount = 0;
        [SerializeField] private int maxCoinCount = 99;
    
        public event Action<int> OnCoinChanged;

        public int CoinCount => coinCount;

        private void OnEnable()
        {
            playerState.OnDied += OnDied;
        }

        private void OnDisable()
        {
            playerState.OnDied -= OnDied;
        }

        public void AddCoin(int amount)
        {
            bool isMaxOver = coinCount + amount > maxCoinCount;
            coinCount = isMaxOver ? maxCoinCount : coinCount + amount;

            OnCoinChanged?.Invoke(coinCount);
        }

        private void OnDied()
        {
            coinCount = 0;
            OnCoinChanged?.Invoke(coinCount);
        }
    }
}