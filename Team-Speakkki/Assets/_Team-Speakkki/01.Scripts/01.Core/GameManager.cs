using UnityEngine;
using UnityEngine.SceneManagement;
using School.PositionSync;
using System;

namespace TeamSpeakkki.Chaewon
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private PlayerState playerState;

        public Action OnGameOver;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
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
            OnGameOver?.Invoke();
        }
    }
}