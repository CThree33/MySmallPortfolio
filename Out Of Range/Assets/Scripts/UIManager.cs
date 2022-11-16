using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthInfoText;
    public TextMeshProUGUI roundInfoText;
    public TextMeshProUGUI ammoInfoText;
    public TextMeshProUGUI zombieInfoText;
    public TextMeshProUGUI coinsInfoText;
    void Start()
    {

    }


    void Update()
    {

        PlayerStats health = FindObjectOfType<PlayerStats>();
        if (health.currentHealth <= 0)
        {
            health.currentHealth = 0;
        }
        healthInfoText.text = health.currentHealth + "";

        PlayerMovement coins = FindObjectOfType<PlayerMovement>();
        
        coinsInfoText.text = "Coins: " + coins.Coins + "";
        Debug.Log(coins.Coins);

        GunScript currentgun = FindObjectOfType<GunScript>();
        if (currentgun.currentAmmo <= 0)
        {
            currentgun.currentAmmo = 0;
        }
        ammoInfoText.text = currentgun.currentAmmo + "/" + currentgun.clipSize;

        WaveManager round = FindObjectOfType<WaveManager>();
        roundInfoText.text = "Round " + round.roundNum;

        WaveManager zombie = FindObjectOfType<WaveManager>();
        zombieInfoText.text = (zombie.zombiesLeft) + "";
    }
}