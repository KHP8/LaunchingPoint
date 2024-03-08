using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{

    private float moveTimer; //make the enemy move every so often
    private float losePlayerTimer; 
    private float waitBeforeSearchTime = 8f; //how long enemy remains in attack state before they search
    private float shotTimer;


    public override void Enter()
    {
        enemy.agent.SetDestination(enemy.agent.transform.position);
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSee(enemy.players[0])) // player can be seen
        {
            // lock the lose player timer and increment the move and shot timers
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.players[0].transform);
            
            // 
            if (shotTimer > enemy.rpm)
            {
                enemy.Shoot();
            }

            // move the enemy to a random position after a random time
            if (moveTimer > Random.Range(3,5))
            {
                enemy.agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
            //enemy.lastKnownPos = enemy.player.transform.position;
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
            enemy.transform.LookAt(enemy.players[0].transform);
        }
    }

}
