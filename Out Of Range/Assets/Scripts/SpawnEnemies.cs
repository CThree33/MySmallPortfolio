using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject Zombie;
    public float MaxInterval;

    private float timer = 0;
    private float interval;

    void Start()
    {
        
    }


    void Update()
    {
        interval = Random.Range(1, MaxInterval);
        
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0;
            Instantiate(Zombie, transform.position, Quaternion.identity);
            interval = Random.Range(1, MaxInterval);

        }
    }
}
