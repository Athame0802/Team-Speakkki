using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class PlayerMove : MonoBehaviour
    {

        [SerializeField] private PlayerDetector detector;
        [SerializeField] private Rigidbody2D rb;

        [SerializeField] private float jumpPower = 3f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float gravityScaleOnJumpKeyHoldDown = 3f;
        
        private static readonly Vector2 blockRepulsiveForce = new Vector2(0, -5f);

        private float basicGravityScale;

        private bool isThisJumpRepulsiveForceAlreadyApplied = false;

        private void Awake()
        {
            basicGravityScale = rb.gravityScale;
        }

        private void OnEnable()
        {
            InputManager.Instance.OnJumpKeyDown += TryJump;

            detector.OnGroundedChangedToTrue += RecoverJumpGravityScale;
            detector.OnHeadCollision += RecoverJumpGravityScale;
            detector.OnHeadCollision += ApplyRepulsiveForce;
            InputManager.Instance.OnJumpKeyUp += RecoverJumpGravityScale;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnJumpKeyDown -= TryJump;

            detector.OnGroundedChangedToTrue -= RecoverJumpGravityScale;
            detector.OnHeadCollision -= RecoverJumpGravityScale;
            detector.OnHeadCollision -= ApplyRepulsiveForce;
            InputManager.Instance.OnJumpKeyUp -= RecoverJumpGravityScale;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void ApplyRepulsiveForce()
        {
            // 점프 한 번에 블럭을 2개 쳤을 때 반발력이 2번 적용되지 않게 하기 위한 조치
            if (isThisJumpRepulsiveForceAlreadyApplied)
                return;

            Debug.Log("반발력 적용됨");

            rb.AddForce(blockRepulsiveForce, ForceMode2D.Impulse);
            isThisJumpRepulsiveForceAlreadyApplied = true;
        }

        private void Move()
        {
            rb.linearVelocityX = InputManager.Instance.HorizontalInput * moveSpeed;
        }

        private void TryJump()
        {
            if (!detector.IsGrounded)
                return;

            // TODO: 코요테 타임, 점프 버퍼 구현

            Jump();
            rb.gravityScale = gravityScaleOnJumpKeyHoldDown;
        }

        private void RecoverJumpGravityScale()
        {
            rb.gravityScale = basicGravityScale;
        }

        private void Jump()
        {
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            isThisJumpRepulsiveForceAlreadyApplied = false;
        }
    }
}
