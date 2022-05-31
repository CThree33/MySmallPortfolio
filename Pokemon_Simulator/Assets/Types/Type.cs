using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Types { Fire, Grass, Water }
[CreateAssetMenu]
public class Type : ScriptableObject
{
    public Types PkmType;
    public Types Weaknesses;
    public Types Strengths;

    public void computeTypes()
    {

    }
}
