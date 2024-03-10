using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    private Vector3 destination;

    public override void Enter()
    {
        while (enemy.target == null)
        {
            enemy.target = enemy.players[Random.Range(0, enemy.players.Count-1)];
        }
        destination = GetDestination(enemy.target);
        enemy.agent.SetDestination(destination);
        //waypointIndex = FindClosestWaypoint();
        //enemy.agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
    }

    public override void Perform()
    {
        //PatrolCycle();
        foreach (GameObject player in enemy.players)
        {
            if (player == null) continue;

            if (enemy.CanSee(player))
            {
                enemy.target = player;
                stateMachine.ChangeState(new AttackState());
            }
        }

        if (!WouldSee(enemy.target, destination))
        {
            destination = GetDestination(enemy.target);
            enemy.agent.SetDestination(destination);
        }
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj">Object that you want to see</param>
    /// <returns></returns>
    public Vector3 GetDestination(GameObject obj)
    {
        Vector3 destination;
        int runs = 4;
        while (runs > 0) 
        {
            destination = Random.insideUnitSphere * 10;
            if (WouldSee(obj, enemy.target.transform.position + destination))
                return enemy.target.transform.position + destination;
            runs--;
        }

        return enemy.target.transform.position;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">Object that you want to see</param>
    /// <param name="pos">The location you want to check</param>
    /// <returns></returns>
    public bool WouldSee(GameObject target, Vector3 pos)
    {   
        Vector3 vector = target.transform.position - pos;

        Ray ray = new Ray(pos + (Vector3.up * enemy.eyeHeight), vector - (Vector3.up * enemy.eyeHeight));
        LayerMask layerMask = ~(1 << 8);
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            if (hitInfo.transform.tag == "Player")
            {
                Debug.DrawRay(ray.origin, ray.direction);
                return true;
            }
        }

        return false;
    }


    /*
    public void PatrolCycle() 
    {
        // implement patrol logic
        if (enemy.agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                if (waypointIndex < enemy.path.waypoints.Count - 1)
                    waypointIndex++;
                else
                    waypointIndex = 0;
                
                enemy.agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                Debug.Log(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0;
            }
        }
    }

    public int FindClosestWaypoint()
    {   
        //assumes waypoints are not empty
        int closest = 0;
        for (int i = 1; i < enemy.path.waypoints.Count; i++)
        {
            if (Vector3.Distance(enemy.path.waypoints[i].position, enemy.agent.transform.position) < 
                Vector3.Distance(enemy.path.waypoints[closest].position, enemy.agent.transform.position))
            {
                closest = i;
            }
        }
        return closest;
    }   
    */
}
