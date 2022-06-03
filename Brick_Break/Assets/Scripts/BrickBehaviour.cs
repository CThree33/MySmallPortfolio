using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{
    public int HP = 1;

    private BrickInstantiator gameMgr;

    private void Awake()
    {
        gameMgr = GameObject.Find("GameMgr").GetComponent<BrickInstantiator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetTexture();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0 )
            Destroy(gameObject);
    }

    public void TakeDamage()
    {
        HP--;
        ResetTexture();
        Debug.Log(gameMgr.Colors[HP-1]);
    }

    private void ResetTexture()
    {
        transform.GetComponent<Renderer>().material.color = gameMgr.Colors[HP-1];
    }

    private void OnDestroy()
    {
        gameMgr.AddPoints();
        gameMgr.bricksCount--;
    }
}
