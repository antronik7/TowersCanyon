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
    private Vector3 targetPosition = Vector3.zero;

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
        enemyPool[numberToSpawn - numberEnemyLeft].Spawn(transform.position, targetPosition);
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

    private void SetTarget()
    {
        Vector2 towerGridPosition = GridManager.instance.GetClosestTowerPosition(transform.position);
        targetPosition = GridManager.instance.GetCellPosition((int)towerGridPosition.x, (int)towerGridPosition.y);//Probably will create problems with float cast...
        targetPosition += (transform.position - targetPosition).normalized * 0.4f;//Small offset so the position of the spawner change the target position...
    }

    public void ResetSpawner()
    {
        numberEnemyLeft = numberToSpawn;
        spawnTimer = 0f;
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        SetTarget();
        gameObject.SetActive(true);
    }

    public int GetNbrEnemiesInSpawner()
    {
        return numberToSpawn;
    }
}
