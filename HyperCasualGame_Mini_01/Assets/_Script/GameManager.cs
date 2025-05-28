using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject clearPanel; // 클리어 패널 (UI)
    public TextMeshProUGUI clearText; // 클리어 텍스트

    [Header("Settings")]
    public string clearMessage = "Stage Clear!";

    private bool isCleared = false;

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 초기화 시 클리어 UI 비활성화
        if (clearPanel != null)
            clearPanel.SetActive(false);
    }

    // 클리어 지점에 도달했을 때 호출할 메서드
    public void OnReachGoal()
    {
        if (isCleared) return; // 이미 클리어한 상태면 무시

        isCleared = true;

        // 클리어 UI 표시
        if (clearPanel != null)
        {
            clearPanel.SetActive(true);
            if (clearText != null)
                clearText.text = clearMessage;
        }

        // 플레이어 조작 비활성화
        var player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            player.enabled = false;
            // 플레이어의 Rigidbody 정지
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }


}
