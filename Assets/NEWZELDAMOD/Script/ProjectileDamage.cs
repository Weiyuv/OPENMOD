using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage = 25;
    public LayerMask enemyLayer;
    public GameObject hitEffectPrefab;  // (Opcional) Efeito ao colidir

    void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto está na layer dos inimigos
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Dá dano
            Vida vida = collision.gameObject.GetComponent<Vida>();
            if (vida != null)
            {
                vida.TakeDamage(damage);
            }

            // Efeito visual no impacto
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }

            // Destroi o projetil após acertar
            Destroy(gameObject);
        }
        else
        {
            // Também destrói ao bater em qualquer outra coisa (se quiser)
            Destroy(gameObject);
        }
    }
}
