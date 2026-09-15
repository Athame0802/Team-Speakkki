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

        private int currentCoin;

        private void Start()
        {
            Refresh(playerWallet.CoinCount);
        }

        private void OnEnable()
        {
            currentCoin += amount;
            Refresh();
        }

        private void OnDisable()
        {
            coinText.text = label_coin_text + currentCoin.ToString("D2");
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (currentCoin > 98) return;
                AddCoin(1);
            }
        }
    }
}
