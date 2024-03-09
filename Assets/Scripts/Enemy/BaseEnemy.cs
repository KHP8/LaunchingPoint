using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This code serves as the base for all enemies
/// 
/// </summary>

/*
    Todos
        LastKnownPos - All files
        TestEnemy - Shoot
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
    public GameObject [] players = new GameObject[4];
    public Transform projectileSource;
    public GameObject prefab;
    [HideInInspector] public GameObject target; 
    
    [Header("Sight Values")]
    //public Path path;
    //public float fieldOfView = 85f;
    public float eyeHeight = 0.6f;
    [HideInInspector] public Vector3 lastKnownPos;

    [Header("Weapon Values")]
    public float rpm;
    public float dmg;
    public float vel;
    public float maxRange;
    [HideInInspector] public bool canCast = true;
    public WaitForSeconds cooldown;
    public Coroutine coro;

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
        players[0] = GameObject.Find("Capsule"); // Should be some (empty maybe) game object at the center of the player
        cooldown = new WaitForSeconds(60 / rpm);
        stateMachine.Initialise();
    }

    void Update()
    {
        currentState = stateMachine.activeState.ToString(); 
    }

    /// <summary>
    /// Determines if the enemy can see the passed target
    /// </summary>
    /// <param name="obj">Object that you want to see</param>
    /// <returns>True if in LOS, otherwise False</returns>
    public bool CanSee(GameObject obj)
    {   
        Vector3 v1 = transform.TransformDirection(Vector3.forward);
        Vector3 v2 = obj.transform.position - transform.position;
        if (Vector3.Dot(v1, v2) > 0) // if player in front of the enemy
        {
            Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), v2 - (Vector3.up * eyeHeight));
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.transform.tag == "Player")
                {
                    Debug.DrawRay(ray.origin, ray.direction);
                    return true;
                }
            }
        }
        return false;
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
}
