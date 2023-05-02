using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamagable
{
    public HealthBar healthBar; 
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private float nextAtkTime = 0f;
    public Weapon weaponData;
    public int HP = 100;

    void Start()
    {
        healthBar.SetMaxHealth(HP);
    }

    void Update()
    {
        if (Time.time < nextAtkTime) return;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            nextAtkTime = Time.time + weaponData.attackRate;
        } 
    }

    void Attack()
    {
        // Detect enemies in range of attack
        // OverlapSphere(the center of the sphere, radius, layers to detect)
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, weaponData.attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(weaponData.attackDmg);
        } 
    }

    // IDamagable Implement 
    public void TakeDamage(int damage)
    {
        HP -= damage;
        healthBar.Damage(damage);

        // Hurt animation here

        if (HP <= 0)
        {
            //Die();
        }
    }
    public void Die()
    {
        // Die animation
        // Disable the player
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmosSelected() // For debugging on sword attack
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, weaponData.attackRange);
    }
}
