using UnityEngine;

public class Goomba : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private new Collider2D collider;
    public float minX = 30f, maxX = 60f, speed = 2f;
    private int direction = 1;
    private int hitCount = 0; // 굼바가 맞은 횟수

    void Update()
    {
        transform.position += Vector3.right * speed * direction * Time.deltaTime;

        if (transform.position.x >= maxX) direction = -1;
        else if (transform.position.x <= minX) direction = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool isSameLayer = (playerLayer.value & (1 << collision.gameObject.layer)) != 0;
        if (!isSameLayer)
            return;

        float playerTopY = collision.collider.bounds.max.y;
        float blockBottomY = collider.bounds.min.y;

        if (playerTopY < blockBottomY)
        {
            hitCount++;
            if(hitCount >= 2) OnHit(collision.gameObject);
        }
    }

    void OnHit(GameObject player)
    {
        Debug.Log($"[Goomba] {name} 굼바가 플레이어에게 맞았습니다.");
        Destroy(gameObject);
    }
}
