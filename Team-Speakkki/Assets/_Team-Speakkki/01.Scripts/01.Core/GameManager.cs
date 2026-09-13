using UnityEngine;
using UnityEngine.SceneManagement;
// using School.PasitionSync;

namespace TeamSpeakkki.Chaewon
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private PlayerState playerState;

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
            playerState.OnDied += GameOver;
        }

        private void OnDisable()
        {
            playerState.OnDied -= GameOver;
        }

        private void GameOver()
        {
            SceneManager.LoadScene(Scenes.GameOver);
        }
    }
}