using UnityEngine;

public class Boss : MonoBehaviour
{
    public int BossHP = 10;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnDestroy()
    {
        // 보스가 파괴될 때 SpawnManager의 BossDie 메서드를 호출
        SpawnManager.instance.BossDie();
    }

}
