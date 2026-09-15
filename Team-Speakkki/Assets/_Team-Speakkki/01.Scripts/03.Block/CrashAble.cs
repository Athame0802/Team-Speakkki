using TeamSpeakkki.Chaewon;
using UnityEngine;

public class CrashAble : MonoBehaviour
{
    [SerializeField] private bool isBreakable = true;
    [SerializeField] private GameObject Coin; // 코인/아이템 등 (선택)
    private int hitCount = 0; // 블록이 맞은 횟수

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        PlayerMove pm = collision.gameObject.GetComponent<PlayerMove>();
        if (pm != null && pm.IsCurrentJumpArising)
        {
            OnHitFromBelow(collision.gameObject);
        }
    }

    void OnHitFromBelow(GameObject player)
    {
        Debug.Log($"{name} 블록이 아래에서 맞았음!");

        // 예: 아이템 박스라면 아이템 스폰
        if (Coin != null && hitCount == 0) // 처음 한 번만 코인 스폰
        {
            Instantiate(Coin, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        // 예: 벽돌이면 파괴
        if (isBreakable)
        {
            hitCount++;
            if(hitCount >= 3) Destroy(gameObject);
        }

        // 파괴되지 않는 블록이면 살짝 튕기는 애니메이션 등 추가 가능
    }
}