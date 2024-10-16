using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float lerpTimer;

    [Header("Health Bar")]  // cannot be placed directly above a private variable
    public float maxHealth = 100f;
    public float health;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image runesGlowing;
    public GameObject healthText;
    public GameObject healthTextBurnt;
    //public Image backHealthBar;
    //public Image xpBar;
    private PlayerUI playerUI;

    [Header("Damage Overlay")]
    public Image overlay; //the DamageOverlay gameObject
    public float duration; //how long the image is fully opaque
    public float fadeSpeed; //how quickly the image fades
    private float durationTimer; //timer to check against duration

    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        //xpBar.fillAmount = 0f;
        playerUI = GetComponent<PlayerUI>();
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (health <= 0)
        {
            deathScreen.transform.localScale = Vector3.one;
            gameObject.GetComponent<PauseMenu>().canPause = false;
            gameObject.GetComponent<PauseMenu>().isPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (overlay.color.a > 0) 
        {
            if (health < 30) // constantly be red if low hp
                return;
                
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                //fade the image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }


    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = frontHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            healthText.GetComponent<TextMeshProUGUI>().text = health.ToString();
            healthTextBurnt.GetComponent<TextMeshProUGUI>().text = health.ToString();
            //backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            //backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            //backHealthBar.fillAmount = hFraction;
            //backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
            healthText.GetComponent<TextMeshProUGUI>().text = health.ToString();
            healthTextBurnt.GetComponent<TextMeshProUGUI>().text = health.ToString();
        }
        if(frontHealthBar.fillAmount <= 0)
        {
            runesGlowing.fillAmount = 0;
            healthText.GetComponent<TextMeshProUGUI>().text = "";
        }
        playerUI.UpdateHP(health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, .5f);
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
