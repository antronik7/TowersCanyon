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
    void Update()
    {
        if (target == null)//temp for testing...
            Destroy(gameObject);
        else
            MoveProjectile();
    }

    private void MoveProjectile()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

        if (transform.position == target.transform.position)
            DamageTarget();
    }

    private void DamageTarget()
    {
        target.ReceiveDamage(damage);
        Destroy(gameObject);
    }

    public void Launch(Enemy towerTarget, float towerProjectileSpeed, int towerProjectileDamage)
    {
        target = towerTarget;
        speed = towerProjectileSpeed;
        damage = towerProjectileDamage;
    }
}
