using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonColorChange : MonoBehaviour
{
    public Move Move;
    public TMP_Text MoveButtonName;

    private Image buttonImg;
    private Color buttonColor;
    private Color wantedColor;

    void Start()
    {
        //if there is no move assigned to the button, get rid of it
        if (Move == null)
            Destroy(gameObject);

        //set the move button text
        MoveButtonName.text = Move.MoveName;

        //set the move button color
        buttonImg = gameObject.GetComponent<Image>();
        switch (Move.MoveType.PkmType)
        {
            case Types.Fire:
                buttonImg.color = Color.red;
                break;
            case Types.Water:
                buttonImg.color = Color.blue;
                break;
            case Types.Grass:
                buttonImg.color = Color.green;
                break;
            default:
                buttonImg.color = Color.white;
                break;
        }
    }
}
