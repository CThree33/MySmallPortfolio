using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameStates { START, PLAYERTURN, ENEMYTURN, WIN, LOSS}

public class BattleSystem : MonoBehaviour
{
    public GameStates state;

    public GameObject OwnPkm;
    public GameObject EnemyPkm;

    public Slider ownSlider;
    public Slider enemySlider;

    //TODO: find a way to set the hp bar via SO and not via GO
    private void Awake()
    {
        state = GameStates.START;
        StartBattle();
    }
    void Start()
    {
        SetHP();
    }

    void StartBattle()
    {
        
    }

    void SetHP()
    {
        enemySlider.minValue = 0;
        enemySlider.maxValue = EnemyPkm.GetComponent<PokemonScript>().currentHP;
        enemySlider.value = enemySlider.maxValue;

        ownSlider.minValue = 0;
        ownSlider.maxValue = OwnPkm.GetComponent<PokemonScript>().currentHP;
        ownSlider.value = ownSlider.maxValue;
    }
}
