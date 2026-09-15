using TeamSpeakkki.Chaewon;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

namespace TeamSpeakkki.Inchang.CoinUI
{
    public class CoinUI : MonoBehaviour
    {
        [SerializeField] private PlayerWallet playerWallet;
        [SerializeField] private TextMeshProUGUI coinText;

        [SerializeField] private string lableCoinText = "Coin : ";

        private void Start()
        {
            Refresh(playerWallet.CoinCount);
        }

        private void OnEnable()
        {
            playerWallet.OnCoinChanged += Refresh;
        }

        private void OnDisable()
        {
            playerWallet.OnCoinChanged -= Refresh;
        }

        private void Refresh(int coinAmount)
        {
            coinText.text = lableCoinText + coinAmount.ToString("D2");
        }
    }
}
