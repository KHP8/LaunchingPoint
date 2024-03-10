using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{

    private float moveTimer; 
    private float waitToMove; // how long enemy stays in one place
    private float losePlayerTimer; 
    private float waitBeforeSearchTime = 3f; //how long enemy remains in attack state before they search
    

    public override void Enter()
    {
        enemy.UseAbility();
        enemy.agent.SetDestination(enemy.agent.transform.position);
        waitToMove = Random.Range(5, 10);
    }

    public override void Exit()
    {
        enemy.StopAbility();
    }

    public override void Perform()
    {
        if (enemy.CanSee(enemy.target)) // player can be seen
        {
            // lock the lose player timer and increment the move and shot timers
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.target.transform);

            // move the enemy to a random position after a random time
            if (moveTimer > waitToMove)
            {
                enemy.agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
                waitToMove = Random.Range(5, 10);
            }
            enemy.lastKnownPos = enemy.target.transform.position;
        }
        else // no LOS on player
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > waitBeforeSearchTime)
            {
                //Change to search state
                stateMachine.ChangeState(new SearchState());
            }
        }

        if (enemy.lastKnownPos != null)
        {
            enemy.transform.LookAt(enemy.lastKnownPos);
        }
    }

}
