using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private int projectileDamage;
    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform projectileSpawnPosition;
    [SerializeField]
    private Renderer towerRenderer;

    private SphereCollider attackRangeCollider;

    private float attackSpeedTimer = 0;
    private List<Enemy> enemyInRange = new List<Enemy>();

    //Awake is always called before any Start functions
    void Awake()
    {
        attackRangeCollider = GetComponent<SphereCollider>();
        SetAttackRange();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy potentialEnemy = other.GetComponent<Enemy>();
        if (potentialEnemy)
            enemyInRange.Add(potentialEnemy);
    }

    void OnTriggerExit(Collider other)
    {
        Enemy potentialEnemy = other.GetComponent<Enemy>();
        if (potentialEnemy)
            enemyInRange.Remove(potentialEnemy);
    }

    private void ManageAttack()
    {
        attackSpeedTimer -= Time.deltaTime;

        if (attackSpeedTimer <= 0f)
        {
            if (enemyInRange.Count > 0)
            {
                SpawnProjectile();
                attackSpeedTimer = attackSpeed;
            }
            else
            {
                attackSpeedTimer = 0f;
            }
        }
    }

    private void SpawnProjectile()
    {
        if (GetClosestEnemy() == null)//temp for testing...
            return;

        GameObject newProectile = Instantiate(projectile, projectileSpawnPosition.position, Quaternion.identity) as GameObject;
        newProectile.GetComponent<Projectile>().Launch(GetClosestEnemy(), projectileSpeed, projectileDamage);
    }

    public void MakeTransparent(bool transparent)
    {
        Color color = towerRenderer.material.color;
        if (transparent)
            color.a = 0.5f;
        else
            color.a = 1f;

        towerRenderer.material.color = color;
    }

    private void SetAttackRange()
    {
        attackRangeCollider.radius = attackRange;
    }

    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = enemyInRange[0];
        float closestDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemyInRange)
        {
            if (enemy == null)//temp for testing...
                continue;

            float enemyDistance = (transform.position - enemy.transform.position).sqrMagnitude;
            if (enemyDistance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = enemyDistance;
            }
        }

        return closestEnemy;
    }
}
