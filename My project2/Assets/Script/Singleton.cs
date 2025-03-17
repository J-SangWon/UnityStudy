using UnityEngine;

public class Singleton : MonoBehaviour
{
    //유니티에서 싱글톤을 사용하면 하나의 인스턴스만 유지하면서 어디서든 접근 가능하게 만들 수 있음
    public static Singleton Instance {  get; private set; }


    private void Awake() //start보타 1회 빠르게 실행
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //씬이 바뀌어도 유지되게 하는 함수
        }
        else if (SoundManager.instance != this)
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 새로운 인스턴스를 파괴
        }
    }

    public void PrintMessage()
    {
        Debug.Log("싱글톤 메시지 출력");
    }

}
