using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("이 오브젝트와 충돌 시 자동으로 삭제됩니다.")]
    public bool destroyOnContact = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 충돌했는지 확인
        if (collision.CompareTag("Player"))
        {
            // GameManager를 통해 클리어 처리
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnReachGoal();
            }

            // 필요 시 오브젝트 제거
            if (destroyOnContact)
            {
                // 예를 들어, 플래그나 아이템을 수집하는 경우를 대비해
                // 충돌 후 오브젝트를 비활성화하거나 제거할 수 있습니다.
                gameObject.SetActive(false);
            }
        }
    }
}
