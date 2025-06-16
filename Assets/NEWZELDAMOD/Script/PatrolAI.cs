using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float chaseRange = 10f;
    public float returnRange = 15f;
    public GameObject rewardItem;

    private int currentPoint = 0;
    private NavMeshAgent agent;
    private GameObject player;
    private bool chasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < chaseRange)
        {
            chasing = true;
        }
        else if (distance > returnRange)
        {
            chasing = false;
        }

        if (chasing)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.5f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }

    public void OnDefeated()
    {
        Instantiate(rewardItem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
