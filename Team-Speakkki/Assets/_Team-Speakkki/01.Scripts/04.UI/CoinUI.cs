using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

namespace TeamSpeakkki.Inchang.CoinUI
{
    public class CoinUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinText;

        [SerializeField] private string label_coin_text = "Coin : ";

        private int currentCoin;

        private void Start()
        {
            Refresh();
        }

        private void AddCoin(int amount)
        {
            currentCoin += amount;
            Refresh();
        }

        private void Refresh()
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
