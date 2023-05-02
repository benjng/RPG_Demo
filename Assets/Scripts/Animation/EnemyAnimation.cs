using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;
    [SerializeField] private EnemyNavMesh enemyNavMesh;
    private EnemyController enemyController;

    //Animation States
    const string ENEMY_GOT_HURT = "GetHit";
    const string ENEMY_WALK = "Walk";
    const string ENEMY_DEAD = "isDead";
    const string ENEMY_IDLE = "Idle";
    //const string ENEMY_ATTACK = "Attack";
    //const string ENEMY_RUN = "Sprint";

    private string currentState = ENEMY_WALK;
    private int checkedHp;
    private float respawnTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        checkedHp = enemyController.startingHp;
        respawnTime = enemyController.respawnTime;
    }

    void Update()
    {
        // If enemy is walking
        if (enemyNavMesh.NavMeshAgent.velocity.magnitude >= 1)
        {
            animator.SetTrigger(ENEMY_WALK);
            currentState = ENEMY_WALK;
            return;
        }

        // If enemy is hurt
        if (enemyController.currentHp < checkedHp)
        {
            currentState = ENEMY_GOT_HURT;
            checkedHp = enemyController.currentHp;
            // If enemy is dead
            if (enemyController.currentHp <= 0)
            {
                animator.SetBool(ENEMY_DEAD, true);
                StartCoroutine(WaitForRespawn());
                return;
            }
            animator.SetTrigger(ENEMY_GOT_HURT);
            return;
        }

        animator.ResetTrigger(currentState);
        animator.SetTrigger(ENEMY_IDLE);
        currentState = ENEMY_IDLE;
    }

    private IEnumerator WaitForRespawn()
    {
        Debug.Log("In corotin");
        yield return new WaitForSeconds(respawnTime);
        Debug.Log("corotin done");
        checkedHp = enemyController.currentHp;
        animator.SetBool(ENEMY_DEAD, false);
    }
}
