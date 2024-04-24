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
        // Check if target is dead
        if (enemy.target.GetComponent<PlayerHealth>().health <= 0)
        {
            stateMachine.ChangeState(new PatrolState());
            return;
        }

        // Perform movement checks
        float dist = Vector3.Distance(enemy.target.transform.position, enemy.transform.position);

        if (enemy.CanSee(enemy.target)) // Can see target
        {
            // lock the lose player timer and increment the move timer
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.target.transform);

            // move the enemy to a random position after a random time
            if ((moveTimer > waitToMove && enemy.agent.enabled) 
                || (dist > .85f * enemy.maxSight && dist < enemy.maxSight) // IF player getting far
                || (dist < .2f * enemy.maxSight && enemy.agent.enabled && enemy.agent.isStopped) // IF player too close
            )
            {
                enemy.SetDestination(); // used to be the other GetDestination/SetDestination combo
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
                // Was previously SearchState()
                stateMachine.ChangeState(new PatrolState()); 
            }
        }

        // Look at the target
        if (enemy.lastKnownPos != null)
        {
            enemy.transform.LookAt(enemy.lastKnownPos + Vector3.up * enemy.eyeHeight);
        }
    }

}
