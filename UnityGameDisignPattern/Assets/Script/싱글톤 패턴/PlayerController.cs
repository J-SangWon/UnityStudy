using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private StateMachine stateMachine;
    void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState());
    }

    void Update()
    {
        stateMachine.Update();
        if (Input.GetKeyDown(KeyCode.Space)) stateMachine.ChangeState(new JumpState());
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) stateMachine.ChangeState(new RunState());
        else if (!Input.anyKey) stateMachine.ChangeState(new IdleState());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // 싱글톤 인스턴스에 접근하여 점수 추가
            GameManager.Instance.AddScore(10);
            Destroy(other.gameObject);
        }
    }

}
