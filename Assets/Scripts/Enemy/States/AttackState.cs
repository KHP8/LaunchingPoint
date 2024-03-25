using UnityEngine;

public class AttackState : BaseState
{

    private float moveTimer; 
    private float waitToMove; // how long enemy stays in one place, set to between 5 and 10
    private float losePlayerTimer; 
    private readonly float waitBeforeSearchTime = 3f; //how long enemy remains in attack state before they search
    

    public override void Enter()
    {
        enemy.UseAbility();
        //enemy.agent.SetDestination(enemy.agent.transform.position); // stop moving
        waitToMove = Random.Range(5, 10);
        if (enemy.agent.enabled && !enemy.WouldSee(enemy.target, enemy.agent.destination)) // if they can't see the target from their current destination
        {
            enemy.SetDestination();
        }
    }

    public override void Exit()
    {
        enemy.StopAbility();
    }

    public override void Perform()
    {

        if (enemy.target.GetComponent<PlayerHealth>().health <= 0)
        {
            stateMachine.ChangeState(new PatrolState());
        }

        if (enemy.CanSee(enemy.target)) // player can be seen
        {
            // lock the lose player timer and increment the move and shot timers
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.target.transform);

            // move the enemy to a random position after a random time
            if (moveTimer > waitToMove && enemy.agent.enabled)
            {
                enemy.agent.SetDestination(enemy.GetDestination(enemy.target, enemy.transform.position));
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
            enemy.transform.LookAt(enemy.lastKnownPos + Vector3.up * enemy.eyeHeight);
        }
    }

}
