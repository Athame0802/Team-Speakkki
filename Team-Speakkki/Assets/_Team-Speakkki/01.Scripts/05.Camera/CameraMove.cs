using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target;
    [SerializeField] private float max_X;
    [SerializeField] private float min_X;
    void LateUpdate()
    {
        if (target == null) return;

        float target_x = Mathf.Clamp(target.position.x, min_X, max_X);

        if (target_x < transform.position.x) return;

        transform.position = new Vector2(target_x, transform.position.y);
    }
}
