using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 특정 태그를 가진 오브젝트와 닿았을 때만 반응
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Clear!");
            // 여기에 원하는 이벤트 실행
        }
    }
}