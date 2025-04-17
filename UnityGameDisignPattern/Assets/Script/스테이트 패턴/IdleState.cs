using UnityEngine;

public class IdleState : IState
{
    public void Enter()
    {
        Debug.Log("Idle 시작");
    }
    public void Update()
    {
        Debug.Log("Idle 유지중");
    }

    public void Exit()
    {
        Debug.Log("Idle 종료");
    }

}
