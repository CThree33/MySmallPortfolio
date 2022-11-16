using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public GameObject Zombie;
    public GameObject BFZ;
    public float MaxInterval;
    public float zombiesToSpawn;
    public float zombieSpawned = 0;
    private float timer = 0;
    private float interval;
    public int roundNum;
    public bool roundIsActive;
    public float zombiesLeft;
    public int randomSpawn;

    public Transform Spawner1;
    public Transform Spawner2;
    public Transform Spawner3;
    public Transform Spawner4;
    public bool canChangeWave = true;

    void Start()
    {
        roundNum = 1;
        roundIsActive = true;
    }

    void Update()
    {
        if (roundIsActive)
        {
            zombiesToSpawn = roundNum * 3;
            zombiesLeft = zombiesToSpawn;
            interval = Random.Range(1, MaxInterval);
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                randomSpawn = Random.Range(0, 4);
                switch (randomSpawn)
                {
                    case 0:
                        timer = 0;
                        Instantiate(Zombie, Spawner1.position, Quaternion.identity);
                        zombieSpawned++;
                        interval = Random.Range(1, MaxInterval);
                        break;
                    case 1:
                        timer = 0;
                        Instantiate(Zombie, Spawner2.position, Quaternion.identity);
                        zombieSpawned++;
                        interval = Random.Range(1, MaxInterval);
                        break;
                    case 2:
                        timer = 0;
                        Instantiate(Zombie, Spawner3.position, Quaternion.identity);
                        zombieSpawned++;
                        interval = Random.Range(1, MaxInterval);
                        break;
                    default:
                        timer = 0;
                        Instantiate(Zombie, Spawner4.position, Quaternion.identity);
                        zombieSpawned++;
                        interval = Random.Range(1, MaxInterval);
                        break;

                }
            }
            if (zombiesToSpawn == 15f)
            {
                randomSpawn = Random.Range(0, 4);
                switch (randomSpawn)
                {
                    case 0:
                        timer = 0;
                        Instantiate(BFZ, Spawner1.position, Quaternion.identity);
                        zombieSpawned++;
                        interval = Random.Range(1, MaxInterval);
                        break;
                    case 1:
                        timer = 0;
                        Instantiate(BFZ, Spawner2.position, Quaternion.identity);
                        zombieSpawned++;
                        interval = Random.Range(1, MaxInterval);
                        break;
                    case 2:
                        timer = 0;
                        Instantiate(BFZ, Spawner3.position, Quaternion.identity);
                        zombieSpawned++;
                        interval = Random.Range(1, MaxInterval);
                        break;
                    default:
                        timer = 0;
                        Instantiate(BFZ, Spawner4.position, Quaternion.identity);
                        zombieSpawned++;
                        interval = Random.Range(1, MaxInterval);
                        break;

                }
                
            }
        }
        if (zombiesToSpawn == zombieSpawned)
        {
            roundIsActive = false;
        }
        if (zombiesLeft == 0)
        {
            zombieSpawned = 0;
            StartCoroutine(NewRound());
        }
    }
    public IEnumerator NewRound()
    {
        if (canChangeWave)
        {
            canChangeWave = false;
            roundNum++;
            yield return new WaitForSeconds(5);
            roundIsActive = true;
            canChangeWave=true;
        }

    }
}