using UnityEngine;

public class IdleState : State<EnemyController>
{
    EnemyController enemy;

    public override void Enter(EnemyController owner)
    {
        enemy = owner;
        Debug.Log("IdleEnter");
    }
    public override void Execute()
    {
        //base.Execute();
        Debug.Log("IdleExcute");
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            enemy.ChangeState(EnemyStates.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("IdleExit");
    }

}
