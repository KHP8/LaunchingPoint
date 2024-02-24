using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class BaseAbility : MonoBehaviour
{
    public string abilityName;
    public UnityEngine.UI.Image image;

    abstract public void UseAbility();
    abstract public void StopAbility();
}
