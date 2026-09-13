using System;
using UnityEngine;

namespace TeamSpeakkki.Chaewon 
{
    public class PlayerWallet : MonoBehaviour
    {
        [SerializeField] private int coinCount = 0;
        [SerializeField] private int maxCoinCount = 99;
    
        public event Action<int> OnCoinChanged;

        public int CoinCount => coinCount;
    
        public void AddCoin(int amount)
        {
            bool isMaxOver = coinCount + amount > maxCoinCount;
            coinCount = isMaxOver ? maxCoinCount : coinCount + amount;

            OnCoinChanged?.Invoke(coinCount);
        }
    }
}