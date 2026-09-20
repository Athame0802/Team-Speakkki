using System;
using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask blockLayer;
        [SerializeField] private GameObject playerFeet;
        [SerializeField] private GameObject playerHead;
        [SerializeField] private Vector2 headDetectingSize = new Vector2(0.9f, 0.05f);
        [SerializeField] private Vector2 feetDetectingSize = new Vector2(0.9f, 0.05f);


        private ContactFilter2D solidContactFilter;
        private Collider2D[] colliders = new Collider2D[4];
        private bool isGrounded = false;
        public event Action OnHeadCollision;
        public event Action OnGroundedChangedToTrue;

        public bool IsGrounded => isGrounded;

        private void Awake()
        {
            solidContactFilter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = blockLayer | groundLayer
            };
        }

        private void FixedUpdate()
        {
            int detectedGroundAmount = DetectSolid(
                playerFeet.transform.position, 
                feetDetectingSize);

            bool currentGrounded = detectedGroundAmount > 0;

            if (currentGrounded == !isGrounded && currentGrounded)
                OnGroundedChangedToTrue?.Invoke();

            isGrounded = currentGrounded;

            int detectedHeadCollisionAmount = DetectSolid(
                playerHead.transform.position, 
                headDetectingSize);

            if (detectedHeadCollisionAmount > 0)
                OnHeadCollision?.Invoke();
        }

        private int DetectSolid(Vector2 position, Vector2 size)
        {
            return Physics2D.OverlapBox(
                position,
                size,
                0f,
                solidContactFilter,
                colliders);
        }
    }
}
