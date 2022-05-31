using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PokemonScript : MonoBehaviour
{
    public Move[] Moves;
    public Pokemon PkmValues;
    public TMP_Text Name;

    public Type Type;
    public Type SecondType;

    public float currentHP;
    public float currentATK;
    public float currentSpATK;
    public float currentDEF;
    public float currentSpDEF;
    public float currentSpeed;

    private string baseURL = "https://pokeapi.co/api/v2/";

    // Start is called before the first frame update
    void Awake()
    {
        Type = PkmValues.Type;
        Name.text = PkmValues.Name;
        currentHP = PkmValues.HP;
        currentATK = PkmValues.ATK;
        currentSpATK = PkmValues.SpATK;
        currentDEF = PkmValues.DEF;
        currentSpDEF = PkmValues.DEF;
        currentSpeed = PkmValues.Speed;
        gameObject.GetComponent<Image>().sprite = PkmValues.Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
