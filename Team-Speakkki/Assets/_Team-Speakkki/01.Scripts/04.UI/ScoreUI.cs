using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

namespace TeamSpeakkki.Inchang.ScoreUI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        [SerializeField] private string label_score_text = "Score : ";

        private int currentScore;

        private void Start()
        {
            Refresh();
        }

        private void AddScore(int amount)
        {
            currentScore += amount;
            Refresh();
        }

        private void Refresh()
        {
            scoreText.text = label_score_text + currentScore.ToString("D6");
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                AddScore(100);
            }
        }
    }
}
