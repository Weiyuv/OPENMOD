using UnityEngine;
using UnityEngine.AI;

public class HealerAI : MonoBehaviour
{
    public float interactionRange = 3f;
    public int healAmount = 2;
    public KeyCode interactKey = KeyCode.E;
    public GameObject healEffect;
    public float wanderRadius = 5f;
    public float wanderInterval = 10f;

    private GameObject player;
    private Health playerHealth;
    private NavMeshAgent agent;
    private float wanderTimer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
            playerHealth = player.GetComponent<Health>();

        agent = GetComponent<NavMeshAgent>();

        // Movimento bem devagar
        if (agent != null)
            agent.speed = 1f;

        wanderTimer = wanderInterval;
    }

    void Update()
    {
        if (player == null || playerHealth == null)
            return;

        // --- Movimento leve (Wander) ---
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderInterval && agent != null)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            wanderTimer = 0;
        }

        // --- Interação de cura ---
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= interactionRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                HealPlayer();
            }
        }
    }

    void HealPlayer()
    {
        if (playerHealth.currentHealth < playerHealth.maxHealth)
        {
            playerHealth.Heal(healAmount);

            if (healEffect != null)
                Instantiate(healEffect, player.transform.position, Quaternion.identity);

            Debug.Log("Curandeiro curou o jogador!");
        }
        else
        {
            Debug.Log("O jogador já está com vida cheia!");
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
