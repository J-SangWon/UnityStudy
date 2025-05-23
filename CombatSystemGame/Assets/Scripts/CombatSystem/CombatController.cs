using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] MeeleFighter meeleFighter;
    private void Awake()
    {
        meeleFighter = GetComponent<MeeleFighter>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var enemy = EnemyManager.instance.GetAttackingEnemy();
            if (enemy && enemy.fighter.IsCounterable && !meeleFighter.inAction)
            {
                StartCoroutine(meeleFighter.PerformCounterAttack(enemy));
            }
            else
            {
                meeleFighter.TryToAttack();
            }




        }
    }

    //private void OnAnimatorMove()
    //{
    //    if (!meeleFighter.InCounter)
    //    {
    //        transform.position += anim.deltaPosition;
    //    }

    //    transform.rotation *= anim.deltaRotation;
    //}

}
