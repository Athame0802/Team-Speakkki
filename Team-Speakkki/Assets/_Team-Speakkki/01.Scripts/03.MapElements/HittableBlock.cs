using TeamSpeakkki.Chaewon;
using UnityEngine;

public class HittableBlock : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private new Collider2D collider;

    [SerializeField] private int requiredHitsToBreak = 1;
    [SerializeField] private bool isBreakable = true;
    [SerializeField] private GameObject SpawningObjectOnFirstHit;
    [SerializeField] private float allowedDiff = 0.1f;

    private int hitCount = 0; // 블록이 맞은 횟수

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool isSameLayer = (playerLayer.value & (1 << collision.gameObject.layer)) != 0;
        if (!isSameLayer)
            return;

        float playerTopY = collision.collider.bounds.max.y;
        float blockBottomY = collider.bounds.min.y;

        float yDiff = Mathf.Abs(playerTopY - blockBottomY);

        if (yDiff < allowedDiff)
            OnHit(collision.gameObject);
    }
    
    void OnHit(GameObject player)
    {
        Debug.Log($"[HittableBlock] {name} 블록이 플레이어에게 맞았습니다.");

        bool shouldSpawnFirstHitObject = hitCount == 0 && SpawningObjectOnFirstHit != null;
        if (shouldSpawnFirstHitObject)
        {
            Instantiate(SpawningObjectOnFirstHit, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        if (isBreakable)
        {
            hitCount++;

            if (hitCount >= requiredHitsToBreak) 
                Break();
        }
    }

    // TODO: 부서질 때 효과 추가
    private void Break()
    {
        Destroy(gameObject);
    }
}