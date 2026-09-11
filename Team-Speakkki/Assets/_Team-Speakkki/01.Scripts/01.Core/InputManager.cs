using System;
using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        public event Action OnJumpKeyPressed;
        private float horizontalInput;

        public float HorizontalInput => horizontalInput;

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

        private void Update()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            // TODO: Space가 아닌 다른 키로도 점프키면 입력 되게 만들기
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnJumpKeyPressed?.Invoke();
            }
        }
    }
}