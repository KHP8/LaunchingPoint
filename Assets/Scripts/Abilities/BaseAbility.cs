using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

abstract public class BaseAbility : MonoBehaviour
{
    public string abilityName;
    public Image image;

    public bool canCast = true;
    [HideInInspector] public WaitForSeconds cooldown;
    [HideInInspector] public float cooldownFloat;
    public AudioClip castSFX;

    abstract public void Awake();

    /// <summary>
    /// Performs the ability. Handled in internals of Base scripts.
    /// </summary>
    /// <remarks>
    /// This method should only be called from the input manager.
    /// </remarks>
    abstract public bool UseAbility();

    /// <summary>
    /// Ends the ability. Useful for full auto or holding down. Handled in internals of Base scripts.
    /// </summary>
    /// <remarks>
    /// This method should only be called from the input manager.
    /// </remarks>
    abstract public void StopAbility();

    /// <summary>
    /// Assigns values to the prefab's collision component.
    /// </summary>
    /// <param name="obj">The prefab of the ability being cast.</param>
    abstract public void ManageCollisionComponents(GameObject obj);

    /// <summary>
    /// Cooldown for the ability
    /// </summary>
    public IEnumerator ResetCastCooldown()
    {
        yield return cooldown;
        canCast = true;
    }
}
