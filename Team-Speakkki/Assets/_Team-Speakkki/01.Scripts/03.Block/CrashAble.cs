using TeamSpeakkki.Chaewon;
using UnityEngine;

public class CrashAble : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private int requiredHitsToBreak = 1;
    [SerializeField] private bool isBreakable = true;
    [SerializeField] private GameObject SpawningObjectOnHit;

    private int hitCount = 0; // 블록이 맞은 횟수

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != playerLayer.value) 
            return;

        PlayerMove pm = collision.gameObject.GetComponent<PlayerMove>();
        if (pm != null && pm.IsCurrentJumpArising)
        {
            OnHitFromBelow(collision.gameObject);
        }
    }
    
    void OnHitFromBelow(GameObject player)
    {
        Debug.Log($"{name} 블록이 아래에서 맞았음!");

        // 아이템 박스라면 처음 한 번만 아이템 스폰
        if (SpawningObjectOnHit != null && hitCount == 0)
        {
            Instantiate(SpawningObjectOnHit, transform.position + Vector3.up * 0.5f, Quaternion.identity);
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