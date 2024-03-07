using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseWaveCollision : MonoBehaviour
{
    [HideInInspector] public BaseWave projectile;
    [HideInInspector] public Vector3 startpoint;

    public float timeBeforeDestroy;

    abstract public void OnCollisionEnter(Collision other);


    abstract public void Update();
}
