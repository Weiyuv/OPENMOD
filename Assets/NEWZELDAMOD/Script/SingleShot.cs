using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab do projetil (ex: uma bala)
    public Transform firePoint;          // Ponto de onde o projetil sai
    public float projectileSpeed = 20f;  // Velocidade do projetil
    public int shotDamage = 25;          // Dano que o projetil vai causar

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))  // Botão direito do mouse ou outro input
        {
            FireShot();
        }
    }

    void FireShot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instancia o projetil na posição do firePoint, olhando para frente
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Adiciona velocidade ao projetil
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * projectileSpeed;
            }

            // Passa o dano para o projetil
            ProjectileDamage pd = projectile.GetComponent<ProjectileDamage>();
            if (pd != null)
            {
                pd.damage = shotDamage;
            }
        }
    }
}
