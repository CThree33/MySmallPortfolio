using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[CreateAssetMenu]
public class Pokemon : ScriptableObject
{
    public float CurrentHP { get; private set; }

    public string Name;

    public Type Type;
    public Type SecondType;
    public Move[] Moves;

    public float HP;
    public float ATK;
    public float SpATK;
    public float DEF;
    public float SpDEF;
    public float Speed;

    public Sprite Sprite;

    
}
