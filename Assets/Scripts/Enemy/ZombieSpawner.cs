using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner Instance;

    public int enemiesAlive;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;

    private void Awake()
    {
        Instance = this;
        enemiesAlive = 1;
    }

    private void Start()
    {
        NextWave(1);
    }

    public void NextWave(int round)
    {
        enemiesAlive = round * 2;

        for (int i = 0; i < enemiesAlive; i++)
        {
            int randomPos = Random.Range(0, spawnPoints.Length);
            GameObject spawnPoint = spawnPoints[randomPos];

            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            GameObject enemySpawn = enemyPrefabs[randomEnemy];

            if(round >= 1 && round <= 3)
            {
                Debug.Log("R4");
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.speed = 6;
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.acceleration = 6;
                enemySpawn.GetComponent<Enemy>().enemyDamage = 20;
                enemySpawn.GetComponent<Enemy>().health = 100;
            } 
            else if(round >= 4 && round <= 6)
            {
                Debug.Log("R4");
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.speed = 8;
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.acceleration = 6;
                enemySpawn.GetComponent<Enemy>().enemyDamage = 30;
                enemySpawn.GetComponent<Enemy>().health = 120;
            } 
            else if(round >= 7 && round <= 9) 
            {
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.speed = 10;
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.acceleration = 8;
                enemySpawn.GetComponent<Enemy>().enemyDamage = 35;
                enemySpawn.GetComponent<Enemy>().health = 160;
            }
            else if(round >= 10 && round <= 12) 
            {
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.speed = 12;
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.acceleration = 10;
                enemySpawn.GetComponent<Enemy>().enemyDamage = 40;
                enemySpawn.GetComponent<Enemy>().health = 200;
            }
            else if(round >= 13 && round <= 15) 
            {
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.speed = 12;
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.acceleration = 12;
                enemySpawn.GetComponent<Enemy>().enemyDamage = 50;
                enemySpawn.GetComponent<Enemy>().health = 220;
            }
            else if(round >= 16 && round <= 20) 
            {
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.speed = 12;
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.acceleration = 16;
                enemySpawn.GetComponent<Enemy>().enemyDamage = 50;
                enemySpawn.GetComponent<Enemy>().health = 250;
            } else if(round >= 21) {
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.speed = 12;
                enemySpawn.GetComponent<Enemy>().NavMeshAgent.acceleration = 16;
                enemySpawn.GetComponent<Enemy>().enemyDamage = 75;
                enemySpawn.GetComponent<Enemy>().health = 300;
            }

            Instantiate(enemySpawn, spawnPoint.transform.position, Quaternion.identity);
        }
    }

    public void CheckRounds()
    {
        if(enemiesAlive <= 0)
        {
            GameManager.Instance.ChangeRound();
            NextWave(GameManager.Instance.round);
        }
    }


}
