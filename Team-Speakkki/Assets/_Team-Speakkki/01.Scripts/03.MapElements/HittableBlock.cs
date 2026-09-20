using TeamSpeakkki.Chaewon;
using UnityEngine;

public class HittableBlock : MonoBehaviour
{
    private static readonly Vector2 blockRepulsiveForce = new Vector2(0, -3f);

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private new Collider2D collider;

    [SerializeField] private int requiredHitsToBreak = 1;
    [SerializeField] private bool isBreakable = true;
    [SerializeField] private GameObject SpawningObjectOnFirstHit;

    private int hitCount = 0; // 블록이 맞은 횟수

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool isSameLayer = (playerLayer.value & (1 << collision.gameObject.layer)) != 0;
        if (!isSameLayer)
            return;

        float playerTopY = collision.collider.bounds.max.y;
        float blockBottomY = collider.bounds.min.y;

        if (playerTopY < blockBottomY)
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

        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        playerMove.ApplyBlockRepulsiveForce(blockRepulsiveForce);
    }

    // TODO: 부서질 때 효과 추가
    private void Break()
    {
        Destroy(gameObject);
    }
}