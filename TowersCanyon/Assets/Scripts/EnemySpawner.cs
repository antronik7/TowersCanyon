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
    private int numberEnemyLeft;
    private Enemy[] enemyPool;

    //Awake is always called before any Start functions
    void Awake()
    {
        ResetSpawner();
        CreateEnemyPool();
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
        enemyPool[numberToSpawn - numberEnemyLeft].Spawn(transform.position);
        --numberEnemyLeft;
    }

    private void CreateEnemyPool()
    {
        enemyPool = new Enemy[numberToSpawn];

        for (int i = 0; i < numberToSpawn; ++i)
        {
            GameObject newEnemy = Instantiate(enemyToSpawn, new Vector3(-1000f, -1000f, -1000f), Quaternion.identity);
            enemyPool[i] = newEnemy.GetComponent<Enemy>();
        }
    }

    public void ResetSpawner()
    {
        numberEnemyLeft = numberToSpawn;
        spawnTimer = 0f;
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public int GetNbrEnemiesInSpawner()
    {
        return numberToSpawn;
    }
}
