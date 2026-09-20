using System;
using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    [DefaultExecutionOrder(-100)]
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        
        public event Action OnJumpKeyDown;
        public event Action OnJumpKeyUp;

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
                OnJumpKeyDown?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                OnJumpKeyUp?.Invoke();
            }
        }
    }
}