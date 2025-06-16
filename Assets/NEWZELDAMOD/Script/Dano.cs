using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{
    public int lives = 1;
    public IAStarFPS iastar;

    void Start()
    {
        // Se quiser inicializar algo
    }

    void Update()
    {
        if (lives <= 0)
        {
            if (iastar != null)
            {
                iastar.Dead();
            }
            Destroy(gameObject, 4f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);  // Exemplo: tirar 1 de vida
                Debug.Log("Inimigo causou dano no Player");
            }
        }
    }

    public void ExplosionDamage()
    {
        lives -= 1;
        Debug.Log("Inimigo tomou dano por explosão. Vidas restantes: " + lives);
    }
}
