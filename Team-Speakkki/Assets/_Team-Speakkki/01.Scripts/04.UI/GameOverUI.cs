using TeamSpeakkki.Chaewon;
using TeamSpeakkki.Managers;
using UnityEngine;

namespace TeamSpeakkki.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;

        private void Start()
        {
            gameOverPanel.SetActive(false);
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameOver += Show;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnGameOver -= Show;
            }
        }

        private void Show()
        {
            gameOverPanel.SetActive(true);
            TimeManager.Instance.StopTimer();
        }
    }
}