using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    

    public override void Enter()
    {
        enemy.SetDestination(enemy.lastKnownPos);
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

        if (enemy.agent.velocity.magnitude < 1)
        {
            Debug.Log("Stopped");
            moveTimer += Time.deltaTime;
        }

        if (moveTimer > Random.Range(3,5))
        {
            enemy.SetDestination(enemy.transform.position + (Random.insideUnitSphere * enemy.localMoveRadius));
            moveTimer = 0;
        }

        if (searchTimer > 10)
            stateMachine.ChangeState(new PatrolState());
    }
}
