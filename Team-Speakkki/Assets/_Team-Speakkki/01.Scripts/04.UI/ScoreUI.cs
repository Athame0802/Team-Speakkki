using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;



namespace HIC
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

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
            scoreText.text = currentScore.ToString("D6");
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddScore(100);
            }
        }
    }
}
