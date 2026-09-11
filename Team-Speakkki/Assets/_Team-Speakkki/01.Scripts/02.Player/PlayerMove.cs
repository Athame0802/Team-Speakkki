using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private PlayerGroundChecker groundChecker;
        [SerializeField] private Rigidbody2D rb;

        [SerializeField] private float jumpPower = 3f;
        [SerializeField] private float moveSpeed = 3f;

        private void OnEnable()
        {
            InputManager.Instance.OnJumpKeyPressed += TryJump;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnJumpKeyPressed -= TryJump;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            rb.linearVelocityX = InputManager.Instance.HorizontalInput;
        }

        private void TryJump()
        {
            if (!groundChecker.IsGrounded)
                return;

            // TODO: 코요테 타임, 점프 버퍼 구현

            Jump();
        }

        private void Jump()
        {
            rb.AddForce(new Vector2(0, jumpPower));
        }
    }
}
