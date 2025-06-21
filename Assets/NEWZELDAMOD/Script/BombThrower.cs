using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public LayerMask enemyLayer;               // Layer dos inimigos
    public float throwDistance = 2f;           // Dist�ncia para onde a bomba "cai"
    public float explosionRadius = 4f;          // Raio da explos�o
    public int bombDamage = 50;                 // Dano da bomba
    public float delayBeforeExplode = 0.5f;     // Tempo antes da explos�o

    public GameObject explosionEffectPrefab;    // Prefab do efeito de explos�o

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(ExplodeBomb());
        }
    }

    IEnumerator ExplodeBomb()
    {
        // Calcula a posi��o da explos�o (na frente do jogador)
        Vector3 explosionPos = transform.position + transform.forward * throwDistance;

        Debug.Log("Bomba lan�ada! Explos�o em " + delayBeforeExplode + " segundos...");

        // Espera o tempo de delay
        yield return new WaitForSeconds(delayBeforeExplode);

        // Spawna o efeito visual da explos�o
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, explosionPos, Quaternion.identity);
        }

        // Faz o OverlapSphere para pegar inimigos na �rea
        Collider[] hitEnemies = Physics.OverlapSphere(explosionPos, explosionRadius, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Vida vida = enemy.GetComponent<Vida>();
            if (vida != null)
            {
                vida.TakeDamage(bombDamage);
                Debug.Log("Inimigo atingido pela bomba: " + enemy.name);
            }
        }

        Debug.DrawRay(explosionPos, Vector3.up * 2, Color.red, 1f);
        Debug.Log("Explos�o conclu�da! Inimigos atingidos: " + hitEnemies.Length);
    }

    private void OnDrawGizmosSelected()
    {
        // Mostra na cena o raio da explos�o
        Vector3 explosionPos = transform.position + transform.forward * throwDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(explosionPos, explosionRadius);
    }
}
