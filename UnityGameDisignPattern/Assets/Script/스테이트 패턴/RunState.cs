using UnityEngine;

public class RunState : IState
{
    public void Enter()
    {
        Debug.Log("Run 시작");
    }
    public void Update()
    {
        Debug.Log("Run 유지중");
    }

    public void Exit()
    {
        Debug.Log("Run 종료");
    }
}
