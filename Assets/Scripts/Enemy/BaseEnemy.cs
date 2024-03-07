using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This code serves as the base for all enemies
/// 
/// </summary>

abstract public class BaseEnemy : MonoBehaviour
{
    private StateMachine stateMachine;

    public NavMeshAgent agent;
    public GameObject [] players = new GameObject[4];
    public Vector3 lastKnownPos;
    
    [Header("Sight Values")]
    //public Path path;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight = 0.6f;

    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10)] public float fireRate;

    //just for debugging purposes, so we can see what state it is in
    [SerializeField] private string currentState;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        players[0] = GameObject.Find("Capsule"); // Should be some (empty maybe) game object at the center of the player
        stateMachine.Initialise();
    }

    void Update()
    {
        currentState = stateMachine.activeState.ToString(); 
    }

    public bool CanSeePlayer(int index)
    {   
        if (players != null)
        {   
            GameObject player = players[index];
            // is the player close enough to be seen
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance) 
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.tag == "Player")
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;

    }
}
