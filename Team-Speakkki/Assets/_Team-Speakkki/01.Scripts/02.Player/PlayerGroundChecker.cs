using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class PlayerGroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask blockLayer;
        [SerializeField] private GameObject playerFeet;
        [SerializeField] private Vector2 detectingSize = new Vector2(0.5f, 0.1f);


        private ContactFilter2D groundContanctFilter;
        private Collider2D[] colliders;
        private bool isGrounded = false;

        public bool IsGrounded => isGrounded;

        private void Awake()
        {
            groundContanctFilter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = blockLayer | groundLayer
            };
        }

        private void FixedUpdate()
        {
            int detectedGroundAmount = Physics2D.OverlapBox(playerFeet.transform.position, detectingSize, 0f, groundContanctFilter, colliders);
            isGrounded = detectedGroundAmount > 0;
        }
    }
}
