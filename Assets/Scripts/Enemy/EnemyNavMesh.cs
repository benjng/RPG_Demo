using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] private Transform chaseTargetTransform;
    private NavMeshAgent navMeshAgent;
    
    public NavMeshAgent NavMeshAgent { get { return navMeshAgent; }}

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = GetComponent<EnemyController>().enemyData.walkSpeed;
    }

    void Update()
    {
        // Move towards chasing target (player)
        navMeshAgent.destination = chaseTargetTransform.position;
    }
}
