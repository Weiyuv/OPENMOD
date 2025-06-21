using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public LayerMask enemyLayer;               // Layer dos inimigos
    public float throwDistance = 2f;           // Distância para onde a bomba "cai"
    public float explosionRadius = 4f;          // Raio da explosão
    public int bombDamage = 50;                 // Dano da bomba
    public float delayBeforeExplode = 0.5f;     // Tempo antes da explosão

    public GameObject explosionEffectPrefab;    // Prefab do efeito de explosão

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(ExplodeBomb());
        }
    }

    IEnumerator ExplodeBomb()
    {
        // Calcula a posição da explosão (na frente do jogador)
        Vector3 explosionPos = transform.position + transform.forward * throwDistance;

        Debug.Log("Bomba lançada! Explosão em " + delayBeforeExplode + " segundos...");

        // Espera o tempo de delay
        yield return new WaitForSeconds(delayBeforeExplode);

        // Spawna o efeito visual da explosão
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, explosionPos, Quaternion.identity);
        }

        // Faz o OverlapSphere para pegar inimigos na área
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
        Debug.Log("Explosão concluída! Inimigos atingidos: " + hitEnemies.Length);
    }

    private void OnDrawGizmosSelected()
    {
        // Mostra na cena o raio da explosão
        Vector3 explosionPos = transform.position + transform.forward * throwDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(explosionPos, explosionRadius);
    }
}
