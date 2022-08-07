using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private int numberToSpawn;
    [SerializeField]
    private GameObject enemyToSpawn;

    private float spawnTimer = 0;
    private float numberEnemyLeft;

    //Awake is always called before any Start functions
    void Awake()
    {
        numberEnemyLeft = numberToSpawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageSpawn();
    }

    private void ManageSpawn()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            if (numberEnemyLeft > 0)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval;
            }
            else
            {
                spawnTimer = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        --numberEnemyLeft;
    }
}
