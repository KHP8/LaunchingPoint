using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 100f;
    static public float switchChance = .05f;


    BaseEnemy enemy;

    void Start()
    {
        health = maxHealth;
        enemy = GetComponent<BaseEnemy>();
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    /// <summary>
    /// Deals damage to the enemy. 
    /// Also determines if the enemy should switch targets.
    /// </summary>
    /// <remarks>
    /// hitter should usually be <c>player</c>
    /// </remarks>
    /// <param name="damage">Damage to deal to enemy</param>
    /// <param name="hitter">The player doing damage. Usually should be <c>player</c>.</param>
    public void TakeDamage(float damage, GameObject hitter)
    {
        health -= damage;

        // If dead, die and end here
        if (health <= 0) 
        {
            RoomHandler roomHandler = GetComponentInParent<RoomHandler>();
            roomHandler.enemies.Remove(gameObject);
            roomHandler.numEnemies--;
            roomHandler.UpdateObjective();
            Destroy(gameObject); // do death animation
            return;
        }
        
        // Decide if need to switch targets
        if (!enemy.CanSee(enemy.target)) // If can't see target, then switch to new target
        {
            enemy.target = hitter;
        }
        else if (Random.value < switchChance) // If can see target, roll chance
        {
            enemy.target = hitter;
        }
        else // Decide if too far away
        {
            float dist = (transform.position - enemy.target.transform.position).magnitude;
            if (dist > enemy.maxSight)
            {
                enemy.target = hitter;
            }
        }
    }
}
