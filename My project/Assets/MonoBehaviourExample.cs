using UnityEngine;

public class MonoBehaviourExample : MonoBehaviour
{
    void Start()
    {
        Debug.Log("시작!");
    }

    void Update()
    {
        Debug.Log("프레임 마다 호출");
    }

    private void FixedUpdate()
    {
        Debug.Log("물리 연산");
    }


}
