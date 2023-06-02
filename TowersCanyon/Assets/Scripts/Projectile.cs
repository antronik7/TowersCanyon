using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy target;
    private float speed;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()//Maybe this should be late update to make sure everything was moved...
    {
        MoveProjectile();
        CheckForImpact();
    }

    private void MoveProjectile()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.GetPosition(), step);
    }

    private void CheckForImpact()
    {
        if (transform.position == target.GetPosition())
            DamageTarget();
    }

    private void DamageTarget()
    {
        if (target.CheckIfAlive())
            target.ReceiveDamage(damage);

        UnspawnProjectile();
    }

    private void UnspawnProjectile()
    {
        Destroy(gameObject);
    }

    public void Launch(Enemy towerTarget, float towerProjectileSpeed, int towerProjectileDamage)
    {
        target = towerTarget;
        speed = towerProjectileSpeed;
        damage = towerProjectileDamage;
    }
}
