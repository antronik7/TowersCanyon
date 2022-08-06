using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float attackRange;
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
                SpawnProjectile();
            else
                attackSpeedTimer = 0f;
        }
    }

    private void SpawnProjectile()
    {
        GameObject newProectile = Instantiate(projectile, projectileSpawnPosition.position, Quaternion.identity) as GameObject;
        newProectile.GetComponent<Projectile>().Launch(enemyInRange[0].transform, projectileSpeed);
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
}
