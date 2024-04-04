using UnityEngine;

/*
    This script acts as the parent to all projectile collision scripts.
    Those scripts should be pre-attached to the prefabs.

    projectile and startpoint will both be assigned from whatever script creates the prefab.

    OnCollisionEnter handles when the prefab hits anything and deals damage when needed.
*/

abstract public class BaseExplosionCollision : MonoBehaviour
{
    [HideInInspector] public BaseProjectile projectile;
    [HideInInspector] public Vector3 startpoint;
    

    abstract public void OnTriggerEnter(Collider other);

    abstract public void Update();
}
