using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseWaveCollision : MonoBehaviour
{
    [HideInInspector] public BaseWave projectile;
    [HideInInspector] public Vector3 startpoint;
    [HideInInspector] public GameObject player;
    public List<GameObject> hitList = new List<GameObject>();

    public float timeBeforeDestroy;

    abstract public void OnTriggerEnter(Collider other);


    abstract public void Update();
}
