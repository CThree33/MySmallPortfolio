using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrickInstantiator : MonoBehaviour
{
    public GameObject BrickObj;
    public int Rows;
    public int Columns;
    public List<Color> Colors;
    [HideInInspector]
    public int Score;
    public TMP_Text ScoreText;
    public TMP_Text ResultText;

    private float xOffset;
    private float yOffset;
    private int scoreToAdd;
    private int scoreToSubtract;
    public int bricksCount;
    
    [SerializeField]
    private float xDistance = 2;
    [SerializeField]
    private float yDistance = 0.80f;

    private int hpToAssign = 1;
    void Start()
    {
        bricksCount = 0;
        scoreToAdd = 25;
        scoreToSubtract = 10;

        InstantiateBricks();
    }

    private void InstantiateBricks()
    {
        //brick instantiation formula, instantiate the horizontal ones first, then go one column down, then repeat
        yOffset = (BrickObj.transform.localScale.y * 0.5f) + yDistance;

        for (int i = 0; i < Rows; i++)
        {
            xOffset = (BrickObj.transform.localScale.x * 0.5f) + xDistance;

            for (int j = 0; j < Columns; j++)
            {
                GameObject brick = Instantiate(BrickObj, new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, 0),
                    Quaternion.identity);

                brick.GetComponentInChildren<BrickBehaviour>().HP = hpToAssign;
                xOffset += xDistance;

                bricksCount++;
            }

            yOffset += yDistance;
            hpToAssign++;
        }
    }

    public void AddPoints()
    {
        Score += scoreToAdd;
        ScoreText.text = "Score: " + Score.ToString();
    }

    public void SubtractPoints()
    {
        Score -= scoreToSubtract;
        ScoreText.text = "Score: " + Score.ToString();
    }

    public void Win()
    {
        Time.timeScale = 0;
        ResultText.text = "You Win!";
    }

    public void Lose()
    {
        Time.timeScale = 0;
        ResultText.text = "You Lose!";
    }

    // Update is called once per frame
    void Update()
    {
        if (bricksCount <= 0)
            Win();
    }
}
