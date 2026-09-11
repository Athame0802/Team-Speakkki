using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamSpeakkki.Chaewon
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private PlayerHealth playerHealth;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            playerHealth.OnDied += GameOver;
        }

        private void OnDisable()
        {
            playerHealth.OnDied -= GameOver;
        }

        private void GameOver()
        {
            SceneManager.LoadScene(Scenes.GameOver);
        }
    }
}