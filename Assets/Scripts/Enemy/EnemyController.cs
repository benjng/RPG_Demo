using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamagable
{
    public HealthBar healthBar;
    public Entity enemyData; // Scriptable entity
    public int startingHp = 100;
    public float respawnTime = 5;

    [HideInInspector]
    public int currentHp;
    
    private Vector3 respawnPosition;

    void Start()
    {
        respawnPosition = transform.position;
        InitStat();
    }

    private void InitStat()
    {
        healthBar.SetMaxHealth(startingHp);
        currentHp = startingHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        healthBar.Damage(damage);

        if (currentHp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Enemy Died!");
        // Die animation

        // Disable this enemy
        GetComponent<Collider>().enabled = false;
        GetComponent<EnemyNavMesh>().enabled = false;
        if (Random.Range(1, 5) > 3) // 2/5 probability dropping item
        {
            
        }
        ItemDrop();

        this.enabled = false;
        StartCoroutine(Revive());
    }

    public void ItemDrop()
    {
        GameObject clonedItem = Instantiate(enemyData.itemDrops[Random.Range(0, enemyData.itemDrops.Length)]);
        clonedItem.transform.position = transform.position + new Vector3(1,5,1);
    }

    IEnumerator Revive()
    {
        yield return new WaitForSeconds(respawnTime);
        Debug.Log("Revived");

        GetComponent<Collider>().enabled = true;
        GetComponent<EnemyNavMesh>().enabled = true;
        this.enabled = true;

        InitStat();
        transform.position = respawnPosition;
    }
}
