using UnityEngine;

public class Vida : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public bool isDead = false;
    public GameObject deathEffect;
    public GameObject respawnEffect;
    public GameObject healEffect;
    public Vector3 respawnPoint;
    public float respawnTime = 5f;
    public Animator animator;
    public MoveChanPhisical moveChanPhisical;

    void Start()
    {
        currentHealth = maxHealth;
        respawnPoint = transform.position;

        moveChanPhisical = GetComponent<MoveChanPhisical>();
        if (moveChanPhisical != null)
        {
            animator = moveChanPhisical.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnPoint"))
        {
            respawnPoint = other.transform.position;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Player tomou dano: " + damage);
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        if (animator != null)
        {
            animator.SetTrigger("Heal");
        }

        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        Debug.Log("Player morreu");
        if (animator != null)
        {
            animator.SetBool("Die", true);
        }

        isDead = true;
        gameObject.SetActive(false);
        Invoke("Respawn", respawnTime);
    }

    public void Respawn()
    {
        Debug.Log("Player respawnou");
        if (animator != null)
        {
            animator.SetBool("Die", false);
        }

        isDead = false;
        currentHealth = maxHealth;
        transform.position = respawnPoint;
        gameObject.SetActive(true);
    }
}
