using UnityEngine;
using UnityEngine.AI;

public class FleeAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float fleeSpeed = 5f;
    public float checkInterval = 0.5f;
    public Animator animator;  // Slot para o Animator no Inspector

    private NavMeshAgent agent;
    private bool isFleeing = false;
    private float checkTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = fleeSpeed;

        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isFleeing && distanceToPlayer <= detectionRange)
        {
            StartFleeing();
        }

        if (isFleeing)
        {
            checkTimer += Time.deltaTime;

            if (checkTimer >= checkInterval)
            {
                checkTimer = 0f;

                float newDistance = Vector3.Distance(transform.position, player.position);

                if (newDistance > detectionRange)
                {
                    StopFleeing();
                }
                else
                {
                    Vector3 directionAway = (transform.position - player.position).normalized;
                    Vector3 fleeTarget = transform.position + directionAway * 5f;

                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(fleeTarget, out hit, 5f, NavMesh.AllAreas))
                    {
                        agent.SetDestination(hit.position);
                    }
                }
            }
        }

        // Atualiza o parâmetro "Speed" do Animator baseado na velocidade real
        if (animator != null)
        {
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }

    void StartFleeing()
    {
        isFleeing = true;
    }

    void StopFleeing()
    {
        isFleeing = false;
        agent.ResetPath();

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
        }
    }
}
