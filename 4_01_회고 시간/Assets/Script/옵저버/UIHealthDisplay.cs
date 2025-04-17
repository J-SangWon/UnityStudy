using UnityEngine;

public class UIHealthDisplay : MonoBehaviour
{
    void Start()
    {
        //이벤트 구독
        EventManager.Instance.AddListner("PlayerHealthChanged", OnPlayerHealthChanged);
        EventManager.Instance.AddListner("PlayerHealthChanged", OnPlayerDied);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListner("PlayerHealthChanged", OnPlayerHealthChanged);
        EventManager.Instance.RemoveListner("PlayerHealthChanged", OnPlayerDied);
    }

    private void OnPlayerHealthChanged(object data)
    {
        int health = (int)data;
        Debug.Log($"UI업데이트 : 체력 변경 {health}");
        //UI요소 업데이트 하는 부분
    }

    private void OnPlayerDied(object data)
    {
        Debug.Log("UI 업데이트 : 플레이어 사망");
    }
}
