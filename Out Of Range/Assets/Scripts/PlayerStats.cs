using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;
    public float currentShield;
    public float maxShield = 100;
    public static PlayerStats singleton;
    public Image healthBar;
    public Image shieldBar;
    public Image image;
    private float newAlpha = 100;
    private bool isHealingShield;
    private float timeWithNoHit;
    public GameObject deathScreen;
    public int coins = 0;

    public bool isDead;

    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        newAlpha = 0.2f;
    }

    void Update()
    {
        timeWithNoHit -= Time.deltaTime;
        if (timeWithNoHit <= 0 && currentShield <= maxShield && !isHealingShield)
        {
            isHealingShield = true;
            InvokeRepeating("RegenShield", 1f, 1f);
        }
        if (timeWithNoHit > 0 || currentShield >= maxShield)
        {
            CancelInvoke();
        }
        if (image.color.a > 0)
        {
            Color newColor = image.color;
            newColor.a -= Time.deltaTime;
            image.color = newColor;
        }
    }
    public void TakeDamage(float dmg)
    {
        isHealingShield = false;
        timeWithNoHit = 10;
        if (currentShield > 0)
        {
            currentShield -= dmg;

            shieldBar.fillAmount = currentShield / 100;
        }
        else if (currentShield <= 0)
        {
            if (currentHealth > 0)
            {
                currentHealth -= dmg;

                healthBar.fillAmount = currentHealth / 100;
                if (currentHealth <= 30)
                {
                    healthBar.GetComponent<Image>().color = Color.red;
                }
            }
            else
            {
                StartCoroutine(Dead());
            }
        }
        ChangeAlpha();
    }
    public void RegenShield()
    {
        isHealingShield = true;
        currentShield += 10;
        if (currentShield > maxShield)
        {
            currentShield = maxShield;
        }
        shieldBar.fillAmount = currentShield / 100;
    }
    public void GiveShield(int healammount)
    {
        currentShield += healammount;
    }
    public void ChangeAlpha()
    {
        Color newColor = image.color;
        newColor.a = newAlpha;
        image.color = newColor;
    }
    public IEnumerator Dead()
    {
        currentHealth = 0;
        isDead = true;

        deathScreen.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Gun").GetComponent<GunScript>().enabled = false;

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);

    }
}