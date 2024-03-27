using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{

    public override void Enter()
    {
        // First, see if a player is in range. If there is, that is the target.
        // Otherwise, choose one at random to seek to.
        bool playerVisible = false;
        foreach (GameObject player in enemy.players)
        {
            if (player != null)
            {
                if (enemy.CanSee(player))
                {
                    playerVisible = true;
                    enemy.target = player;
                    stateMachine.ChangeState(new AttackState());
                    break;
                }
            }
        }
        if (!playerVisible)
        {
            while (enemy.target == null && enemy.players.Count != 0)
            {
                enemy.target = enemy.players[Random.Range(0, enemy.players.Count)];
            }
            enemy.SetDestination();
        }
    }

    public override void Perform()
    {
        // If a player is in sight, target the player
        foreach (GameObject player in enemy.players)
        {
            if (player == null) continue;

            if (enemy.CanSee(player))
            {
                if (enemy.target != player)
                {
                    enemy.StopMoving(); 
                }
                enemy.target = player;
                stateMachine.ChangeState(new AttackState());
            }
        }

        // If the target would not be seen by the new destination, make a new destination
        if (enemy.agent.enabled && !enemy.WouldSee(enemy.target, enemy.agent.destination))
        {
            enemy.SetDestination();
        }
    }

    public override void Exit()
    {

    }

}
