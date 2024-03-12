using UnityEngine;

abstract public class BaseEnemyCollision : MonoBehaviour
{
    [HideInInspector] public BaseEnemy baseEnemy;
    [HideInInspector] public Vector3 startpoint;

    public Animator animator;
    public float timeBeforeDestroy;
    public float timeBeforeAnimation;

    abstract public void OnTriggerEnter(Collider other);

    abstract public void AnimationCollisionStart();

    abstract public void Update();
}
