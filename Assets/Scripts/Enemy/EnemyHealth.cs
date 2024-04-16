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
    /// </summary>
    /// <remarks>
    /// hitter should usually be <c>gameObject</c>
    /// </remarks>
    /// <param name="damage">Damage to deal to enemy</param>
    /// <param name="hitter">The player doing damage. Usually should be <c>gameObject</c>.</param>
    public void TakeDamage(float damage, GameObject hitter)
    {
        health -= damage;

        // If dead, die and end here
        if (health <= 0) 
        {
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

    /// <summary>
    /// Old, to be replaced.
    /// Updating to the new one will require an additional field in all collisions 
    /// and will therefore need to change all ManageCollisionComponents. 
    /// <para>
    /// Add a reference to the player gameObject in each collision:
    /// <c>CollisionScriptNameHere.caster = gameObject;</c>
    /// </para>
    /// </summary>
    /// <remarks>
    /// See Scripts > Enemy > EnemyHealth.cs for the new usage
    /// </remarks>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        health -= damage;

        // If dead, die and end here
        if (health <= 0) 
        {
            GetComponentInParent<RoomHandler>().enemies.Remove(this.gameObject);
            GetComponentInParent<RoomHandler>().numEnemies--;
            GetComponentInParent<RoomHandler>().UpdateObjective();
            Destroy(gameObject); // do death animation
            return;
        }
    }
}
