using System.Collections.Generic;
using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class DeathZone : MonoBehaviour
    {
        private const float DEATH_ZONE_HEIGHT = 1f;

        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask monsterLayer;

        private float widthOfMap = 0f;
        private List<Collider2D> objectsInDeathZone = new(5);
        private ContactFilter2D zoneContactFilter;

        private void Awake()
        {
            zoneContactFilter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = playerLayer | monsterLayer
            };
        }

        private void FixedUpdate()
        {
            int collisionCount = Physics2D.OverlapBox(
                transform.position, 
                new Vector2(widthOfMap * 1.5f, DEATH_ZONE_HEIGHT),
                0f,
                zoneContactFilter,
                objectsInDeathZone);

            if (collisionCount <= 0)
                return;

            foreach (Collider2D collisionCollider in objectsInDeathZone)
            {
                bool hasDamageable= collisionCollider.TryGetComponent<IDamageable>(out IDamageable damageable);
                if (!hasDamageable)
                    continue;

                damageable.Die();
            }
        }

        public void SetDeathZone(float mapOriginX, float y, float width)
        {
            float x = (width / 2) + mapOriginX;
            transform.position = new Vector2(x, y);

            widthOfMap = width;
        }
    }
}
