using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    private float waitBeforeMove;
    

    public override void Enter()
    {
        enemy.SetDestination(enemy.lastKnownPos);
        waitBeforeMove = Random.Range(3, 5);
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        foreach (GameObject player in enemy.players)
        {
            if (player == null) continue;

            if (enemy.CanSee(player))
            {
                enemy.target = player;
                stateMachine.ChangeState(new AttackState());
                break;
            }
        }


        searchTimer += Time.deltaTime;

        if (enemy.agent.enabled && enemy.agent.velocity.magnitude < 1)
        {
            moveTimer += Time.deltaTime;
        }

        if (moveTimer > waitBeforeMove)
        {
            enemy.SetDestination(enemy.transform.position + (Random.insideUnitSphere * enemy.localMoveRadius));
            moveTimer = 0;
            waitBeforeMove = Random.Range(3,5);
        }

        if (searchTimer > 10)
            stateMachine.ChangeState(new PatrolState());
    }
}
