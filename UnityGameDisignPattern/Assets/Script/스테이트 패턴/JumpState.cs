using UnityEngine;

public class JumpState : IState
{
    public void Enter()
    {
        Debug.Log("Jump 시작");
    }
    public void Update()
    {
        Debug.Log("Jump 유지중");
    }

    public void Exit()
    {
        Debug.Log("Jump 종료");
    }
}
