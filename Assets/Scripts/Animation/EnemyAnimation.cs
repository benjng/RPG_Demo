using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;
    [SerializeField] private EnemyNavMesh enemyNavMesh;
    private EnemyController enemyController;

    //Animation States
    const string ENEMY_IDLE = "isIdling";
    const string ENEMY_WALK = "isWalking";
    const string ENEMY_GOT_HIT = "triggerGotHit";
    const string ENEMY_DEAD = "triggerDead";

    private string currentState = ENEMY_WALK;
    private int checkedHp;
    private float respawnTime;

    void Start(){
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        checkedHp = enemyController.startingHp;
        respawnTime = enemyController.respawnTime;
    }

    public void ChangeAnimationState(string newState){
        // STOP current state immediately
        if (currentState != newState)
        {
            animator.SetBool(currentState, false);
        }
        animator.SetBool(newState, true);
        currentState = newState;
    }

    void Update(){
        // If enemy is walking
        if (enemyNavMesh.NavMeshAgent.velocity.magnitude >= 1){
            ChangeAnimationState(ENEMY_WALK);
            return;
        }
        
        if (enemyController.currentHp >= checkedHp){
            ChangeAnimationState(ENEMY_IDLE);
            return;
        }
        
        checkedHp = enemyController.currentHp;
        // If enemy is hurt
        if (enemyController.currentHp > 0){
            animator.SetTrigger(ENEMY_GOT_HIT);
            return;
        }
        // If enemy is dead
        Debug.Log("DEAD animation");
        animator.SetTrigger(ENEMY_DEAD);
        StartCoroutine(WaitForRespawn());
        return;
    }

    private IEnumerator WaitForRespawn(){
        yield return new WaitForSeconds(respawnTime);
        checkedHp = enemyController.currentHp;
        animator.Rebind();
    }
}
