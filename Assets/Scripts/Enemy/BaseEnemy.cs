using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This code serves as the base for all enemies
/// 
/// </summary>

/*
    Todos
        LastKnownPos - All files
        SearchState - See sticky note
        AttackState - See sticky note
        AttackState - Look at
        PatrolState - Pathing?
        CanSee - Avoid projectiles (do more than just 1 raycast) (spherecast?) (layermask)
*/

abstract public class BaseEnemy : MonoBehaviour
{
    private StateMachine stateMachine;

    [Header("References")]
    public NavMeshAgent agent;
    public List<GameObject> players = new();
    public Transform projectileSource;
    public GameObject prefab;
    public List<Transform> waypoints = new();
    [HideInInspector] public GameObject target; 
    
    [Header("Sight Values")]
    //public Path path;
    //public float fieldOfView = 85f;
    public float eyeHeight = 0.6f;
    public float localMoveRadius = 10f;
    [HideInInspector] public Vector3 lastKnownPos;

    [Header("Weapon Values")]
    public float rpm;
    public float dmg;
    public float vel;
    public float maxRange;
    [HideInInspector] public bool canCast = true;
    public WaitForSeconds cooldown;
    public Coroutine coro;

    private int enemyLayer;

    //just for debugging purposes, so we can see what state it is in
    [SerializeField] private string currentState;

    /// <summary>
    /// Handles the detailed usage of the enemy ability. May instantiate projectiles, create beams, etc.
    /// </summary>
    abstract public IEnumerator Shoot();

    /// <summary>
    /// Ends the ability.
    /// </summary>
    /// <remarks>
    /// This method should only be called on exiting the attack state.
    /// </remarks>
    abstract public void StopAbility();
    
    /// <summary>
    /// Assigns values to the prefab's collision component.
    /// </summary>
    /// <param name="obj">The prefab of the ability being cast.</param>
    abstract public void ManageCollisionComponents(GameObject obj);


    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        players.Add(GameObject.Find("Player")); // Should be some (empty maybe) game object at the center of the player
        cooldown = new WaitForSeconds(60 / rpm);
        stateMachine.Initialise();
        GameObject poi = GameObject.Find("POIs");
        for (int i = 0; i < poi.transform.childCount && i < 4; i++) // (i < 4) b/c list is hard set to 4
        {
            waypoints.Add(poi.transform.GetChild(i));
        }
        enemyLayer = gameObject.layer;
    }

    void Update()
    {
        currentState = stateMachine.activeState.ToString(); 
    }

    public void UseAbility() 
    {
        coro = StartCoroutine(Shoot());
    }

    public IEnumerator ResetCastCooldown()
    {
        yield return cooldown;
        canCast = true;
    }

    /// <summary>
    /// Returns the Vector3 position that the NavMeshAgent should move to.
    /// Performs 4 runs to see if some area around the target is a good fit
    /// If it is, returns that. Otherwise, return the target's position
    /// </summary>
    /// <param name="obj">Object that you want to see</param>
    /// <returns>A Vector3 of a point around the target</returns>
    public Vector3 GetDestination(GameObject target)
    {
        Vector3 destination;
        int runs = 4;
        while (runs > 0) 
        {
            destination = Random.insideUnitSphere * localMoveRadius;
            if (WouldSee(target, target.transform.position + destination))
                return target.transform.position + destination;
            runs--;
        }

        return target.transform.position;
    }

    /// <summary>
    /// Returns the Vector3 position that the NavMeshAgent should move to.
    /// Performs 4 runs to see if some area around the target is a good fit
    /// If it is, returns that. Otherwise, return the current position
    /// </summary>
    /// <param name="obj">Object that you want to see</param>
    /// <returns>A Vector3 of a point around the pos</returns>
    public Vector3 GetDestination(GameObject target, Vector3 pos)
    {
        Vector3 destination;
        int runs = 4;
        while (runs > 0) 
        {
            destination = Random.insideUnitSphere * localMoveRadius;
            if (WouldSee(target, pos + destination))
                return pos + destination;
            runs--;
        }

        return pos;
    }

    /// <summary>
    /// Moves the enemy to an area around the target where they would see it, or directly to the target
    /// </summary>
    public void SetDestination()
    {
        agent.SetDestination(GetDestination(target));
    }

    /// <summary>
    /// Moves the enemy to the given position
    /// </summary>
    /// <param name="pos">Vector3 position to move to</param>
    public void SetDestination(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    /// <summary>
    /// Stops the NavMesh
    /// </summary>
    public void StopMoving()
    {
        agent.SetDestination(transform.position);
    }

    /// <summary>
    /// Determines if the enemy can see the passed target
    /// </summary>
    /// <param name="target">Object that you want to see</param>
    /// <returns>True if in LOS, otherwise False</returns>
    public bool CanSee(GameObject target)
    {   
        Vector3 v1 = transform.TransformDirection(Vector3.forward);
        Vector3 v2 = target.transform.position - transform.position;
        if (Vector3.Dot(v1, v2) > 0) // if player in front of the enemy
        {
            Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), v2 - (Vector3.up * eyeHeight));
            LayerMask layerMask = ~(1 << enemyLayer);
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
            {
                if (hitInfo.transform.gameObject == target)
                {
                    Debug.DrawRay(ray.origin, ray.direction);
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Returns a bool if the target can be seen from the pos. 
    /// Does not consider direction, only if a LOS can be present
    /// </summary>
    /// <param name="target">Object that you want to see</param>
    /// <param name="pos">The location you want to check</param>
    /// <returns>True if would see the target. Otherwise False</returns>
    public bool WouldSee(GameObject target, Vector3 pos)
    {   
        Vector3 vector = target.transform.position - pos;

        Ray ray = new Ray(pos + (Vector3.up * eyeHeight), vector - (Vector3.up * eyeHeight));
        LayerMask layerMask = ~(1 << enemyLayer);
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            if (hitInfo.transform.gameObject == target)
            {
                Debug.DrawRay(ray.origin, ray.direction);
                return true;
            }
        }
        return false;
    }

}
