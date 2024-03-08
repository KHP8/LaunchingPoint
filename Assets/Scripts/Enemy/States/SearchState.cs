using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;

    public override void Enter()
    {
        enemy.agent.SetDestination(enemy.lastKnownPos);
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        for (int i = 0; i < enemy.players.Length; i++)
            {
            if (enemy.CanSee(enemy.players[i]))
                stateMachine.ChangeState(new AttackState());

            if (enemy.agent.remainingDistance < enemy.agent.stoppingDistance)
            {
                searchTimer += Time.deltaTime;
                moveTimer += Time.deltaTime;

                if (moveTimer > Random.Range(3,5))
                {
                    enemy.agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                    moveTimer = 0;
                }

                if (searchTimer > 10)
                    stateMachine.ChangeState(new PatrolState());
            }
            
        }
    }
}
