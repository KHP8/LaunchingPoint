using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{

    private float moveTimer; //make the enemy move every so often
    private float losePlayerTimer; //how long enemy remains in attack state before they search
    private float waitBeforeSearchTime = 8f;
    private float shotTimer;


    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.Agent.transform.position);
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer()) // player can be seen
        {
            // lock the lose player timer and increment the move and shot timers
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            
            // 
            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }

            // move the enemy to a random position after a random time
            if (moveTimer > Random.Range(3,5))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
            enemy.LastKnownPos = enemy.Player.transform.position;
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

        if (enemy.LastKnownPos != null)
        {
            enemy.transform.LookAt(enemy.LastKnownPos);
        }
    }

    public void Shoot()
    {
        //store a reference to the gun barrel
        Transform gunBarrel = enemy.gunBarrel;
        //instantiate a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, enemy.transform.rotation);
        //calculate direction to player
        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.transform.position).normalized;
        //add force rigidbody of the bullet
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f, 3f) , Vector3.up) * shootDirection * 40;

        Debug.Log("Shooting");
        shotTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
