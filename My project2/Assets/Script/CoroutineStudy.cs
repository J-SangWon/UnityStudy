using System.Collections;
using UnityEngine;

public class CoroutineStudy : MonoBehaviour
{
    //C# 유니티 코루틴(Coroutine) 정리
    //코루틴은 일반적인 함수와 달리 실행을 멈췄다가 다시 이어서 실행할 수 있는 기능
    //이를 이용하면 일정 시간 후 실행되거나,
    //특정 조건을 기다리는 등의 기능을 쉽게 구현할 수 있음
    void Start()
    {
        StartCoroutine(ExampleConroutine() );
    }

    IEnumerator ExampleConroutine()
    {
        Debug.Log("코루틴 시작");
        yield return new WaitForSeconds(2f);//2초 대기(정지 아님)
        Debug.Log("2초 후 실행");
    }
    void Update()
    {
        
    }
}
